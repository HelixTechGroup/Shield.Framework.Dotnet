#region Usings
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Shield.Framework.Exceptions;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Collections;
using Shield.Framework.Validation.Exceptions;
#endregion

namespace Shield.Framework.Validation
{
    public sealed class ValidationTarget<TType> : IValidationTarget<TType>
    {
        #region Members
        private readonly IDictionary<string, object> m_exceptionData;
        private readonly string m_file;
        private readonly int m_line;
        private readonly string m_name;
        private readonly ValidationRuleSet<TType> m_rules;
        private readonly string m_source;
        private readonly TType m_value;
        #endregion

        #region Properties
        public KeyValuePair<string, object>[] ExceptionData
        {
            get { return m_exceptionData.ToArray(); }
        }

        public string File
        {
            get { return m_file; }
        }

        public int Line
        {
            get { return m_line; }
        }

        public string Name
        {
            get { return m_name; }
        }

        public IEnumerable<IValidationRule<TType>> Rules
        {
            get { return m_rules; }
        }

        public string Source
        {
            get { return m_source; }
        }

        public TType Value
        {
            get { return m_value; }
        }
        #endregion

        public ValidationTarget(TType value,
                                string name,
                                [CallerFilePath] string file = null,
                                [CallerMemberName] string source = null,
                                [CallerLineNumber] int line = -1)
        {
            m_value = value;
            m_name = name;
            m_file = file;
            m_source = source;
            m_line = line;

            m_name.ThrowIfNull();

            m_rules = new ValidationRuleSet<TType>(this);
            m_exceptionData = new ConcurrentDictionary<string, object>();
            SetExceptionData();
        }

        public ValidationTarget(Expression<Func<TType>> memberExpression,
                                [CallerFilePath] string file = null,
                                [CallerMemberName] string source = null,
                                [CallerLineNumber] int line = -1)
        {
            m_name = memberExpression.GetMemberName();
            m_value = memberExpression.Compile()();
            m_file = file;
            m_source = source;
            m_line = line;

            m_name.ThrowIfNull();

            m_rules = new ValidationRuleSet<TType>(this);
            m_exceptionData = new ConcurrentDictionary<string, object>();
            SetExceptionData();
        }

        #region Methods
        public static implicit operator TType(ValidationTarget<TType> argument) => argument.Value;

        public static bool IsOfType(object value, bool throwError = false)
        {
            if (value.ContainsType<TType>())
                return true;

            if (!throwError)
                return false;

            var ruleType = typeof(TType).FullName;
            var valueType = value.GetType().FullName;
            throw ExceptionProvider.ArgumentException("",
                                                     ExceptionMessages.TypesIsOfTypeFailed.Inject(ruleType, valueType));
        }

        public static bool IsNotOfType(object value, bool throwError = false)
        {
            if (!value.ContainsType<TType>())
                return true;

            if (!throwError)
                return false;

            var ruleType = typeof(TType).FullName;
            throw ExceptionProvider.ArgumentException("",
                                                     ExceptionMessages.TypesIsNotOfTypeFailed.Inject(ruleType));
        }

        public void AddExceptionData(string key, object value)
        {
            m_exceptionData.Add(key, value.ToString());
        }

        public IValidationTarget<TType> And(IValidationPredicate<TType> predicate)
        {
            predicate.ThrowIfNull();

            m_rules.CurrentRule.AddPredicate(predicate);
            return this;
        }

        public IValidationTarget<TType> And(Predicate<TType> predicate, string message)
        {
            predicate.ThrowIfNull();
            message.ThrowIfNull();

            m_rules.CurrentRule.AddPredicate(new ValidationPredicate<TType, ArgumentException>(predicate, message));
            return this;
        }

        public IValidationTarget<TType> Or(IValidationPredicate<TType> predicate)
        {
            predicate.ThrowIfNull();

            m_rules.Add(new ValidationRule<TType>(this).AddPredicate(predicate));
            return this;
        }

        public IValidationTarget<TType> Or(Predicate<TType> predicate, string message)
        {
            predicate.ThrowIfNull();
            message.ThrowIfNull();

            m_rules.Add(new ValidationRule<TType>(this).AddPredicate(
                                                                     new ValidationPredicate<TType, ArgumentException>(predicate, message)));
            return this;
        }

        public IValidationTarget<TType> Or()
        {
            m_rules.Add(new ValidationRule<TType>(this));
            return this;
        }

        public IValidate<TType> WithMessage(string message)
        {
            message.ThrowIfNull();

            m_rules.WithMessage(message);
            return this;
        }

        public ValidationExceptionSelector<TType> WithException()
        {
            return new ValidationExceptionSelector<TType>(this);
        }

        public IValidate<TType> WithException<TNewException>() where TNewException : Exception, new()
        {
            m_rules.WithException<TNewException>();
            return this;
        }

        public ValidationResult Validate()
        {
            return m_rules.Validate();
        }

        public Exception Throw()
        {
            throw m_rules.Throw();
        }

        public TException Throw<TException>(string message = null) where TException : Exception, new()
        {
            throw m_rules.Throw<TException>(message);
        }

        public ValidationException ThrowAll()
        {
            throw m_rules.ThrowAll();
        }

        public IValidationTarget<TType> IsRequired()
        {
            return And(new ValidationPredicate<TType, ArgumentException>(v => v != null
                                                                              && !v.Equals(default(TType)),
                                                                         ExceptionMessages.CommonIsRequiredFailed));
        }

        public IValidationTarget<TType> IsNotNull()
        {
            return And(new ValidationPredicate<TType, ArgumentNullException>(v => v != null,
                                                                             ExceptionMessages.CommonIsNotNullFailed));
        }

        public IValidationTarget<TType> IsNotDefault()
        {
            return And(new ValidationPredicate<TType, ArgumentException>(v => !v.Equals(default(TType)),
                                                                         ExceptionMessages.CommonIsNotDefaultFailed));
        }

        private void SetExceptionData()
        {
            m_exceptionData.Add("Value", m_value);
            m_exceptionData.Add("Name", m_name);
            m_exceptionData.Add("File", m_file);
            m_exceptionData.Add("Source", m_source);
            m_exceptionData.Add("Line", m_line);
        }
        #endregion
    }
}