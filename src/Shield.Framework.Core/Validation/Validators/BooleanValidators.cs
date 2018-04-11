using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators
{
    public static class BooleanValidators
    {
        public static ValidationRule<bool> IsTrue(this ValidationRule<bool> rule)
        {
            rule.AddValidator(new RuleValidator<bool>(v => v, 
                                                      ExceptionMessages.BooleansIsTrueFailed));

            return rule;
        }

        public static ValidationRule<bool> IsFalse(this ValidationRule<bool> rule)
        {
            rule.AddValidator(new RuleValidator<bool>(v => !v, 
                                                      ExceptionMessages.BooleansIsFalseFailed));

            return rule;
        }
    }
}