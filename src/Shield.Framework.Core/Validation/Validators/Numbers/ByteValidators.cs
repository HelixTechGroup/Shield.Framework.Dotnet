using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class ByteValidators
    {
        public static ValidationRule<byte> IsInRange(this ValidationRule<byte> rule, byte minValue, byte maxValue)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v > minValue, 
                                                       ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<byte>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<byte> IsNotInRange(this ValidationRule<byte> rule, byte minValue, byte maxValue)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v < minValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<byte>(v => v > maxValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<byte> IsGreaterThan(this ValidationRule<byte> rule, byte minValue)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v > minValue,
                                                       ExceptionMessages.NumbersIsGtFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<byte> IsGreaterThanOrEqual(this ValidationRule<byte> rule, byte minValue)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v >= minValue,
                                                       ExceptionMessages.NumbersIsGteFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<byte> IsLessThan(this ValidationRule<byte> rule, byte maxValue)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsLtFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<byte> IsLessThanOrEqual(this ValidationRule<byte> rule, byte maxValue)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v <= maxValue,
                                                       ExceptionMessages.NumbersIsLteFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<byte> IsEqualTo(this ValidationRule<byte> rule, byte expected)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v == expected,
                                                       ExceptionMessages.NumbersIsFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<byte> IsNotEqualToOrEqual(this ValidationRule<byte> rule, byte expected)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v >= expected,
                                                       ExceptionMessages.NumbersIsNotFailed.Inject(rule.Value, expected)));

            return rule;
        }        

        public static ValidationRule<byte> IsNonZero(this ValidationRule<byte> rule)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v != 0,
                                                       ExceptionMessages.NumbersIsNonZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<byte> IsZero(this ValidationRule<byte> rule)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v == 0,
                                                       ExceptionMessages.NumbersIsZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<byte> IsMaximumValue(this ValidationRule<byte> rule)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v == byte.MaxValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, byte.MaxValue)));

            return rule;
        }

        public static ValidationRule<byte> IsMinimumValue(this ValidationRule<byte> rule)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v == byte.MinValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, byte.MinValue)));

            return rule;
        }

        public static ValidationRule<byte> IsNotMaximumValue(this ValidationRule<byte> rule)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v != byte.MaxValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, byte.MaxValue)));

            return rule;
        }

        public static ValidationRule<byte> IsNotMinimumValue(this ValidationRule<byte> rule)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v != byte.MinValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, byte.MinValue)));

            return rule;
        }

        public static ValidationRule<byte> IsEven(this ValidationRule<byte> rule)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v % 2 == 0,
                                                       ExceptionMessages.NumbersIsEvenFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<byte> IsOdd(this ValidationRule<byte> rule)
        {
            rule.AddValidator(new RuleValidator<byte>(v => v % 2 != 0,
                                                       ExceptionMessages.NumbersIsOddFailed.Inject(rule.Value)));

            return rule;
        }
    }
}