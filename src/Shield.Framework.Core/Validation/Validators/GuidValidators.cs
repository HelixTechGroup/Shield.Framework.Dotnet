using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators
{
    public static class GuidValidators
    {

        public static ValidationRule<Guid> IsEmpty(this ValidationRule<Guid> rule)
        {
            rule.AddValidator(new RuleValidator<Guid>(v => v.Equals(Guid.Empty),
                                                      ExceptionMessages.GuidsIsEmptyFailed));

            return rule;
        }

        public static ValidationRule<Guid> IsNotEmpty(this ValidationRule<Guid> rule)
        {
            rule.AddValidator(new RuleValidator<Guid>(v => !v.Equals(Guid.Empty), 
                                                      ExceptionMessages.GuidsIsNotEmptyFailed));

            return rule;
        }

        public static ValidationRule<Guid> IsEqual(this ValidationRule<Guid> rule, Guid guid)
        {
            rule.AddValidator(new RuleValidator<Guid>(v => v.Equals(guid),
                                                      ExceptionMessages.GuidsIsEqualToFailed.Inject(rule.Value, guid)));

            return rule;
        }

        public static ValidationRule<Guid> IsNotEqual(this ValidationRule<Guid> rule, Guid guid)
        {
            rule.AddValidator(new RuleValidator<Guid>(v => !v.Equals(guid),
                                                      ExceptionMessages.GuidsIsNotEqualToFailed.Inject(rule.Value, guid)));

            return rule;
        }
    }
}