#region Usings
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class SByteValidators
    {
        #region Methods
        public static IValidationTarget<sbyte> IsInRange(this IValidationTarget<sbyte> target, sbyte minValue, sbyte maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<sbyte>(v => v > minValue,
                                                                       ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(
                                                                                                                             target.Value,
                                                                                                                             minValue)))
                         .And(new OutOfRangeValidationPredicate<sbyte>(v => v < maxValue,
                                                                       ExceptionMessages.NumbersIsInRangeTooHighFailed
                                                                                        .Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<sbyte> IsNotInRange(this IValidationTarget<sbyte> target, sbyte minValue, sbyte maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<sbyte>(v => v < minValue,
                                                                       ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(
                                                                                                                                target.Value,
                                                                                                                                minValue)))
                         .And(new OutOfRangeValidationPredicate<sbyte>(v => v > maxValue,
                                                                       ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(
                                                                                                                                 target.Value,
                                                                                                                                 maxValue)));
        }

        public static IValidationTarget<sbyte> IsGreaterThan(this IValidationTarget<sbyte> target, sbyte minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<sbyte>(v => v > minValue,
                                                                       ExceptionMessages.NumbersIsGtFailed.Inject(target.Value, minValue)));
        }

        public static IValidationTarget<sbyte> IsGreaterThanOrEqual(this IValidationTarget<sbyte> target, sbyte minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<sbyte>(v => v >= minValue,
                                                                       ExceptionMessages.NumbersIsGteFailed
                                                                                        .Inject(target.Value, minValue)));
        }

        public static IValidationTarget<sbyte> IsLessThan(this IValidationTarget<sbyte> target, sbyte maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<sbyte>(v => v < maxValue,
                                                                       ExceptionMessages.NumbersIsLtFailed.Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<sbyte> IsLessThanOrEqual(this IValidationTarget<sbyte> target, sbyte maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<sbyte>(v => v <= maxValue,
                                                                       ExceptionMessages.NumbersIsLteFailed
                                                                                        .Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<sbyte> IsEqualTo(this IValidationTarget<sbyte> target, sbyte expected)
        {
            return target.And(new DefaultValidationPredicate<sbyte>(v => v == expected,
                                                                    ExceptionMessages.NumbersIsFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<sbyte> IsNotEqualToOrEqual(this IValidationTarget<sbyte> target, sbyte expected)
        {
            return target.And(new DefaultValidationPredicate<sbyte>(v => v >= expected,
                                                                    ExceptionMessages.NumbersIsNotFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<sbyte> IsPositive(this IValidationTarget<sbyte> target)
        {
            return target.And(new DefaultValidationPredicate<sbyte>(v => v > 0,
                                                                    ExceptionMessages.NumbersIsPositiveFailed.Inject(target.Value)));
        }

        public static IValidationTarget<sbyte> IsNegative(this IValidationTarget<sbyte> target)
        {
            return target.And(new DefaultValidationPredicate<sbyte>(v => v < 0,
                                                                    ExceptionMessages.NumbersIsNegativeFailed.Inject(target.Value)));
        }

        public static IValidationTarget<sbyte> IsNonZero(this IValidationTarget<sbyte> target)
        {
            return target.And(new DefaultValidationPredicate<sbyte>(v => v != 0,
                                                                    ExceptionMessages.NumbersIsNonZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<sbyte> IsZero(this IValidationTarget<sbyte> target)
        {
            return target.And(new DefaultValidationPredicate<sbyte>(v => v == 0,
                                                                    ExceptionMessages.NumbersIsZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<sbyte> IsMaximumValue(this IValidationTarget<sbyte> target)
        {
            return target.And(new DefaultValidationPredicate<sbyte>(v => v == sbyte.MaxValue,
                                                                    ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                     target.Value,
                                                                                                                     sbyte.MaxValue)));
        }

        public static IValidationTarget<sbyte> IsMinimumValue(this IValidationTarget<sbyte> target)
        {
            return target.And(new DefaultValidationPredicate<sbyte>(v => v == sbyte.MinValue,
                                                                    ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                     target.Value,
                                                                                                                     sbyte.MinValue)));
        }

        public static IValidationTarget<sbyte> IsNotMaximumValue(this IValidationTarget<sbyte> target)
        {
            return target.And(new DefaultValidationPredicate<sbyte>(v => v != sbyte.MaxValue,
                                                                    ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                     target.Value,
                                                                                                                     sbyte.MaxValue)));
        }

        public static IValidationTarget<sbyte> IsNotMinimumValue(this IValidationTarget<sbyte> target)
        {
            return target.And(new DefaultValidationPredicate<sbyte>(v => v != sbyte.MinValue,
                                                                    ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                     target.Value,
                                                                                                                     sbyte.MinValue)));
        }

        public static IValidationTarget<sbyte> IsEven(this IValidationTarget<sbyte> target)
        {
            return target.And(new DefaultValidationPredicate<sbyte>(v => v % 2 == 0,
                                                                    ExceptionMessages.NumbersIsEvenFailed.Inject(target.Value)));
        }

        public static IValidationTarget<sbyte> IsOdd(this IValidationTarget<sbyte> target)
        {
            return target.And(new DefaultValidationPredicate<sbyte>(v => v % 2 != 0,
                                                                    ExceptionMessages.NumbersIsOddFailed.Inject(target.Value)));
        }
        #endregion
    }
}