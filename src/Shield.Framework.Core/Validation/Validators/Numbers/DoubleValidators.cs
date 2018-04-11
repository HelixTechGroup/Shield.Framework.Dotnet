using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class DoubleValidators
    {
        private const double m_comparisionTolerance = 0.00001f;

        public static ValidationRule<double> IsInRange(this ValidationRule<double> rule, double minValue, double maxValue)
        {
            rule.AddValidator(new RuleValidator<double>(v => v > minValue, 
                                                       ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<double>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<double> IsNotInRange(this ValidationRule<double> rule, double minValue, double maxValue)
        {
            rule.AddValidator(new RuleValidator<double>(v => v < minValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<double>(v => v > maxValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<double> IsGreaterThan(this ValidationRule<double> rule, double minValue)
        {
            rule.AddValidator(new RuleValidator<double>(v => v > minValue,
                                                       ExceptionMessages.NumbersIsGtFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<double> IsGreaterThanOrEqual(this ValidationRule<double> rule, double minValue)
        {
            rule.AddValidator(new RuleValidator<double>(v => v >= minValue,
                                                       ExceptionMessages.NumbersIsGteFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<double> IsLessThan(this ValidationRule<double> rule, double maxValue)
        {
            rule.AddValidator(new RuleValidator<double>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsLtFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<double> IsLessThanOrEqual(this ValidationRule<double> rule, double maxValue)
        {
            rule.AddValidator(new RuleValidator<double>(v => v <= maxValue,
                                                       ExceptionMessages.NumbersIsLteFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<double> IsEqualTo(this ValidationRule<double> rule, double expected)
        {
            rule.AddValidator(new RuleValidator<double>(v => Math.Abs(v - expected) < m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<double> IsNotEqualToOrEqual(this ValidationRule<double> rule, double expected)
        {
            rule.AddValidator(new RuleValidator<double>(v => v >= expected,
                                                       ExceptionMessages.NumbersIsNotFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<double> IsPositive(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => v > 0,
                                                       ExceptionMessages.NumbersIsPositiveFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<double> IsNegative(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => v < 0,
                                                       ExceptionMessages.NumbersIsNegativeFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<double> IsNonZero(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => Math.Abs(v) > 0.0f,
                                                       ExceptionMessages.NumbersIsNonZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<double> IsZero(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => Math.Abs(v) < 0.0f,
                                                       ExceptionMessages.NumbersIsZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<double> IsMaximumValue(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => Math.Abs(v - double.MaxValue) < m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, double.MaxValue)));

            return rule;
        }

        public static ValidationRule<double> IsMinimumValue(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => Math.Abs(v - double.MinValue) < m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, double.MinValue)));

            return rule;
        }

        public static ValidationRule<double> IsNotMaximumValue(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => Math.Abs(v - double.MaxValue) > m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, double.MaxValue)));

            return rule;
        }

        public static ValidationRule<double> IsNotMinimumValue(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => Math.Abs(v - double.MinValue) > m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, double.MinValue)));

            return rule;
        }

        public static ValidationRule<double> IsEven(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => Math.Abs(v % 2) < m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsEvenFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<double> IsOdd(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => Math.Abs(v % 2) > m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsOddFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<double> IsInfinite(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(double.IsInfinity,
                                                       ExceptionMessages.NumbersIsInfinityFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<double> IsPositiveInfinite(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(double.IsPositiveInfinity,
                                                       ExceptionMessages.NumbersIsPositiveInfinityFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<double> IsNegativeInfinite(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(double.IsNegativeInfinity,
                                                       ExceptionMessages.NumbersIsNegativeInfinityFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<double> IsNotInfinite(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => !double.IsInfinity(v),
                                                       ExceptionMessages.NumbersIsNotInfinityFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<double> IsNotPositiveInfinite(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => !double.IsPositiveInfinity(v),
                                                       ExceptionMessages.NumbersIsNotPositiveInfinityFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<double> IsNotNegativeInfinite(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => !double.IsNegativeInfinity(v),
                                                       ExceptionMessages.NumbersIsNotNegativeInfinityFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<double> IsNaN(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(double.IsNaN,
                                                       ExceptionMessages.NumbersIsNaNFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<double> IsNumber(this ValidationRule<double> rule)
        {
            rule.AddValidator(new RuleValidator<double>(v => !double.IsNaN(v),
                                                       ExceptionMessages.NumbersIsNumberFailed.Inject(rule.Value)));

            return rule;
        }
    }
}