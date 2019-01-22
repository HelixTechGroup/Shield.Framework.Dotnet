using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;

namespace Shield.Framework.Validation.Validators.Time
{
    public static class DateTimeOffsetOffsetValidators
    {
        #region Methods
        public static IValidationTarget<DateTimeOffset> IsAfter(this IValidationTarget<DateTimeOffset> target, DateTimeOffset offset)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v > offset,
                                                                      ExceptionMessages.DateTimeIsAfterFailed
                                                                          .Inject(target.Value, offset)));
        }

        public static IValidationTarget<DateTimeOffset> IsAfterNow(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v > DateTimeOffset.Now,
                                                                      ExceptionMessages.DateTimeIsAfterFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.Now)));
        }

        public static IValidationTarget<DateTimeOffset> IsAfterUtcNow(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v > DateTimeOffset.UtcNow,
                                                                      ExceptionMessages.DateTimeIsAfterFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.UtcNow)));
        }

        public static IValidationTarget<DateTimeOffset> IsAfterToday(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v.Date > DateTimeOffset.Now.Date,
                                                                      ExceptionMessages.DateTimeIsAfterFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.Now.Date)));
        }

        public static IValidationTarget<DateTimeOffset> IsAfterOrSame(this IValidationTarget<DateTimeOffset> target, DateTimeOffset offset)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v >= offset,
                                                                      ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(
                                                                          target.Value,
                                                                          offset)));
        }

        public static IValidationTarget<DateTimeOffset> IsAfterOrSameAsNow(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v >= DateTimeOffset.Now,
                                                                      ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.Now)));
        }

        public static IValidationTarget<DateTimeOffset> IsAfterOrSameAsUtcNow(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v >= DateTimeOffset.UtcNow,
                                                                      ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.UtcNow)));
        }

        public static IValidationTarget<DateTimeOffset> IsAfterOrSameAsToday(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v.Date >= DateTimeOffset.Now.Date,
                                                                      ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.Now.Date)));
        }

        public static IValidationTarget<DateTimeOffset> IsBefore(this IValidationTarget<DateTimeOffset> target, DateTimeOffset offset)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v < offset,
                                                                      ExceptionMessages.DateTimeIsBeforeFailed
                                                                          .Inject(target.Value, offset)));
        }

        public static IValidationTarget<DateTimeOffset> IsBeforeNow(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v < DateTimeOffset.Now,
                                                                      ExceptionMessages.DateTimeIsBeforeFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.Now)));
        }

        public static IValidationTarget<DateTimeOffset> IsBeforeUtcNow(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v < DateTimeOffset.UtcNow,
                                                                      ExceptionMessages.DateTimeIsBeforeFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.UtcNow)));
        }

        public static IValidationTarget<DateTimeOffset> IsBeforeToday(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v.Date < DateTimeOffset.Now.Date,
                                                                      ExceptionMessages.DateTimeIsBeforeFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.Now.Date)));
        }

        public static IValidationTarget<DateTimeOffset> IsBeforeOrSame(this IValidationTarget<DateTimeOffset> target, DateTimeOffset offset)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v <= offset,
                                                                      ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(
                                                                          target.Value,
                                                                          offset)));
        }

        public static IValidationTarget<DateTimeOffset> IsBeforeOrSameAsNow(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v >= DateTimeOffset.Now,
                                                                      ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.Now)));
        }

        public static IValidationTarget<DateTimeOffset> IsBeforeOrSameAsUtcNow(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v >= DateTimeOffset.UtcNow,
                                                                      ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.UtcNow)));
        }

        public static IValidationTarget<DateTimeOffset> IsBeforeOrSameAsToday(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v.Date >= DateTimeOffset.Now.Date,
                                                                      ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.Now.Date)));
        }

        public static IValidationTarget<DateTimeOffset> IsEqual(this IValidationTarget<DateTimeOffset> target, DateTimeOffset offset)
        {
            return target.And(new DefaultValidationPredicate<DateTimeOffset>(v => v == offset,
                                                                      ExceptionMessages.DateTimeIsSameAsFailed
                                                                          .Inject(target.Value, offset)));
        }

        public static IValidationTarget<DateTimeOffset> IsEqualToNow(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new DefaultValidationPredicate<DateTimeOffset>(v => v == DateTimeOffset.Now,
                                                                      ExceptionMessages.DateTimeIsSameAsFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.Now)));
        }

        public static IValidationTarget<DateTimeOffset> IsEqualToUtcNow(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new DefaultValidationPredicate<DateTimeOffset>(v => v == DateTimeOffset.UtcNow,
                                                                      ExceptionMessages.DateTimeIsSameAsFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.UtcNow)));
        }

        public static IValidationTarget<DateTimeOffset> IsEqualToToday(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new DefaultValidationPredicate<DateTimeOffset>(v => v.Date == DateTimeOffset.Now.Date,
                                                                      ExceptionMessages.DateTimeIsSameAsFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.Now.Date)));
        }

        public static IValidationTarget<DateTimeOffset> IsNotEqual(this IValidationTarget<DateTimeOffset> target, DateTimeOffset offset)
        {
            return target.And(new DefaultValidationPredicate<DateTimeOffset>(v => v != offset,
                                                                      ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(
                                                                          target.Value,
                                                                          offset)));
        }

        public static IValidationTarget<DateTimeOffset> IsNotEqualToNow(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new DefaultValidationPredicate<DateTimeOffset>(v => v != DateTimeOffset.Now,
                                                                      ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.Now)));
        }

        public static IValidationTarget<DateTimeOffset> IsNotEqualToUtcNow(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new DefaultValidationPredicate<DateTimeOffset>(v => v != DateTimeOffset.UtcNow,
                                                                      ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.UtcNow)));
        }

        public static IValidationTarget<DateTimeOffset> IsNotEqualToToday(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new DefaultValidationPredicate<DateTimeOffset>(v => v.Date != DateTimeOffset.Now.Date,
                                                                      ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.Now.Date)));
        }

        public static IValidationTarget<DateTimeOffset> IsMaximumValue(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v == DateTimeOffset.MaxValue,
                                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.MaxValue)));
        }

        public static IValidationTarget<DateTimeOffset> IsMinimumValue(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v == DateTimeOffset.MinValue,
                                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.MinValue)));
        }

        public static IValidationTarget<DateTimeOffset> IsNotMaximumValue(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v != DateTimeOffset.MaxValue,
                                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.MaxValue)));
        }

        public static IValidationTarget<DateTimeOffset> IsNotMinimumValue(this IValidationTarget<DateTimeOffset> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v != DateTimeOffset.MinValue,
                                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                          target.Value,
                                                                          DateTimeOffset.MinValue)));
        }

        public static IValidationTarget<DateTimeOffset> HasRangeBetween(this IValidationTarget<DateTimeOffset> target,
                                                                       DateTimeOffset minLength,
                                                                       DateTimeOffset maxLength)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v > minLength,
                                                                      ExceptionMessages.DateTimeHasRangeBetweenFailedTooEarly.Inject(
                                                                          minLength,
                                                                          maxLength,
                                                                          target.Value)))
                .And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v < maxLength,
                                                             ExceptionMessages.DateTimeHasRangeBetweenFailedTooLate.Inject(
                                                                 minLength,
                                                                 maxLength,
                                                                 target.Value)));
        }

        public static IValidationTarget<DateTimeOffset> HasRangeNotBetween(this IValidationTarget<DateTimeOffset> target,
                                                                          DateTimeOffset minLength,
                                                                          DateTimeOffset maxLength)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v < minLength,
                                                                      ExceptionMessages.DateTimeHasRangeNotBetweenFailedTooEarly.Inject(
                                                                          minLength,
                                                                          maxLength,
                                                                          target.Value)))
                .And(new OutOfRangeValidationPredicate<DateTimeOffset>(v => v > maxLength,
                                                             ExceptionMessages.DateTimeHasRangeNotBetweenFailedTooLate.Inject(
                                                                 minLength,
                                                                 maxLength,
                                                                 target.Value)));
        }
        #endregion
    }
}