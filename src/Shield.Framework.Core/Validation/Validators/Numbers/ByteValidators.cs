#region Usings
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators.Numbers
{
    public static class ByteValidators
    {
        #region Methods
        public static IValidationTarget<byte> IsInRange(this IValidationTarget<byte> target, byte minValue, byte maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<byte>(v => v > minValue,
                                                                      ExceptionMessages.NumbersIsInRangeTooLowFailed.Inject(
                                                                                                                            target.Value,
                                                                                                                            minValue)))
                         .And(new OutOfRangeValidationPredicate<byte>(v => v < maxValue,
                                                                      ExceptionMessages.NumbersIsInRangeTooHighFailed
                                                                                       .Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<byte> IsNotInRange(this IValidationTarget<byte> target, byte minValue, byte maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<byte>(v => v < minValue,
                                                                      ExceptionMessages.NumbersIsNotInRangeTooLowFailed.Inject(
                                                                                                                               target.Value,
                                                                                                                               minValue)))
                         .And(new OutOfRangeValidationPredicate<byte>(v => v > maxValue,
                                                                      ExceptionMessages.NumbersIsNotInRangeTooHighFailed.Inject(
                                                                                                                                target.Value,
                                                                                                                                maxValue)));
        }

        public static IValidationTarget<byte> IsGreaterThan(this IValidationTarget<byte> target, byte minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<byte>(v => v > minValue,
                                                                      ExceptionMessages.NumbersIsGtFailed.Inject(target.Value, minValue)));
        }

        public static IValidationTarget<byte> IsGreaterThanOrEqual(this IValidationTarget<byte> target, byte minValue)
        {
            return target.And(new OutOfRangeValidationPredicate<byte>(v => v >= minValue,
                                                                      ExceptionMessages.NumbersIsGteFailed.Inject(target.Value, minValue)));
        }

        public static IValidationTarget<byte> IsLessThan(this IValidationTarget<byte> target, byte maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<byte>(v => v < maxValue,
                                                                      ExceptionMessages.NumbersIsLtFailed.Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<byte> IsLessThanOrEqual(this IValidationTarget<byte> target, byte maxValue)
        {
            return target.And(new OutOfRangeValidationPredicate<byte>(v => v <= maxValue,
                                                                      ExceptionMessages.NumbersIsLteFailed.Inject(target.Value, maxValue)));
        }

        public static IValidationTarget<byte> IsEqualTo(this IValidationTarget<byte> target, byte expected)
        {
            return target.And(new DefaultValidationPredicate<byte>(v => v == expected,
                                                                   ExceptionMessages.NumbersIsFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<byte> IsNotEqualTo(this IValidationTarget<byte> target, byte expected)
        {
            return target.And(new DefaultValidationPredicate<byte>(v => v >= expected,
                                                                   ExceptionMessages.NumbersIsNotFailed.Inject(target.Value, expected)));
        }

        public static IValidationTarget<byte> IsNonZero(this IValidationTarget<byte> target)
        {
            return target.And(new DefaultValidationPredicate<byte>(v => v != 0,
                                                                   ExceptionMessages.NumbersIsNonZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<byte> IsZero(this IValidationTarget<byte> target)
        {
            return target.And(new DefaultValidationPredicate<byte>(v => v == 0,
                                                                   ExceptionMessages.NumbersIsZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<byte> IsMaximumValue(this IValidationTarget<byte> target)
        {
            return target.And(new DefaultValidationPredicate<byte>(v => v == byte.MaxValue,
                                                                   ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                    target.Value,
                                                                                                                    byte.MaxValue)));
        }

        public static IValidationTarget<byte> IsMinimumValue(this IValidationTarget<byte> target)
        {
            return target.And(new DefaultValidationPredicate<byte>(v => v == byte.MinValue,
                                                                   ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                    target.Value,
                                                                                                                    byte.MinValue)));
        }

        public static IValidationTarget<byte> IsNotMaximumValue(this IValidationTarget<byte> target)
        {
            return target.And(new DefaultValidationPredicate<byte>(v => v != byte.MaxValue,
                                                                   ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                    target.Value,
                                                                                                                    byte.MaxValue)));
        }

        public static IValidationTarget<byte> IsNotMinimumValue(this IValidationTarget<byte> target)
        {
            return target.And(new DefaultValidationPredicate<byte>(v => v != byte.MinValue,
                                                                   ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                    target.Value,
                                                                                                                    byte.MinValue)));
        }

        public static IValidationTarget<byte> IsEven(this IValidationTarget<byte> target)
        {
            return target.And(new DefaultValidationPredicate<byte>(v => v % 2 == 0,
                                                                   ExceptionMessages.NumbersIsEvenFailed.Inject(target.Value)));
        }

        public static IValidationTarget<byte> IsOdd(this IValidationTarget<byte> target)
        {
            return target.And(new DefaultValidationPredicate<byte>(v => v % 2 != 0,
                                                                   ExceptionMessages.NumbersIsOddFailed.Inject(target.Value)));
        }
        #endregion
    }
}