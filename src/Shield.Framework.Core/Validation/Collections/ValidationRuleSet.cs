#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using Shield.Framework.Collections;
using Shield.Framework.Exceptions;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
#endregion

namespace Shield.Framework.Validation.Collections
{
    internal sealed class ValidationRuleSet<TType> : ConcurrentList<IValidationRule<TType>>
    {
        #region Members
        private readonly IValidationTarget<TType> m_target;
        #endregion

        #region Properties
        public IValidationRule<TType> CurrentRule
        {
            get
            {
                if (this.IsEmpty())
                    Add(new ValidationRule<TType>(m_target));

                return this[Count - 1];
            }
        }
        #endregion

        public ValidationRuleSet(IValidationTarget<TType> target)
        {
            target.ThrowIfNull();

            m_target = target;
        }

        #region Methods
        public void WithMessage(string message)
        {
            message.ThrowIfNull();
            foreach (var rule in this)
                rule.WithMessage(message);
        }

        public void WithException<TException>() where TException : Exception, new()
        {
            foreach (var rule in this)
                rule.WithException<TException>();
        }

        public ValidationResult Validate()
        {
            var results = ValidationResult.Success;
            foreach (var rule in this)
            {
                results &= rule.Validate();
                if (!results.IsValid)
                    break;
            }

            return results;
        }

        public Exception Throw()
        {
            return this.Select(rule => rule.Throw()).FirstOrDefault(ex => ex != null);
        }

        public TException Throw<TException>(string message = null) where TException : Exception, new()
        {
            return this.Select(rule => rule.Throw<TException>()).FirstOrDefault(ex => ex != null);
        }

        public ValidationException ThrowAll()
        {
            var exceptions = new List<Exception>();
            foreach (var rule in this)
            {
                var ex = rule.ThrowAll();
                if (ex != null)
                    exceptions.AddRange(ex.InnerExceptions);
            }

            if (exceptions.IsEmpty())
                return null;

            return ExceptionFactory.ValidationException(m_target.Name,
                                                        "Validation of {0} Failed.".Inject(m_target.Name),
                                                        exceptions,
                                                        m_target.ExceptionData);
        }
        #endregion
    }
}