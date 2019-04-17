#region Usings
using System;
using System.Linq;
using Shield.Framework.Collections;
using Shield.Framework.Exceptions;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
#endregion

namespace Shield.Framework.Validation.Collections
{
    internal sealed class ValidationPredicateSet<TType> : ConcurrentList<IValidationPredicate<TType>>
    {
        #region Members
        private readonly IValidationRule<TType> m_rule;
        #endregion

        public ValidationPredicateSet(IValidationRule<TType> rule)
        {
            rule.ThrowIfNull();

            m_rule = rule;
        }

        #region Methods
        public override void Add(IValidationPredicate<TType> item)
        {
            item.ThrowIfNull();

            if (item.Rule is null || item.Rule != m_rule)
                item.WithRule(m_rule);

            base.Add(item);
        }

        public void Add<TException>(Predicate<TType> predicate, string message = null) where TException : Exception, new()
        {
            Add(new ValidationPredicate<TType, TException>(m_rule, predicate, message));
        }

        public void WithException<TException>() where TException : Exception, new()
        {
            foreach (var predicate in this)
                predicate.WithException<TException>();
        }

        public void WithMessage(string message)
        {
            message.ThrowIfNull();
            foreach (var predicate in this)
                predicate.WithMessage(message);
        }

        public Exception Throw()
        {
            return (this.Where(validator => !validator.Validate())
                        .Select(GetExceptionFromPredicate)).FirstOrDefault();
        }

        public TException Throw<TException>(string message = null) where TException : Exception, new()
        {
            if (!string.IsNullOrEmpty(message))
                WithMessage(message);

            return (this.Where(validator => !validator.Validate())
                        .Select(validator => ExceptionProvider.GenerateArgumentException<TException>(m_rule.Target.Name,
                                                                                                    validator.ErrorMessage,
                                                                                                    m_rule.Target.ExceptionData)))
                .FirstOrDefault();
        }

        public ValidationException ThrowAll()
        {
            var exceptions = this.Where(predicate => !predicate.Validate())
                                 .Select(error =>
                                             ExceptionProvider
                                                 .ArgumentException(
                                                                    m_rule.Target.Name,
                                                                    error.ErrorMessage,
                                                                    m_rule.Target
                                                                          .ExceptionData));
            if (exceptions.IsEmpty())
                return null;

            return ExceptionProvider.ValidationException(m_rule.Target.Name,
                                                        "Validation of {0} Failed.".Inject(m_rule.Target.Name),
                                                        exceptions,
                                                        m_rule.Target.ExceptionData);
        }

        public ValidationResult Validate()
        {
            var results = ValidationResult.Success;
            results = this.Aggregate(results,
                                     (current, validator) =>
                                         current
                                         & (validator.Validate()
                                                ? ValidationResult.Success
                                                : ValidationResult.Fail(validator.ErrorMessage)));

            return results;
        }

        private Exception GetExceptionFromPredicate(IValidationPredicate<TType> predicate)
        {
            return predicate.GenerateException(m_rule.Target.Name, m_rule.Target.ExceptionData);
        }
        #endregion
    }
}