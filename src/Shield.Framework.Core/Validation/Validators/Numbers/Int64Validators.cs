using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class Int64Validators
    {
        public static ValidationRule<long> IsInRange(this ValidationRule<long> rule, long minValue, long maxValue)
        {
            rule.AddValidator(new RuleValidator<long>(v => v > minValue, 
                                                       ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<long>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<long> IsNotInRange(this ValidationRule<long> rule, long minValue, long maxValue)
        {
            rule.AddValidator(new RuleValidator<long>(v => v < minValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<long>(v => v > maxValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<long> IsGreaterThan(this ValidationRule<long> rule, long minValue)
        {
            rule.AddValidator(new RuleValidator<long>(v => v > minValue,
                                                       ExceptionMessages.NumbersIsGtFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<long> IsGreaterThanOrEqual(this ValidationRule<long> rule, long minValue)
        {
            rule.AddValidator(new RuleValidator<long>(v => v >= minValue,
                                                       ExceptionMessages.NumbersIsGteFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<long> IsLessThan(this ValidationRule<long> rule, long maxValue)
        {
            rule.AddValidator(new RuleValidator<long>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsLtFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<long> IsLessThanOrEqual(this ValidationRule<long> rule, long maxValue)
        {
            rule.AddValidator(new RuleValidator<long>(v => v <= maxValue,
                                                       ExceptionMessages.NumbersIsLteFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<long> IsEqualTo(this ValidationRule<long> rule, long expected)
        {
            rule.AddValidator(new RuleValidator<long>(v => v == expected,
                                                       ExceptionMessages.NumbersIsFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<long> IsNotEqualToOrEqual(this ValidationRule<long> rule, long expected)
        {
            rule.AddValidator(new RuleValidator<long>(v => v >= expected,
                                                       ExceptionMessages.NumbersIsNotFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<long> IsPositive(this ValidationRule<long> rule)
        {
            rule.AddValidator(new RuleValidator<long>(v => v > 0,
                                                       ExceptionMessages.NumbersIsPositiveFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<long> IsNegative(this ValidationRule<long> rule)
        {
            rule.AddValidator(new RuleValidator<long>(v => v < 0,
                                                       ExceptionMessages.NumbersIsNegativeFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<long> IsNonZero(this ValidationRule<long> rule)
        {
            rule.AddValidator(new RuleValidator<long>(v => v != 0,
                                                       ExceptionMessages.NumbersIsNonZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<long> IsZero(this ValidationRule<long> rule)
        {
            rule.AddValidator(new RuleValidator<long>(v => v == 0,
                                                       ExceptionMessages.NumbersIsZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<long> IsMaximumValue(this ValidationRule<long> rule)
        {
            rule.AddValidator(new RuleValidator<long>(v => v == long.MaxValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, long.MaxValue)));

            return rule;
        }

        public static ValidationRule<long> IsMinimumValue(this ValidationRule<long> rule)
        {
            rule.AddValidator(new RuleValidator<long>(v => v == long.MinValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, long.MinValue)));

            return rule;
        }

        public static ValidationRule<long> IsNotMaximumValue(this ValidationRule<long> rule)
        {
            rule.AddValidator(new RuleValidator<long>(v => v != long.MaxValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, long.MaxValue)));

            return rule;
        }

        public static ValidationRule<long> IsNotMinimumValue(this ValidationRule<long> rule)
        {
            rule.AddValidator(new RuleValidator<long>(v => v != long.MinValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, long.MinValue)));

            return rule;
        }

        public static ValidationRule<long> IsEven(this ValidationRule<long> rule)
        {
            rule.AddValidator(new RuleValidator<long>(v => v % 2 == 0,
                                                       ExceptionMessages.NumbersIsEvenFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<long> IsOdd(this ValidationRule<long> rule)
        {
            rule.AddValidator(new RuleValidator<long>(v => v % 2 != 0,
                                                       ExceptionMessages.NumbersIsOddFailed.Inject(rule.Value)));

            return rule;
        }
    }
}