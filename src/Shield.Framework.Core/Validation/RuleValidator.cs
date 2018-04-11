using System;
using System.Collections.Generic;
using System.Text;
using Shield.Framework.Exceptions;
using Shield.Framework.Extensions;

namespace Shield.Framework.Validation
{
    public class RuleValidator<TType>
    {
        private readonly Predicate<TType> m_predicate;
        private string m_message = "Validation predicate does not satisfy the rule.";

        public RuleValidator(Predicate<TType> predicate)
        {
            predicate.ThrowIfNull();
            m_predicate = predicate;
        }

        public RuleValidator(Predicate<TType> predicate, string message)
        {
            predicate.ThrowIfNull();
            m_predicate = predicate;
            m_message = message;
        }

        public ValidationResult Validate(TType value)
        {
            var result = ValidationResult.Success;
            return m_predicate(value) ? ValidationResult.Success : ValidationResult.Fail(new[] { m_message });
        }

        public RuleValidator<TType> WithMessage(string message)
        {
            message.ThrowIfNull();
            m_message = message;

            return this;
        }
    }
}
