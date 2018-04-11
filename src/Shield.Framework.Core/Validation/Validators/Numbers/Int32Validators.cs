using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class Int32Validators
    {
        public static ValidationRule<int> IsInRange(this ValidationRule<int> rule, int minValue, int maxValue)
        {
            rule.AddValidator(new RuleValidator<int>(v => v > minValue, 
                                                       ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<int>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<int> IsNotInRange(this ValidationRule<int> rule, int minValue, int maxValue)
        {
            rule.AddValidator(new RuleValidator<int>(v => v < minValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<int>(v => v > maxValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<int> IsGreaterThan(this ValidationRule<int> rule, int minValue)
        {
            rule.AddValidator(new RuleValidator<int>(v => v > minValue,
                                                       ExceptionMessages.NumbersIsGtFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<int> IsGreaterThanOrEqual(this ValidationRule<int> rule, int minValue)
        {
            rule.AddValidator(new RuleValidator<int>(v => v >= minValue,
                                                       ExceptionMessages.NumbersIsGteFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<int> IsLessThan(this ValidationRule<int> rule, int maxValue)
        {
            rule.AddValidator(new RuleValidator<int>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsLtFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<int> IsLessThanOrEqual(this ValidationRule<int> rule, int maxValue)
        {
            rule.AddValidator(new RuleValidator<int>(v => v <= maxValue,
                                                       ExceptionMessages.NumbersIsLteFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<int> IsEqualTo(this ValidationRule<int> rule, int expected)
        {
            rule.AddValidator(new RuleValidator<int>(v => v == expected,
                                                       ExceptionMessages.NumbersIsFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<int> IsNotEqualToOrEqual(this ValidationRule<int> rule, int expected)
        {
            rule.AddValidator(new RuleValidator<int>(v => v >= expected,
                                                       ExceptionMessages.NumbersIsNotFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<int> IsPositive(this ValidationRule<int> rule)
        {
            rule.AddValidator(new RuleValidator<int>(v => v > 0,
                                                       ExceptionMessages.NumbersIsPositiveFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<int> IsNegative(this ValidationRule<int> rule)
        {
            rule.AddValidator(new RuleValidator<int>(v => v < 0,
                                                       ExceptionMessages.NumbersIsNegativeFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<int> IsNonZero(this ValidationRule<int> rule)
        {
            rule.AddValidator(new RuleValidator<int>(v => v != 0,
                                                       ExceptionMessages.NumbersIsNonZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<int> IsZero(this ValidationRule<int> rule)
        {
            rule.AddValidator(new RuleValidator<int>(v => v == 0,
                                                       ExceptionMessages.NumbersIsZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<int> IsMaximumValue(this ValidationRule<int> rule)
        {
            rule.AddValidator(new RuleValidator<int>(v => v == int.MaxValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, int.MaxValue)));

            return rule;
        }

        public static ValidationRule<int> IsMinimumValue(this ValidationRule<int> rule)
        {
            rule.AddValidator(new RuleValidator<int>(v => v == int.MinValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, int.MinValue)));

            return rule;
        }

        public static ValidationRule<int> IsNotMaximumValue(this ValidationRule<int> rule)
        {
            rule.AddValidator(new RuleValidator<int>(v => v != int.MaxValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, int.MaxValue)));

            return rule;
        }

        public static ValidationRule<int> IsNotMinimumValue(this ValidationRule<int> rule)
        {
            rule.AddValidator(new RuleValidator<int>(v => v != int.MinValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, int.MinValue)));

            return rule;
        }

        public static ValidationRule<int> IsEven(this ValidationRule<int> rule)
        {
            rule.AddValidator(new RuleValidator<int>(v => v % 2 == 0,
                                                       ExceptionMessages.NumbersIsEvenFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<int> IsOdd(this ValidationRule<int> rule)
        {
            rule.AddValidator(new RuleValidator<int>(v => v % 2 != 0,
                                                       ExceptionMessages.NumbersIsOddFailed.Inject(rule.Value)));

            return rule;
        }
    }
}