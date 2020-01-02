#region Usings
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class Int64Validators
    {
        #region Methods
        public static IValidationTarget<long> IsInRange(this IValidationTarget<long> target, long minValue, long maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<long>(v => v > minValue,
                                                                      ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(
                                                                                                                            target.Value,
                                                                                                                            minValue)))
                         .And(new OutOfRangeValidationPredicate<long>(v => v < maxValue,
                                                                      ExceptionMessages.NumbersIsInRangeTooHighFailed
                                                                                       .Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<long> IsNotInRange(this IValidationTarget<long> target, long minValue, long maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<long>(v => v < minValue,
                                                                      ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(
                                                                                                                               target.Value,
                                                                                                                               minValue)))
                         .And(new OutOfRangeValidationPredicate<long>(v => v > maxValue,
                                                                      ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(
                                                                                                                                target.Value,
                                                                                                                                maxValue)));
        }

        public static IValidationTarget<long> IsGreaterThan(this IValidationTarget<long> target, long minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<long>(v => v > minValue,
                                                                      ExceptionMessages.NumbersIsGtFailed.Inject(target.Value, minValue)));
        }

        public static IValidationTarget<long> IsGreaterThanOrEqual(this IValidationTarget<long> target, long minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<long>(v => v >= minValue,
                                                                      ExceptionMessages.NumbersIsGteFailed.Inject(target.Value, minValue)));
        }

        public static IValidationTarget<long> IsLessThan(this IValidationTarget<long> target, long maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<long>(v => v < maxValue,
                                                                      ExceptionMessages.NumbersIsLtFailed.Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<long> IsLessThanOrEqual(this IValidationTarget<long> target, long maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<long>(v => v <= maxValue,
                                                                      ExceptionMessages.NumbersIsLteFailed.Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<long> IsEqualTo(this IValidationTarget<long> target, long expected)
        {
            return target.And(new DefaultValidationPredicate<long>(v => v == expected,
                                                                   ExceptionMessages.NumbersIsFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<long> IsNotEqualToOrEqual(this IValidationTarget<long> target, long expected)
        {
            return target.And(new DefaultValidationPredicate<long>(v => v >= expected,
                                                                   ExceptionMessages.NumbersIsNotFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<long> IsPositive(this IValidationTarget<long> target)
        {
            return target.And(new DefaultValidationPredicate<long>(v => v > 0,
                                                                   ExceptionMessages.NumbersIsPositiveFailed.Inject(target.Value)));
        }

        public static IValidationTarget<long> IsNegative(this IValidationTarget<long> target)
        {
            return target.And(new DefaultValidationPredicate<long>(v => v < 0,
                                                                   ExceptionMessages.NumbersIsNegativeFailed.Inject(target.Value)));
        }

        public static IValidationTarget<long> IsNonZero(this IValidationTarget<long> target)
        {
            return target.And(new DefaultValidationPredicate<long>(v => v != 0,
                                                                   ExceptionMessages.NumbersIsNonZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<long> IsZero(this IValidationTarget<long> target)
        {
            return target.And(new DefaultValidationPredicate<long>(v => v == 0,
                                                                   ExceptionMessages.NumbersIsZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<long> IsMaximumValue(this IValidationTarget<long> target)
        {
            return target.And(new DefaultValidationPredicate<long>(v => v == long.MaxValue,
                                                                   ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                    target.Value,
                                                                                                                    long.MaxValue)));
        }

        public static IValidationTarget<long> IsMinimumValue(this IValidationTarget<long> target)
        {
            return target.And(new DefaultValidationPredicate<long>(v => v == long.MinValue,
                                                                   ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                    target.Value,
                                                                                                                    long.MinValue)));
        }

        public static IValidationTarget<long> IsNotMaximumValue(this IValidationTarget<long> target)
        {
            return target.And(new DefaultValidationPredicate<long>(v => v != long.MaxValue,
                                                                   ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                    target.Value,
                                                                                                                    long.MaxValue)));
        }

        public static IValidationTarget<long> IsNotMinimumValue(this IValidationTarget<long> target)
        {
            return target.And(new DefaultValidationPredicate<long>(v => v != long.MinValue,
                                                                   ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                    target.Value,
                                                                                                                    long.MinValue)));
        }

        public static IValidationTarget<long> IsEven(this IValidationTarget<long> target)
        {
            return target.And(new DefaultValidationPredicate<long>(v => v % 2 == 0,
                                                                   ExceptionMessages.NumbersIsEvenFailed.Inject(target.Value)));
        }

        public static IValidationTarget<long> IsOdd(this IValidationTarget<long> target)
        {
            return target.And(new DefaultValidationPredicate<long>(v => v % 2 != 0,
                                                                   ExceptionMessages.NumbersIsOddFailed.Inject(target.Value)));
        }
        #endregion
    }
}