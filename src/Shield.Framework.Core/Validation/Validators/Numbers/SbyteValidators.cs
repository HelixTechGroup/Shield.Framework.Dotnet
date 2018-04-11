using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class SByteValidators
    {
        public static ValidationRule<sbyte> IsInRange(this ValidationRule<sbyte> rule, sbyte minValue, sbyte maxValue)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v > minValue, 
                                                       ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<sbyte>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<sbyte> IsNotInRange(this ValidationRule<sbyte> rule, sbyte minValue, sbyte maxValue)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v < minValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<sbyte>(v => v > maxValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<sbyte> IsGreaterThan(this ValidationRule<sbyte> rule, sbyte minValue)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v > minValue,
                                                       ExceptionMessages.NumbersIsGtFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<sbyte> IsGreaterThanOrEqual(this ValidationRule<sbyte> rule, sbyte minValue)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v >= minValue,
                                                       ExceptionMessages.NumbersIsGteFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<sbyte> IsLessThan(this ValidationRule<sbyte> rule, sbyte maxValue)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsLtFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<sbyte> IsLessThanOrEqual(this ValidationRule<sbyte> rule, sbyte maxValue)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v <= maxValue,
                                                       ExceptionMessages.NumbersIsLteFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<sbyte> IsEqualTo(this ValidationRule<sbyte> rule, sbyte expected)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v == expected,
                                                       ExceptionMessages.NumbersIsFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<sbyte> IsNotEqualToOrEqual(this ValidationRule<sbyte> rule, sbyte expected)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v >= expected,
                                                       ExceptionMessages.NumbersIsNotFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<sbyte> IsPositive(this ValidationRule<sbyte> rule)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v > 0,
                                                       ExceptionMessages.NumbersIsPositiveFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<sbyte> IsNegative(this ValidationRule<sbyte> rule)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v < 0,
                                                       ExceptionMessages.NumbersIsNegativeFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<sbyte> IsNonZero(this ValidationRule<sbyte> rule)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v != 0,
                                                       ExceptionMessages.NumbersIsNonZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<sbyte> IsZero(this ValidationRule<sbyte> rule)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v == 0,
                                                       ExceptionMessages.NumbersIsZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<sbyte> IsMaximumValue(this ValidationRule<sbyte> rule)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v == sbyte.MaxValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, sbyte.MaxValue)));

            return rule;
        }

        public static ValidationRule<sbyte> IsMinimumValue(this ValidationRule<sbyte> rule)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v == sbyte.MinValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, sbyte.MinValue)));

            return rule;
        }

        public static ValidationRule<sbyte> IsNotMaximumValue(this ValidationRule<sbyte> rule)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v != sbyte.MaxValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, sbyte.MaxValue)));

            return rule;
        }

        public static ValidationRule<sbyte> IsNotMinimumValue(this ValidationRule<sbyte> rule)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v != sbyte.MinValue,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, sbyte.MinValue)));

            return rule;
        }

        public static ValidationRule<sbyte> IsEven(this ValidationRule<sbyte> rule)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v % 2 == 0,
                                                       ExceptionMessages.NumbersIsEvenFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<sbyte> IsOdd(this ValidationRule<sbyte> rule)
        {
            rule.AddValidator(new RuleValidator<sbyte>(v => v % 2 != 0,
                                                       ExceptionMessages.NumbersIsOddFailed.Inject(rule.Value)));

            return rule;
        }
    }
}