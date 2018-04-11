using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class Int16Validators
    {
        public static ValidationRule<short> IsInRange(this ValidationRule<short> rule, short minValue, short maxValue)
        {
            rule.AddValidator(new RuleValidator<short>(v => v > minValue, 
                                                       ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<short>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<short> IsNotInRange(this ValidationRule<short> rule, short minValue, short maxValue)
        {
            rule.AddValidator(new RuleValidator<short>(v => v < minValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<short>(v => v > maxValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<short> IsGreaterThan(this ValidationRule<short> rule, short minValue)
        {
            rule.AddValidator(new RuleValidator<short>(v => v > minValue,
                                                       ExceptionMessages.NumbersIsGtFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<short> IsGreaterThanOrEqual(this ValidationRule<short> rule, short minValue)
        {
            rule.AddValidator(new RuleValidator<short>(v => v >= minValue,
                                                       ExceptionMessages.NumbersIsGteFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<short> IsLessThan(this ValidationRule<short> rule, short maxValue)
        {
            rule.AddValidator(new RuleValidator<short>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsLtFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<short> IsLessThanOrEqual(this ValidationRule<short> rule, short maxValue)
        {
            rule.AddValidator(new RuleValidator<short>(v => v <= maxValue,
                                                       ExceptionMessages.NumbersIsLteFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<short> IsEqualTo(this ValidationRule<short> rule, short expected)
        {
            rule.AddValidator(new RuleValidator<short>(v => v == expected,
                                                       ExceptionMessages.NumbersIsFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<short> IsNotEqualToOrEqual(this ValidationRule<short> rule, short expected)
        {
            rule.AddValidator(new RuleValidator<short>(v => v >= expected,
                                                       ExceptionMessages.NumbersIsNotFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<short> IsPositive(this ValidationRule<short> rule)
        {
            rule.AddValidator(new RuleValidator<short>(v => v > 0,
                                                       ExceptionMessages.NumbersIsPositiveFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<short> IsNegative(this ValidationRule<short> rule)
        {
            rule.AddValidator(new RuleValidator<short>(v => v < 0,
                                                       ExceptionMessages.NumbersIsNegativeFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<short> IsNonZero(this ValidationRule<short> rule)
        {
            rule.AddValidator(new RuleValidator<short>(v => v != 0,
                                                       ExceptionMessages.NumbersIsNonZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<short> IsZero(this ValidationRule<short> rule)
        {
            rule.AddValidator(new RuleValidator<short>(v => v == 0,
                                                       ExceptionMessages.NumbersIsZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<short> IsMaximumValue(this ValidationRule<short> rule)
        {
            rule.AddValidator(new RuleValidator<short>(v => v == short.MaxValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, short.MaxValue)));

            return rule;
        }

        public static ValidationRule<short> IsMinimumValue(this ValidationRule<short> rule)
        {
            rule.AddValidator(new RuleValidator<short>(v => v == short.MinValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, short.MinValue)));

            return rule;
        }

        public static ValidationRule<short> IsNotMaximumValue(this ValidationRule<short> rule)
        {
            rule.AddValidator(new RuleValidator<short>(v => v != short.MaxValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, short.MaxValue)));

            return rule;
        }

        public static ValidationRule<short> IsNotMinimumValue(this ValidationRule<short> rule)
        {
            rule.AddValidator(new RuleValidator<short>(v => v != short.MinValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, short.MinValue)));

            return rule;
        }

        public static ValidationRule<short> IsEven(this ValidationRule<short> rule)
        {
            rule.AddValidator(new RuleValidator<short>(v => v % 2 == 0,
                                                       ExceptionMessages.NumbersIsEvenFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<short> IsOdd(this ValidationRule<short> rule)
        {
            rule.AddValidator(new RuleValidator<short>(v => v % 2 != 0,
                                                       ExceptionMessages.NumbersIsOddFailed.Inject(rule.Value)));

            return rule;
        }
    }
}