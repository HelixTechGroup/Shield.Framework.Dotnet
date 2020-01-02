#region Usings
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class Int32Validators
    {
        #region Methods
        public static IValidationTarget<int> IsInRange(this IValidationTarget<int> target, int minValue, int maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<int>(v => v > minValue,
                                                                     ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(
                                                                                                                           target.Value,
                                                                                                                           minValue)))
                         .And(new OutOfRangeValidationPredicate<int>(v => v < maxValue,
                                                                     ExceptionMessages.NumbersIsInRangeTooHighFailed.Inject(
                                                                                                                            target.Value,
                                                                                                                            maxValue)));
        }

        public static IValidationTarget<int> IsNotInRange(this IValidationTarget<int> target, int minValue, int maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<int>(v => v < minValue,
                                                                     ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(
                                                                                                                              target.Value,
                                                                                                                              minValue)))
                         .And(new OutOfRangeValidationPredicate<int>(v => v > maxValue,
                                                                     ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(
                                                                                                                               target.Value,
                                                                                                                               maxValue)));
        }

        public static IValidationTarget<int> IsGreaterThan(this IValidationTarget<int> target, int minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<int>(v => v > minValue,
                                                                     ExceptionMessages.NumbersIsGtFailed.Inject(target.Value, minValue)));
        }

        public static IValidationTarget<int> IsGreaterThanOrEqual(this IValidationTarget<int> target, int minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<int>(v => v >= minValue,
                                                                     ExceptionMessages.NumbersIsGteFailed.Inject(target.Value, minValue)));
        }

        public static IValidationTarget<int> IsLessThan(this IValidationTarget<int> target, int maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<int>(v => v < maxValue,
                                                                     ExceptionMessages.NumbersIsLtFailed.Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<int> IsLessThanOrEqual(this IValidationTarget<int> target, int maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<int>(v => v <= maxValue,
                                                                     ExceptionMessages.NumbersIsLteFailed.Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<int> IsEqualTo(this IValidationTarget<int> target, int expected)
        {
            return target.And(new DefaultValidationPredicate<int>(v => v == expected,
                                                                  ExceptionMessages.NumbersIsFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<int> IsNotEqualToOrEqual(this IValidationTarget<int> target, int expected)
        {
            return target.And(new DefaultValidationPredicate<int>(v => v >= expected,
                                                                  ExceptionMessages.NumbersIsNotFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<int> IsPositive(this IValidationTarget<int> target)
        {
            return target.And(new DefaultValidationPredicate<int>(v => v > 0,
                                                                  ExceptionMessages.NumbersIsPositiveFailed.Inject(target.Value)));
        }

        public static IValidationTarget<int> IsNegative(this IValidationTarget<int> target)
        {
            return target.And(new DefaultValidationPredicate<int>(v => v < 0,
                                                                  ExceptionMessages.NumbersIsNegativeFailed.Inject(target.Value)));
        }

        public static IValidationTarget<int> IsNonZero(this IValidationTarget<int> target)
        {
            return target.And(new DefaultValidationPredicate<int>(v => v != 0,
                                                                  ExceptionMessages.NumbersIsNonZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<int> IsZero(this IValidationTarget<int> target)
        {
            return target.And(new DefaultValidationPredicate<int>(v => v == 0,
                                                                  ExceptionMessages.NumbersIsZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<int> IsMaximumValue(this IValidationTarget<int> target)
        {
            return target.And(new DefaultValidationPredicate<int>(v => v == int.MaxValue,
                                                                  ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                   target.Value,
                                                                                                                   int.MaxValue)));
        }

        public static IValidationTarget<int> IsMinimumValue(this IValidationTarget<int> target)
        {
            return target.And(new DefaultValidationPredicate<int>(v => v == int.MinValue,
                                                                  ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                   target.Value,
                                                                                                                   int.MinValue)));
        }

        public static IValidationTarget<int> IsNotMaximumValue(this IValidationTarget<int> target)
        {
            return target.And(new DefaultValidationPredicate<int>(v => v != int.MaxValue,
                                                                  ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                   target.Value,
                                                                                                                   int.MaxValue)));
        }

        public static IValidationTarget<int> IsNotMinimumValue(this IValidationTarget<int> target)
        {
            return target.And(new DefaultValidationPredicate<int>(v => v != int.MinValue,
                                                                  ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                   target.Value,
                                                                                                                   int.MinValue)));
        }

        public static IValidationTarget<int> IsEven(this IValidationTarget<int> target)
        {
            return target.And(new DefaultValidationPredicate<int>(v => v % 2 == 0,
                                                                  ExceptionMessages.NumbersIsEvenFailed.Inject(target.Value)));
        }

        public static IValidationTarget<int> IsOdd(this IValidationTarget<int> target)
        {
            return target.And(new DefaultValidationPredicate<int>(v => v % 2 != 0,
                                                                  ExceptionMessages.NumbersIsOddFailed.Inject(target.Value)));
        }
        #endregion
    }
}