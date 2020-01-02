#region Usings
using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators.Time
{
    public static class TimeSpanValidators
    {
        #region Methods
        public static IValidationTarget<TimeSpan> IsAfter(this IValidationTarget<TimeSpan> target, TimeSpan timeSpan)
        {
            return target.And(new OutOfRangeValidationPredicate<TimeSpan>(v => v > timeSpan,
                                                                          ExceptionMessages.DateTimeIsAfterFailed.Inject(
                                                                                                                         target.Value,
                                                                                                                         timeSpan)));
        }

        public static IValidationTarget<TimeSpan> IsAfterOrSame(this IValidationTarget<TimeSpan> target, TimeSpan timeSpan)
        {
            return target.And(new OutOfRangeValidationPredicate<TimeSpan>(v => v >= timeSpan,
                                                                          ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(
                                                                                                                                 target.Value,
                                                                                                                                 timeSpan)));
        }

        public static IValidationTarget<TimeSpan> IsBefore(this IValidationTarget<TimeSpan> target, TimeSpan timeSpan)
        {
            return target.And(new OutOfRangeValidationPredicate<TimeSpan>(v => v < timeSpan,
                                                                          ExceptionMessages.DateTimeIsBeforeFailed.Inject(
                                                                                                                          target.Value,
                                                                                                                          timeSpan)));
        }

        public static IValidationTarget<TimeSpan> IsBeforeOrSame(this IValidationTarget<TimeSpan> target, TimeSpan timeSpan)
        {
            return target.And(new OutOfRangeValidationPredicate<TimeSpan>(v => v <= timeSpan,
                                                                          ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(
                                                                                                                                  target.Value,
                                                                                                                                  timeSpan)));
        }

        public static IValidationTarget<TimeSpan> IsEqual(this IValidationTarget<TimeSpan> target, TimeSpan timeSpan)
        {
            return target.And(new DefaultValidationPredicate<TimeSpan>(v => v == timeSpan,
                                                                       ExceptionMessages.DateTimeIsSameAsFailed.Inject(
                                                                                                                       target.Value,
                                                                                                                       timeSpan)));
        }

        public static IValidationTarget<TimeSpan> IsNotEqual(this IValidationTarget<TimeSpan> target, TimeSpan timeSpan)
        {
            return target.And(new DefaultValidationPredicate<TimeSpan>(v => v != timeSpan,
                                                                       ExceptionMessages.DateTimeIsNotSameAsFailed
                                                                                        .Inject(target.Value, timeSpan)));
        }

        public static IValidationTarget<TimeSpan> IsMaximumValue(this IValidationTarget<TimeSpan> target)
        {
            return target.And(new OutOfRangeValidationPredicate<TimeSpan>(v => v == TimeSpan.MaxValue,
                                                                          ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                           target.Value,
                                                                                                                           TimeSpan.MaxValue)));
        }

        public static IValidationTarget<TimeSpan> IsMinimumValue(this IValidationTarget<TimeSpan> target)
        {
            return target.And(new OutOfRangeValidationPredicate<TimeSpan>(v => v == TimeSpan.MinValue,
                                                                          ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                           target.Value,
                                                                                                                           TimeSpan.MinValue)));
        }

        public static IValidationTarget<TimeSpan> IsNotMaximumValue(this IValidationTarget<TimeSpan> target)
        {
            return target.And(new OutOfRangeValidationPredicate<TimeSpan>(v => v != TimeSpan.MaxValue,
                                                                          ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                           target.Value,
                                                                                                                           TimeSpan.MaxValue)));
        }

        public static IValidationTarget<TimeSpan> IsNotMinimumValue(this IValidationTarget<TimeSpan> target)
        {
            return target.And(new OutOfRangeValidationPredicate<TimeSpan>(v => v != TimeSpan.MinValue,
                                                                          ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                           target.Value,
                                                                                                                           TimeSpan.MinValue)));
        }

        public static IValidationTarget<TimeSpan> HasRangeBetween(this IValidationTarget<TimeSpan> target,
                                                                  TimeSpan minLength,
                                                                  TimeSpan maxLength)
        {
            return target.And(new OutOfRangeValidationPredicate<TimeSpan>(v => v > minLength,
                                                                          ExceptionMessages.DateTimeHasRangeBetweenFailedTooEarly.Inject(
                                                                                                                                         minLength,
                                                                                                                                         maxLength,
                                                                                                                                         target
                                                                                                                                             .Value)))
                         .And(new OutOfRangeValidationPredicate<TimeSpan>(v => v < maxLength,
                                                                          ExceptionMessages.DateTimeHasRangeBetweenFailedTooLate.Inject(
                                                                                                                                        minLength,
                                                                                                                                        maxLength,
                                                                                                                                        target
                                                                                                                                            .Value)));
        }

        public static IValidationTarget<TimeSpan> HasRangeNotBetween(this IValidationTarget<TimeSpan> target,
                                                                     TimeSpan minLength,
                                                                     TimeSpan maxLength)
        {
            return target.And(new OutOfRangeValidationPredicate<TimeSpan>(v => v < minLength,
                                                                          ExceptionMessages.DateTimeHasRangeNotBetweenFailedTooEarly.Inject(
                                                                                                                                            minLength,
                                                                                                                                            maxLength,
                                                                                                                                            target
                                                                                                                                                .Value)))
                         .And(new OutOfRangeValidationPredicate<TimeSpan>(v => v > maxLength,
                                                                          ExceptionMessages.DateTimeHasRangeNotBetweenFailedTooLate.Inject(
                                                                                                                                           minLength,
                                                                                                                                           maxLength,
                                                                                                                                           target
                                                                                                                                               .Value)));
        }

        public static IValidationTarget<TimeSpan> IsZero(this IValidationTarget<TimeSpan> target)
        {
            return target.And(new DefaultValidationPredicate<TimeSpan>(v => v == TimeSpan.Zero,
                                                                       ExceptionMessages.NumbersIsNonZeroFailed.Inject(target.Value)));
        }

        public static IValidationTarget<TimeSpan> IsNonZero(this IValidationTarget<TimeSpan> target)
        {
            return target.And(new DefaultValidationPredicate<TimeSpan>(v => v != TimeSpan.Zero,
                                                                       ExceptionMessages.NumbersIsZeroFailed.Inject(target.Value)));
        }
        #endregion
    }
}