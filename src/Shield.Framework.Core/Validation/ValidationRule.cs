#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using Shield.Framework.Collections;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Collections;
using Shield.Framework.Validation.Exceptions;
#endregion

namespace Shield.Framework.Validation
{
    public sealed class ValidationRule<TType> : IValidationRule<TType>
    {
        #region Members
        private readonly IList<Predicate<TType>> m_conditions;
        private readonly ValidationPredicateSet<TType> m_predicates;
        private readonly IValidationTarget<TType> m_target;
        #endregion

        #region Properties
        public IEnumerable<IValidationPredicate<TType>> Predicates
        {
            get { return m_predicates; }
        }

        public IValidationTarget<TType> Target
        {
            get { return m_target; }
        }
        #endregion

        public ValidationRule(IValidationTarget<TType> target)
        {
            target.ThrowIfNull();

            m_target = target;
            m_conditions = new ConcurrentList<Predicate<TType>>();
            m_predicates = new ValidationPredicateSet<TType>(this);
        }

        #region Methods
        private static bool CheckConditions(IValidationTarget<TType> target, IEnumerable<Predicate<TType>> conditions)
        {
            return conditions.Aggregate(true,
                                        (current, condition) =>
                                            condition != null && current && condition(target.Value));
        }

        public Exception Throw()
        {
            return !CheckConditions(m_target, m_conditions) ? null : m_predicates.Throw();
        }

        public TException Throw<TException>(string message = null) where TException : Exception, new()
        {
            return !CheckConditions(m_target, m_conditions) ? null : m_predicates.Throw<TException>();
        }

        public ValidationException ThrowAll()
        {
            return !CheckConditions(m_target, m_conditions) ? null : m_predicates.ThrowAll();
        }

        public IValidationRule<TType> AddPredicate(IValidationPredicate<TType> predicate)
        {
            predicate.ThrowIfNull();

            m_predicates.Add(predicate);
            return this;
        }

        public ValidationExceptionSelector<TType> WithException()
        {
            return new ValidationExceptionSelector<TType>(this);
        }

        public IValidate<TType> WithException<TNewException>() where TNewException : Exception, new()
        {
            m_predicates.WithException<TNewException>();
            return this;
        }

        public IValidate<TType> WithMessage(string message)
        {
            message.ThrowIfNull();

            m_predicates.WithMessage(message);
            return this;
        }

        public ValidationResult Validate()
        {
            return CheckConditions(m_target, m_conditions) ? m_predicates.Validate() : ValidationResult.Success;
        }
        #endregion
    }
}