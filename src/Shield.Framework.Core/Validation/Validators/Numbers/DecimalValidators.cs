using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class DecimalValidators
    {
        private const decimal m_comparisionTolerance = 0.00001M;

        public static ValidationRule<decimal> IsInRange(this ValidationRule<decimal> rule, decimal minValue, decimal maxValue)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => v > minValue, 
                                                       ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<decimal>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<decimal> IsNotInRange(this ValidationRule<decimal> rule, decimal minValue, decimal maxValue)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => v < minValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<decimal>(v => v > maxValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<decimal> IsGreaterThan(this ValidationRule<decimal> rule, decimal minValue)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => v > minValue,
                                                       ExceptionMessages.NumbersIsGtFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<decimal> IsGreaterThanOrEqual(this ValidationRule<decimal> rule, decimal minValue)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => v >= minValue,
                                                       ExceptionMessages.NumbersIsGteFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<decimal> IsLessThan(this ValidationRule<decimal> rule, decimal maxValue)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsLtFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<decimal> IsLessThanOrEqual(this ValidationRule<decimal> rule, decimal maxValue)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => v <= maxValue,
                                                       ExceptionMessages.NumbersIsLteFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<decimal> IsEqualTo(this ValidationRule<decimal> rule, decimal expected)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => Math.Abs(v - expected) < m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<decimal> IsNotEqualToOrEqual(this ValidationRule<decimal> rule, decimal expected)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => v >= expected,
                                                       ExceptionMessages.NumbersIsNotFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<decimal> IsPositive(this ValidationRule<decimal> rule)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => v > 0,
                                                       ExceptionMessages.NumbersIsPositiveFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<decimal> IsNegative(this ValidationRule<decimal> rule)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => v < 0,
                                                       ExceptionMessages.NumbersIsNegativeFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<decimal> IsNonZero(this ValidationRule<decimal> rule)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => Math.Abs(v) > 0.0M,
                                                       ExceptionMessages.NumbersIsNonZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<decimal> IsZero(this ValidationRule<decimal> rule)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => Math.Abs(v) < 0.0M,
                                                       ExceptionMessages.NumbersIsZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<decimal> IsMaximumValue(this ValidationRule<decimal> rule)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => Math.Abs(v - decimal.MaxValue) < m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, decimal.MaxValue)));

            return rule;
        }

        public static ValidationRule<decimal> IsMinimumValue(this ValidationRule<decimal> rule)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => Math.Abs(v - decimal.MinValue) < m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, decimal.MinValue)));

            return rule;
        }

        public static ValidationRule<decimal> IsNotMaximumValue(this ValidationRule<decimal> rule)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => Math.Abs(v - decimal.MaxValue) > m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, decimal.MaxValue)));

            return rule;
        }

        public static ValidationRule<decimal> IsNotMinimumValue(this ValidationRule<decimal> rule)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => Math.Abs(v - decimal.MinValue) > m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, decimal.MinValue)));

            return rule;
        }

        public static ValidationRule<decimal> IsEven(this ValidationRule<decimal> rule)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => Math.Abs(v % 2) < m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsEvenFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<decimal> IsOdd(this ValidationRule<decimal> rule)
        {
            rule.AddValidator(new RuleValidator<decimal>(v => Math.Abs(v % 2) > m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsOddFailed.Inject(rule.Value)));

            return rule;
        }        
    }
}