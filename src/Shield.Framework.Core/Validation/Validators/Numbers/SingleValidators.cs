#region Usings
using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class SingleValidators
    {
        #region Members
        private const float m_comparisionTolerance = 0.00001f;
        #endregion

        #region Methods
        public static IValidationTarget<float> IsInRange(this IValidationTarget<float> target, float minValue, float maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<float>(v => v > minValue,
                                                                       ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(
                                                                                                                             target.Value,
                                                                                                                             minValue)))
                         .And(new OutOfRangeValidationPredicate<float>(v => v < maxValue,
                                                                       ExceptionMessages.NumbersIsInRangeTooHighFailed
                                                                                        .Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<float> IsNotInRange(this IValidationTarget<float> target, float minValue, float maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<float>(v => v < minValue,
                                                                       ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(
                                                                                                                                target.Value,
                                                                                                                                minValue)))
                         .And(new OutOfRangeValidationPredicate<float>(v => v > maxValue,
                                                                       ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(
                                                                                                                                 target.Value,
                                                                                                                                 maxValue)));
        }

        public static IValidationTarget<float> IsGreaterThan(this IValidationTarget<float> target, float minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<float>(v => v > minValue,
                                                                       ExceptionMessages.NumbersIsGtFailed.Inject(target.Value, minValue)));
        }

        public static IValidationTarget<float> IsGreaterThanOrEqual(this IValidationTarget<float> target, float minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<float>(v => v >= minValue,
                                                                       ExceptionMessages.NumbersIsGteFailed
                                                                                        .Inject(target.Value, minValue)));
        }

        public static IValidationTarget<float> IsLessThan(this IValidationTarget<float> target, float maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<float>(v => v < maxValue,
                                                                       ExceptionMessages.NumbersIsLtFailed.Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<float> IsLessThanOrEqual(this IValidationTarget<float> target, float maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<float>(v => v <= maxValue,
                                                                       ExceptionMessages.NumbersIsLteFailed
                                                                                        .Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<float> IsEqualTo(this IValidationTarget<float> target, float expected)
        {
            return target.And(new DefaultValidationPredicate<float>(v => Math.Abs(v - expected) < m_comparisionTolerance,
                                                                    ExceptionMessages.NumbersIsFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<float> IsNotEqualToOrEqual(this IValidationTarget<float> target, float expected)
        {
            return target.And(new DefaultValidationPredicate<float>(v => v >= expected,
                                                                    ExceptionMessages.NumbersIsNotFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<float> IsPositive(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(v => v > 0,
                                                                    ExceptionMessages.NumbersIsPositiveFailed.Inject(target.Value)));
        }

        public static IValidationTarget<float> IsNegative(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(v => v < 0,
                                                                    ExceptionMessages.NumbersIsNegativeFailed.Inject(target.Value)));
        }

        public static IValidationTarget<float> IsNonZero(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(v => Math.Abs(v) > 0.0f,
                                                                    ExceptionMessages.NumbersIsNonZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<float> IsZero(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(v => Math.Abs(v) < 0.0f,
                                                                    ExceptionMessages.NumbersIsZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<float> IsMaximumValue(this IValidationTarget<float> target)
        {
            return target.And(new OutOfRangeValidationPredicate<float>(v => Math.Abs(v - float.MaxValue) < m_comparisionTolerance,
                                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                        target.Value,
                                                                                                                        float.MaxValue)));
        }

        public static IValidationTarget<float> IsMinimumValue(this IValidationTarget<float> target)
        {
            return target.And(new OutOfRangeValidationPredicate<float>(v => Math.Abs(v - float.MinValue) < m_comparisionTolerance,
                                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                        target.Value,
                                                                                                                        float.MinValue)));
        }

        public static IValidationTarget<float> IsNotMaximumValue(this IValidationTarget<float> target)
        {
            return target.And(new OutOfRangeValidationPredicate<float>(v => Math.Abs(v - float.MaxValue) > m_comparisionTolerance,
                                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                        target.Value,
                                                                                                                        float.MaxValue)));
        }

        public static IValidationTarget<float> IsNotMinimumValue(this IValidationTarget<float> target)
        {
            return target.And(new OutOfRangeValidationPredicate<float>(v => Math.Abs(v - float.MinValue) > m_comparisionTolerance,
                                                                       ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                        target.Value,
                                                                                                                        float.MinValue)));
        }

        public static IValidationTarget<float> IsEven(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(v => Math.Abs(v % 2) < m_comparisionTolerance,
                                                                    ExceptionMessages.NumbersIsEvenFailed.Inject(target.Value)));
        }

        public static IValidationTarget<float> IsOdd(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(v => Math.Abs(v % 2) > m_comparisionTolerance,
                                                                    ExceptionMessages.NumbersIsOddFailed.Inject(target.Value)));
        }

        public static IValidationTarget<float> IsInfinite(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(float.IsInfinity,
                                                                    ExceptionMessages.NumbersIsInfinityFailed.Inject(target.Value)));
        }

        public static IValidationTarget<float> IsPositiveInfinite(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(float.IsPositiveInfinity,
                                                                    ExceptionMessages.NumbersIsPositiveInfinityFailed
                                                                                     .Inject(target.Value)));
        }

        public static IValidationTarget<float> IsNegativeInfinite(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(float.IsNegativeInfinity,
                                                                    ExceptionMessages.NumbersIsNegativeInfinityFailed
                                                                                     .Inject(target.Value)));
        }

        public static IValidationTarget<float> IsNotInfinite(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(v => !float.IsInfinity(v),
                                                                    ExceptionMessages.NumbersIsNotInfinityFailed.Inject(target.Value)));
        }

        public static IValidationTarget<float> IsNotPositiveInfinite(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(v => !float.IsPositiveInfinity(v),
                                                                    ExceptionMessages.NumbersIsNotPositiveInfinityFailed.Inject(
                                                                                                                                target.Value)));
        }

        public static IValidationTarget<float> IsNotNegativeInfinite(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(v => !float.IsNegativeInfinity(v),
                                                                    ExceptionMessages.NumbersIsNotNegativeInfinityFailed.Inject(
                                                                                                                                target.Value)));
        }

        public static IValidationTarget<float> IsNaN(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(float.IsNaN,
                                                                    ExceptionMessages.NumbersIsNaNFailed.Inject(target.Value)));
        }

        public static IValidationTarget<float> IsNumber(this IValidationTarget<float> target)
        {
            return target.And(new DefaultValidationPredicate<float>(v => !float.IsNaN(v),
                                                                    ExceptionMessages.NumbersIsNumberFailed.Inject(target.Value)));
        }
        #endregion
    }
}