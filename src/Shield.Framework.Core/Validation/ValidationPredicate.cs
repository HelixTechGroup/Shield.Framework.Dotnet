#region Usings
using System;
using System.Collections.Generic;
using Shield.Framework.Exceptions;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
#endregion

namespace Shield.Framework.Validation
{
    public class ValidationPredicate<TType, TException> : IValidationPredicate<TType> where TException : Exception, new()
    {
        #region Members
        private readonly Predicate<TType> m_predicate;
        private string m_message = "Validation predicate does not satisfy the rule.";
        private IValidationRule<TType> m_rule;
        #endregion

        #region Properties
        public string ErrorMessage
        {
            get { return m_message; }
        }

        public Type ExceptionType
        {
            get { return typeof(TException); }
        }

        public IValidationRule<TType> Rule
        {
            get { return m_rule; }
        }
        #endregion

        public ValidationPredicate(Predicate<TType> predicate)
        {
            predicate.ThrowIfNull();

            m_rule = null;
            m_predicate = predicate;
        }

        public ValidationPredicate(Predicate<TType> predicate, string message) : this(predicate)
        {
            message.ThrowIfNull();
            m_message = message;
        }

        internal ValidationPredicate(IValidationRule<TType> rule, Predicate<TType> predicate)
        {
            rule.ThrowIfNull();
            predicate.ThrowIfNull();

            m_rule = rule;
            m_predicate = predicate;
        }

        internal ValidationPredicate(IValidationRule<TType> rule, Predicate<TType> predicate, string message) : this(rule, predicate)
        {
            message.ThrowIfNull();
            m_message = message;
        }

        #region Methods
        public ValidationResult Validate()
        {
            m_rule.ThrowIfNull();
            return !m_predicate(m_rule.Target.Value) ? ValidationResult.Fail(m_message) : ValidationResult.Success;
        }

        public IValidationPredicate<TType> WithRule(IValidationRule<TType> rule)
        {
            m_rule.ThrowIfNull();

            m_rule = rule;
            return this;
        }

        public IValidate<TType> WithMessage(string message)
        {
            message.ThrowIfNull();
            m_message = message;

            return this;
        }

        public ValidationExceptionSelector<TType> WithException()
        {
            return new ValidationExceptionSelector<TType>(this);
        }

        public IValidate<TType> WithException<TNewException>() where TNewException : Exception, new()
        {
            return new ValidationPredicate<TType, TNewException>(m_rule, m_predicate, m_message);
        }

        public Exception GenerateException(string name, KeyValuePair<string, object>[] exceptionData)
        {
            return ExceptionFactory.GenerateArgumentException<TException>(name, m_message, exceptionData);
        }
        #endregion
    }
}