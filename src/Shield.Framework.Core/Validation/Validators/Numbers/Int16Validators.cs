#region Usings
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class Int16Validators
    {
        #region Methods
        public static IValidationTarget<short> IsInRange(this IValidationTarget<short> target, short minValue, short maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<short>(v => v > minValue,
                                                                       ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(
                                                                                                                             target.Value,
                                                                                                                             minValue)))
                         .And(new OutOfRangeValidationPredicate<short>(v => v < maxValue,
                                                                       ExceptionMessages.NumbersIsInRangeTooHighFailed
                                                                                        .Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<short> IsNotInRange(this IValidationTarget<short> target, short minValue, short maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<short>(v => v < minValue,
                                                                       ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(
                                                                                                                                target.Value,
                                                                                                                                minValue)))
                         .And(new OutOfRangeValidationPredicate<short>(v => v > maxValue,
                                                                       ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(
                                                                                                                                 target.Value,
                                                                                                                                 maxValue)));
        }

        public static IValidationTarget<short> IsGreaterThan(this IValidationTarget<short> target, short minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<short>(v => v > minValue,
                                                                       ExceptionMessages.NumbersIsGtFailed.Inject(target.Value, minValue)));
        }

        public static IValidationTarget<short> IsGreaterThanOrEqual(this IValidationTarget<short> target, short minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<short>(v => v >= minValue,
                                                                       ExceptionMessages.NumbersIsGteFailed
                                                                                        .Inject(target.Value, minValue)));
        }

        public static IValidationTarget<short> IsLessThan(this IValidationTarget<short> target, short maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<short>(v => v < maxValue,
                                                                       ExceptionMessages.NumbersIsLtFailed.Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<short> IsLessThanOrEqual(this IValidationTarget<short> target, short maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<short>(v => v <= maxValue,
                                                                       ExceptionMessages.NumbersIsLteFailed
                                                                                        .Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<short> IsEqualTo(this IValidationTarget<short> target, short expected)
        {
            return target.And(new DefaultValidationPredicate<short>(v => v == expected,
                                                                    ExceptionMessages.NumbersIsFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<short> IsNotEqualTo(this IValidationTarget<short> target, short expected)
        {
            return target.And(new DefaultValidationPredicate<short>(v => v >= expected,
                                                                    ExceptionMessages.NumbersIsNotFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<short> IsPositive(this IValidationTarget<short> target)
        {
            return target.And(new DefaultValidationPredicate<short>(v => v > 0,
                                                                    ExceptionMessages.NumbersIsPositiveFailed.Inject(target.Value)));
        }

        public static IValidationTarget<short> IsNegative(this IValidationTarget<short> target)
        {
            return target.And(new DefaultValidationPredicate<short>(v => v < 0,
                                                                    ExceptionMessages.NumbersIsNegativeFailed.Inject(target.Value)));
        }

        public static IValidationTarget<short> IsNonZero(this IValidationTarget<short> target)
        {
            return target.And(new DefaultValidationPredicate<short>(v => v != 0,
                                                                    ExceptionMessages.NumbersIsNonZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<short> IsZero(this IValidationTarget<short> target)
        {
            return target.And(new DefaultValidationPredicate<short>(v => v == 0,
                                                                    ExceptionMessages.NumbersIsZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<short> IsMaximumValue(this IValidationTarget<short> target)
        {
            return target.And(new DefaultValidationPredicate<short>(v => v == short.MaxValue,
                                                                    ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                     target.Value,
                                                                                                                     short.MaxValue)));
        }

        public static IValidationTarget<short> IsMinimumValue(this IValidationTarget<short> target)
        {
            return target.And(new DefaultValidationPredicate<short>(v => v == short.MinValue,
                                                                    ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                     target.Value,
                                                                                                                     short.MinValue)));
        }

        public static IValidationTarget<short> IsNotMaximumValue(this IValidationTarget<short> target)
        {
            return target.And(new DefaultValidationPredicate<short>(v => v != short.MaxValue,
                                                                    ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                     target.Value,
                                                                                                                     short.MaxValue)));
        }

        public static IValidationTarget<short> IsNotMinimumValue(this IValidationTarget<short> target)
        {
            return target.And(new DefaultValidationPredicate<short>(v => v != short.MinValue,
                                                                    ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                     target.Value,
                                                                                                                     short.MinValue)));
        }

        public static IValidationTarget<short> IsEven(this IValidationTarget<short> target)
        {
            return target.And(new DefaultValidationPredicate<short>(v => v % 2 == 0,
                                                                    ExceptionMessages.NumbersIsEvenFailed.Inject(target.Value)));
        }

        public static IValidationTarget<short> IsOdd(this IValidationTarget<short> target)
        {
            return target.And(new DefaultValidationPredicate<short>(v => v % 2 != 0,
                                                                    ExceptionMessages.NumbersIsOddFailed.Inject(target.Value)));
        }
        #endregion
    }
}