#region Usings
using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class DecimalValidators
    {
        #region Members
        private const decimal m_comparisionTolerance = 0.00001M;
        #endregion

        #region Methods
        public static IValidationTarget<decimal> IsInRange(this IValidationTarget<decimal> target, decimal minValue, decimal maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<decimal>(v => v > minValue,
                                                                         ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(
                                                                                                                               target.Value,
                                                                                                                               minValue)))
                         .And(new OutOfRangeValidationPredicate<decimal>(v => v < maxValue,
                                                                         ExceptionMessages.NumbersIsInRangeTooHighFailed.Inject(
                                                                                                                                target.Value,
                                                                                                                                maxValue)));
        }

        public static IValidationTarget<decimal> IsNotInRange(this IValidationTarget<decimal> target, decimal minValue, decimal maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<decimal>(v => v < minValue,
                                                                         ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(
                                                                                                                                  target.Value,
                                                                                                                                  minValue)))
                         .And(new OutOfRangeValidationPredicate<decimal>(v => v > maxValue,
                                                                         ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(
                                                                                                                                   target.Value,
                                                                                                                                   maxValue)));
        }

        public static IValidationTarget<decimal> IsGreaterThan(this IValidationTarget<decimal> target, decimal minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<decimal>(v => v > minValue,
                                                                         ExceptionMessages.NumbersIsGtFailed
                                                                                          .Inject(target.Value, minValue)));
        }

        public static IValidationTarget<decimal> IsGreaterThanOrEqual(this IValidationTarget<decimal> target, decimal minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<decimal>(v => v >= minValue,
                                                                         ExceptionMessages.NumbersIsGteFailed
                                                                                          .Inject(target.Value, minValue)));
        }

        public static IValidationTarget<decimal> IsLessThan(this IValidationTarget<decimal> target, decimal maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<decimal>(v => v < maxValue,
                                                                         ExceptionMessages.NumbersIsLtFailed
                                                                                          .Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<decimal> IsLessThanOrEqual(this IValidationTarget<decimal> target, decimal maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<decimal>(v => v <= maxValue,
                                                                         ExceptionMessages.NumbersIsLteFailed
                                                                                          .Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<decimal> IsEqualTo(this IValidationTarget<decimal> target, decimal expected)
        {
            return target.And(new DefaultValidationPredicate<decimal>(v => Math.Abs(v - expected) < m_comparisionTolerance,
                                                                      ExceptionMessages.NumbersIsFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<decimal> IsNotEqualTo(this IValidationTarget<decimal> target, decimal expected)
        {
            return target.And(new DefaultValidationPredicate<decimal>(v => v >= expected,
                                                                      ExceptionMessages.NumbersIsNotFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<decimal> IsPositive(this IValidationTarget<decimal> target)
        {
            return target.And(new DefaultValidationPredicate<decimal>(v => v > 0,
                                                                      ExceptionMessages.NumbersIsPositiveFailed.Inject(target.Value)));
        }

        public static IValidationTarget<decimal> IsNegative(this IValidationTarget<decimal> target)
        {
            return target.And(new DefaultValidationPredicate<decimal>(v => v < 0,
                                                                      ExceptionMessages.NumbersIsNegativeFailed.Inject(target.Value)));
        }

        public static IValidationTarget<decimal> IsNonZero(this IValidationTarget<decimal> target)
        {
            return target.And(new DefaultValidationPredicate<decimal>(v => Math.Abs(v) > 0.0M,
                                                                      ExceptionMessages.NumbersIsNonZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<decimal> IsZero(this IValidationTarget<decimal> target)
        {
            return target.And(new DefaultValidationPredicate<decimal>(v => Math.Abs(v) < 0.0M,
                                                                      ExceptionMessages.NumbersIsZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<decimal> IsMaximumValue(this IValidationTarget<decimal> target)
        {
            return target.And(new DefaultValidationPredicate<decimal>(v => Math.Abs(v - decimal.MaxValue) < m_comparisionTolerance,
                                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                       target.Value,
                                                                                                                       decimal.MaxValue)));
        }

        public static IValidationTarget<decimal> IsMinimumValue(this IValidationTarget<decimal> target)
        {
            return target.And(new DefaultValidationPredicate<decimal>(v => Math.Abs(v - decimal.MinValue) < m_comparisionTolerance,
                                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                       target.Value,
                                                                                                                       decimal.MinValue)));
        }

        public static IValidationTarget<decimal> IsNotMaximumValue(this IValidationTarget<decimal> target)
        {
            return target.And(new DefaultValidationPredicate<decimal>(v => Math.Abs(v - decimal.MaxValue) > m_comparisionTolerance,
                                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                       target.Value,
                                                                                                                       decimal.MaxValue)));
        }

        public static IValidationTarget<decimal> IsNotMinimumValue(this IValidationTarget<decimal> target)
        {
            return target.And(new DefaultValidationPredicate<decimal>(v => Math.Abs(v - decimal.MinValue) > m_comparisionTolerance,
                                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                       target.Value,
                                                                                                                       decimal.MinValue)));
        }

        public static IValidationTarget<decimal> IsEven(this IValidationTarget<decimal> target)
        {
            return target.And(new DefaultValidationPredicate<decimal>(v => Math.Abs(v % 2) < m_comparisionTolerance,
                                                                      ExceptionMessages.NumbersIsEvenFailed.Inject(target.Value)));
        }

        public static IValidationTarget<decimal> IsOdd(this IValidationTarget<decimal> target)
        {
            return target.And(new DefaultValidationPredicate<decimal>(v => Math.Abs(v % 2) > m_comparisionTolerance,
                                                                      ExceptionMessages.NumbersIsOddFailed.Inject(target.Value)));
        }
        #endregion
    }
}