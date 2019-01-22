#region Usings
using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class DoubleValidators
    {
        #region Members
        private const double m_comparisionTolerance = 0.00001f;
        #endregion

        #region Methods
        public static IValidationTarget<double> IsInRange(this IValidationTarget<double> target, double minValue, double maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<double>(v => v > minValue,
                                                                        ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(
                                                                                                                              target.Value,
                                                                                                                              minValue)))
                         .And(new OutOfRangeValidationPredicate<double>(v => v < maxValue,
                                                                        ExceptionMessages.NumbersIsInRangeTooHighFailed.Inject(
                                                                                                                               target.Value,
                                                                                                                               maxValue)));
        }

        public static IValidationTarget<double> IsNotInRange(this IValidationTarget<double> target, double minValue, double maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<double>(v => v < minValue,
                                                                        ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(
                                                                                                                                 target.Value,
                                                                                                                                 minValue)))
                         .And(new OutOfRangeValidationPredicate<double>(v => v > maxValue,
                                                                        ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(
                                                                                                                                  target.Value,
                                                                                                                                  maxValue)));
        }

        public static IValidationTarget<double> IsGreaterThan(this IValidationTarget<double> target, double minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<double>(v => v > minValue,
                                                                        ExceptionMessages.NumbersIsGtFailed
                                                                                         .Inject(target.Value, minValue)));
        }

        public static IValidationTarget<double> IsGreaterThanOrEqual(this IValidationTarget<double> target, double minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<double>(v => v >= minValue,
                                                                        ExceptionMessages.NumbersIsGteFailed
                                                                                         .Inject(target.Value, minValue)));
        }

        public static IValidationTarget<double> IsLessThan(this IValidationTarget<double> target, double maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<double>(v => v < maxValue,
                                                                        ExceptionMessages.NumbersIsLtFailed
                                                                                         .Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<double> IsLessThanOrEqual(this IValidationTarget<double> target, double maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<double>(v => v <= maxValue,
                                                                        ExceptionMessages.NumbersIsLteFailed
                                                                                         .Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<double> IsEqualTo(this IValidationTarget<double> target, double expected)
        {
            return target.And(new DefaultValidationPredicate<double>(v => Math.Abs(v - expected) < m_comparisionTolerance,
                                                                     ExceptionMessages.NumbersIsFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<double> IsNotEqualTo(this IValidationTarget<double> target, double expected)
        {
            return target.And(new DefaultValidationPredicate<double>(v => v >= expected,
                                                                     ExceptionMessages.NumbersIsNotFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<double> IsPositive(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => v > 0,
                                                                     ExceptionMessages.NumbersIsPositiveFailed.Inject(target.Value)));
        }

        public static IValidationTarget<double> IsNegative(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => v < 0,
                                                                     ExceptionMessages.NumbersIsNegativeFailed.Inject(target.Value)));
        }

        public static IValidationTarget<double> IsNonZero(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => Math.Abs(v) > 0.0f,
                                                                     ExceptionMessages.NumbersIsNonZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<double> IsZero(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => Math.Abs(v) < 0.0f,
                                                                     ExceptionMessages.NumbersIsZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<double> IsMaximumValue(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => Math.Abs(v - double.MaxValue) < m_comparisionTolerance,
                                                                     ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                      target.Value,
                                                                                                                      double.MaxValue)));
        }

        public static IValidationTarget<double> IsMinimumValue(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => Math.Abs(v - double.MinValue) < m_comparisionTolerance,
                                                                     ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                      target.Value,
                                                                                                                      double.MinValue)));
        }

        public static IValidationTarget<double> IsNotMaximumValue(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => Math.Abs(v - double.MaxValue) > m_comparisionTolerance,
                                                                     ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                      target.Value,
                                                                                                                      double.MaxValue)));
        }

        public static IValidationTarget<double> IsNotMinimumValue(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => Math.Abs(v - double.MinValue) > m_comparisionTolerance,
                                                                     ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                      target.Value,
                                                                                                                      double.MinValue)));
        }

        public static IValidationTarget<double> IsEven(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => Math.Abs(v % 2) < m_comparisionTolerance,
                                                                     ExceptionMessages.NumbersIsEvenFailed.Inject(target.Value)));
        }

        public static IValidationTarget<double> IsOdd(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => Math.Abs(v % 2) > m_comparisionTolerance,
                                                                     ExceptionMessages.NumbersIsOddFailed.Inject(target.Value)));
        }

        public static IValidationTarget<double> IsInfinite(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(double.IsInfinity,
                                                                     ExceptionMessages.NumbersIsInfinityFailed.Inject(target.Value)));
        }

        public static IValidationTarget<double> IsPositiveInfinite(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(double.IsPositiveInfinity,
                                                                     ExceptionMessages.NumbersIsPositiveInfinityFailed
                                                                                      .Inject(target.Value)));
        }

        public static IValidationTarget<double> IsNegativeInfinite(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(double.IsNegativeInfinity,
                                                                     ExceptionMessages.NumbersIsNegativeInfinityFailed
                                                                                      .Inject(target.Value)));
        }

        public static IValidationTarget<double> IsNotInfinite(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => !double.IsInfinity(v),
                                                                     ExceptionMessages.NumbersIsNotInfinityFailed.Inject(target.Value)));
        }

        public static IValidationTarget<double> IsNotPositiveInfinite(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => !double.IsPositiveInfinity(v),
                                                                     ExceptionMessages.NumbersIsNotPositiveInfinityFailed.Inject(
                                                                                                                                 target.Value)));
        }

        public static IValidationTarget<double> IsNotNegativeInfinite(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => !double.IsNegativeInfinity(v),
                                                                     ExceptionMessages.NumbersIsNotNegativeInfinityFailed.Inject(
                                                                                                                                 target.Value)));
        }

        public static IValidationTarget<double> IsNaN(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(double.IsNaN,
                                                                     ExceptionMessages.NumbersIsNaNFailed.Inject(target.Value)));
        }

        public static IValidationTarget<double> IsNumber(this IValidationTarget<double> target)
        {
            return target.And(new DefaultValidationPredicate<double>(v => !double.IsNaN(v),
                                                                     ExceptionMessages.NumbersIsNumberFailed.Inject(target.Value)));
        }
        #endregion
    }
}