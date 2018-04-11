using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class SingleValidators
    {
        private const float m_comparisionTolerance = 0.00001f;

        public static ValidationRule<float> IsInRange(this ValidationRule<float> rule, float minValue, float maxValue)
        {
            rule.AddValidator(new RuleValidator<float>(v => v > minValue, 
                                                       ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<float>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<float> IsNotInRange(this ValidationRule<float> rule, float minValue, float maxValue)
        {
            rule.AddValidator(new RuleValidator<float>(v => v < minValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(rule.Value, minValue)));

            rule.AddValidator(new RuleValidator<float>(v => v > maxValue,
                                                       ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<float> IsGreaterThan(this ValidationRule<float> rule, float minValue)
        {
            rule.AddValidator(new RuleValidator<float>(v => v > minValue,
                                                       ExceptionMessages.NumbersIsGtFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<float> IsGreaterThanOrEqual(this ValidationRule<float> rule, float minValue)
        {
            rule.AddValidator(new RuleValidator<float>(v => v >= minValue,
                                                       ExceptionMessages.NumbersIsGteFailed.Inject(rule.Value, minValue)));

            return rule;
        }

        public static ValidationRule<float> IsLessThan(this ValidationRule<float> rule, float maxValue)
        {
            rule.AddValidator(new RuleValidator<float>(v => v < maxValue,
                                                       ExceptionMessages.NumbersIsLtFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<float> IsLessThanOrEqual(this ValidationRule<float> rule, float maxValue)
        {
            rule.AddValidator(new RuleValidator<float>(v => v <= maxValue,
                                                       ExceptionMessages.NumbersIsLteFailed.Inject(rule.Value, maxValue)));

            return rule;
        }

        public static ValidationRule<float> IsEqualTo(this ValidationRule<float> rule, float expected)
        {
            rule.AddValidator(new RuleValidator<float>(v => Math.Abs(v - expected) < m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<float> IsNotEqualToOrEqual(this ValidationRule<float> rule, float expected)
        {
            rule.AddValidator(new RuleValidator<float>(v => v >= expected,
                                                       ExceptionMessages.NumbersIsNotFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<float> IsPositive(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => v > 0,
                                                       ExceptionMessages.NumbersIsPositiveFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<float> IsNegative(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => v < 0,
                                                       ExceptionMessages.NumbersIsNegativeFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<float> IsNonZero(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => Math.Abs(v) > 0.0f,
                                                       ExceptionMessages.NumbersIsNonZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<float> IsZero(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => Math.Abs(v) < 0.0f,
                                                       ExceptionMessages.NumbersIsZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<float> IsMaximumValue(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => Math.Abs(v - float.MaxValue) < m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, float.MaxValue)));

            return rule;
        }

        public static ValidationRule<float> IsMinimumValue(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => Math.Abs(v - float.MinValue) < m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, float.MinValue)));

            return rule;
        }

        public static ValidationRule<float> IsNotMaximumValue(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => Math.Abs(v - float.MaxValue) > m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, float.MaxValue)));

            return rule;
        }

        public static ValidationRule<float> IsNotMinimumValue(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => Math.Abs(v - float.MinValue) > m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, float.MinValue)));

            return rule;
        }

        public static ValidationRule<float> IsEven(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => Math.Abs(v % 2) < m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsEvenFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<float> IsOdd(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => Math.Abs(v % 2) > m_comparisionTolerance,
                                                       ExceptionMessages.NumbersIsOddFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<float> IsInfinite(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(float.IsInfinity,
                                                       ExceptionMessages.NumbersIsInfinityFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<float> IsPositiveInfinite(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(float.IsPositiveInfinity,
                                                       ExceptionMessages.NumbersIsPositiveInfinityFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<float> IsNegativeInfinite(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(float.IsNegativeInfinity,
                                                       ExceptionMessages.NumbersIsNegativeInfinityFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<float> IsNotInfinite(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => !float.IsInfinity(v),
                                                       ExceptionMessages.NumbersIsNotInfinityFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<float> IsNotPositiveInfinite(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => !float.IsPositiveInfinity(v),
                                                       ExceptionMessages.NumbersIsNotPositiveInfinityFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<float> IsNotNegativeInfinite(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => !float.IsNegativeInfinity(v),
                                                       ExceptionMessages.NumbersIsNotNegativeInfinityFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<float> IsNaN(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(float.IsNaN,
                                                       ExceptionMessages.NumbersIsNaNFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<float> IsNumber(this ValidationRule<float> rule)
        {
            rule.AddValidator(new RuleValidator<float>(v => !float.IsNaN(v),
                                                       ExceptionMessages.NumbersIsNumberFailed.Inject(rule.Value)));

            return rule;
        }
    }
}