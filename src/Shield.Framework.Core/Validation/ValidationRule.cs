using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Shield.Framework.Exceptions;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation
{
    public class ValidationRule<TType>
    {
        protected TType m_value;
        protected string m_name;
        protected string m_file;
        protected string m_source;
        protected int m_line;
        protected IDictionary<string, string> m_exceptionData;
        protected IList<Predicate<TType>> m_conditions;
        protected IList<RuleValidator<TType>> m_validators;
        protected bool m_throwErrors;
        protected ValidationResult m_results;

        public TType Value
        {
            get { return m_value; }
        }

        public string Name
        {
            get { return m_name; }
        }

        public string File
        {
            get { return m_file; }
        }

        public string Source
        {
            get { return m_source; }
        }

        public int Line
        {
            get { return m_line; }
        }        

        public ValidationRule()
        {
            m_conditions = new List<Predicate<TType>>();
            m_validators = new List<RuleValidator<TType>>();
        }

        public ValidationResult Validate()
        {
            m_value.ThrowIfNull();
            m_validators.ThrowIfNull();
            m_conditions.ThrowIfNull();      

            var conditionsFullfilled = m_conditions.Aggregate(true,
                                                (current, condition) => 
                                                condition != null && 
                                                current & condition(m_value));

            var result = ValidationResult.Success;
            if (conditionsFullfilled)
                result = m_validators.Aggregate(result, (current, validator) => current & validator.Validate(m_value));

            ThrowErrors();            
            return result;
        }

        public ValidationRule<TType> For(TType value, string name,
                               [CallerFilePath] string file = null,
                               [CallerMemberName] string source = null,
                               [CallerLineNumber] int line = -1)
        {
            m_value = value;
            m_name = name;
            m_file = file;
            m_source = source;
            m_line = line;            
            SetDefaultData();

            return this;
        }

        public ValidationRule<TType> For(Expression<Func<TType>> memberExpression,
                               [CallerFilePath] string file = null,
                               [CallerMemberName] string source = null,
                               [CallerLineNumber] int line = -1)
        {
            m_name = memberExpression.GetMemberName();
            m_value = memberExpression.Compile()();
            m_file = file;
            m_source = source;
            m_line = line;
            SetDefaultData();

            return this;

        }

        public ValidationRule<TType> IsRequired(bool throwError = false)
        {
            m_validators.Add(new RuleValidator<TType>(v => v != null 
                                                      && !v.Equals(default(TType)), 
                                                      ExceptionMessages.CommonIsRequiredFailed));

            return this;
        }

        public ValidationRule<TType> IsNotNull(bool throwError = false)
        {
            m_validators.Add(new RuleValidator<TType>(v => v != null, 
                                                      ExceptionMessages.CommonIsNotNullFailed));                                                

            return this;
        }

        public ValidationRule<TType> IsNotDefault(bool throwError = false)
        {
            m_validators.Add(new RuleValidator<TType>(v => !v.Equals(default(TType)), 
                                                      ExceptionMessages.CommonIsNotNullFailed));

            return this;
        }        

        public static bool IsOfType(object value, bool throwError = false)
        {
            if (value.ContainsType<TType>())
                return true;

            if (!throwError)
                return false;

            var ruleType = typeof(TType).FullName;
            var valueType = value.GetType().FullName;
            throw ExceptionFactory.ArgumentException("",
                                                     ExceptionMessages.TypesIsOfTypeFailed.Inject(ruleType, valueType));
        }

        public static bool IsNotOfType(object value, bool throwError = false)
        {
            if (!value.ContainsType<TType>())
                return true;

            if (!throwError)
                return false;

            var ruleType = typeof(TType).FullName;
            throw ExceptionFactory.ArgumentException("",
                                                     ExceptionMessages.TypesIsNotOfTypeFailed.Inject(ruleType));
        }

        public ValidationRule<TType> AddValidator(RuleValidator<TType> validator)
        {
            validator.ThrowIfNull();

            m_validators.Add(validator);
            return this;
        }

        protected void SetDefaultData()
        {
            m_exceptionData = new ConcurrentDictionary<string, string>();
            m_exceptionData.Add("Value", m_value.ToString());
            m_exceptionData.Add("Name", m_name);
            m_exceptionData.Add("File", m_file);
            m_exceptionData.Add("Source", m_source);
            m_exceptionData.Add("Line", m_line.ToString());
        }

        protected void ThrowErrors()
        {
            m_name.ThrowIfNull();

            if (!m_throwErrors)
                return;

            var exceptions = m_results.ErrorMessages.Select(error =>
                                                                ExceptionFactory.ArgumentException(m_name,
                                                                                                   error,
                                                                                                   m_exceptionData.ToArray()));

            throw ExceptionFactory.ValidationException(m_name, 
                                                       "Validation of {0} Failed.".Inject(m_name), 
                                                       exceptions, m_exceptionData.ToArray());
        }
    }
}