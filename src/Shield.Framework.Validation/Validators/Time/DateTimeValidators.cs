#region Usings
using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators.Time
{
    public static class DateTimeValidators
    {
        #region Methods
        public static IValidationTarget<DateTime> IsAfter(this IValidationTarget<DateTime> target, DateTime dateTime)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v > dateTime,
                                                                          ExceptionMessages.DateTimeIsAfterFailed.Inject(
                                                                                                                         target.Value,
                                                                                                                         dateTime)));
        }

        public static IValidationTarget<DateTime> IsAfterNow(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v > DateTime.Now,
                                                                          ExceptionMessages.DateTimeIsAfterFailed.Inject(
                                                                                                                         target.Value,
                                                                                                                         DateTime.Now)));
        }

        public static IValidationTarget<DateTime> IsAfterUtcNow(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v > DateTime.UtcNow,
                                                                          ExceptionMessages.DateTimeIsAfterFailed.Inject(
                                                                                                                         target.Value,
                                                                                                                         DateTime.UtcNow)));
        }

        public static IValidationTarget<DateTime> IsAfterToday(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v.Date > DateTime.Today.Date,
                                                                          ExceptionMessages.DateTimeIsAfterFailed.Inject(
                                                                                                                         target.Value,
                                                                                                                         DateTime.Now.Date)));
        }

        public static IValidationTarget<DateTime> IsAfterOrSame(this IValidationTarget<DateTime> target, DateTime dateTime)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v >= dateTime,
                                                                          ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(
                                                                                                                                 target.Value,
                                                                                                                                 dateTime)));
        }

        public static IValidationTarget<DateTime> IsAfterOrSameAsNow(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v >= DateTime.Now,
                                                                          ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(
                                                                                                                                 target.Value,
                                                                                                                                 DateTime.Now)));
        }

        public static IValidationTarget<DateTime> IsAfterOrSameAsUtcNow(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v >= DateTime.UtcNow,
                                                                          ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(
                                                                                                                                 target.Value,
                                                                                                                                 DateTime.UtcNow)));
        }

        public static IValidationTarget<DateTime> IsAfterOrSameAsToday(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v.Date >= DateTime.Today.Date,
                                                                          ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(
                                                                                                                                 target.Value,
                                                                                                                                 DateTime.Now.Date)));
        }

        public static IValidationTarget<DateTime> IsBefore(this IValidationTarget<DateTime> target, DateTime dateTime)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v < dateTime,
                                                                          ExceptionMessages.DateTimeIsBeforeFailed.Inject(
                                                                                                                          target.Value,
                                                                                                                          dateTime)));
        }

        public static IValidationTarget<DateTime> IsBeforeNow(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v < DateTime.Now,
                                                                          ExceptionMessages.DateTimeIsBeforeFailed.Inject(
                                                                                                                          target.Value,
                                                                                                                          DateTime.Now)));
        }

        public static IValidationTarget<DateTime> IsBeforeUtcNow(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v < DateTime.UtcNow,
                                                                          ExceptionMessages.DateTimeIsBeforeFailed.Inject(
                                                                                                                          target.Value,
                                                                                                                          DateTime.UtcNow)));
        }

        public static IValidationTarget<DateTime> IsBeforeToday(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v.Date < DateTime.Today.Date,
                                                                          ExceptionMessages.DateTimeIsBeforeFailed.Inject(
                                                                                                                          target.Value,
                                                                                                                          DateTime.Now.Date)));
        }

        public static IValidationTarget<DateTime> IsBeforeOrSame(this IValidationTarget<DateTime> target, DateTime dateTime)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v <= dateTime,
                                                                          ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(
                                                                                                                                  target.Value,
                                                                                                                                  dateTime)));
        }

        public static IValidationTarget<DateTime> IsBeforeOrSameAsNow(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v >= DateTime.Now,
                                                                          ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(
                                                                                                                                  target.Value,
                                                                                                                                  DateTime.Now)));
        }

        public static IValidationTarget<DateTime> IsBeforeOrSameAsUtcNow(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v >= DateTime.UtcNow,
                                                                          ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(
                                                                                                                                  target.Value,
                                                                                                                                  DateTime.UtcNow)));
        }

        public static IValidationTarget<DateTime> IsBeforeOrSameAsToday(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v.Date >= DateTime.Today.Date,
                                                                          ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(
                                                                                                                                  target.Value,
                                                                                                                                  DateTime
                                                                                                                                      .Now.Date)));
        }

        public static IValidationTarget<DateTime> IsEqual(this IValidationTarget<DateTime> target, DateTime dateTime)
        {
            return target.And(new DefaultValidationPredicate<DateTime>(v => v == dateTime,
                                                                       ExceptionMessages.DateTimeIsSameAsFailed.Inject(
                                                                                                                       target.Value,
                                                                                                                       dateTime)));
        }

        public static IValidationTarget<DateTime> IsEqualToNow(this IValidationTarget<DateTime> target)
        {
            return target.And(new DefaultValidationPredicate<DateTime>(v => v == DateTime.Now,
                                                                       ExceptionMessages.DateTimeIsSameAsFailed.Inject(
                                                                                                                       target.Value,
                                                                                                                       DateTime.Now)));
        }

        public static IValidationTarget<DateTime> IsEqualToUtcNow(this IValidationTarget<DateTime> target)
        {
            return target.And(new DefaultValidationPredicate<DateTime>(v => v == DateTime.UtcNow,
                                                                       ExceptionMessages.DateTimeIsSameAsFailed.Inject(
                                                                                                                       target.Value,
                                                                                                                       DateTime.UtcNow)));
        }

        public static IValidationTarget<DateTime> IsEqualToToday(this IValidationTarget<DateTime> target)
        {
            return target.And(new DefaultValidationPredicate<DateTime>(v => v.Date == DateTime.Today.Date,
                                                                       ExceptionMessages.DateTimeIsSameAsFailed.Inject(
                                                                                                                       target.Value,
                                                                                                                       DateTime.Now.Date)));
        }

        public static IValidationTarget<DateTime> IsNotEqual(this IValidationTarget<DateTime> target, DateTime dateTime)
        {
            return target.And(new DefaultValidationPredicate<DateTime>(v => v != dateTime,
                                                                       ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(
                                                                                                                          target.Value,
                                                                                                                          dateTime)));
        }

        public static IValidationTarget<DateTime> IsNotEqualToNow(this IValidationTarget<DateTime> target)
        {
            return target.And(new DefaultValidationPredicate<DateTime>(v => v != DateTime.Now,
                                                                       ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(
                                                                                                                          target.Value,
                                                                                                                          DateTime.Now)));
        }

        public static IValidationTarget<DateTime> IsNotEqualToUtcNow(this IValidationTarget<DateTime> target)
        {
            return target.And(new DefaultValidationPredicate<DateTime>(v => v != DateTime.UtcNow,
                                                                       ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(
                                                                                                                          target.Value,
                                                                                                                          DateTime.UtcNow)));
        }

        public static IValidationTarget<DateTime> IsNotEqualToToday(this IValidationTarget<DateTime> target)
        {
            return target.And(new DefaultValidationPredicate<DateTime>(v => v.Date != DateTime.Today.Date,
                                                                       ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(
                                                                                                                          target.Value,
                                                                                                                          DateTime.Now.Date)));
        }

        public static IValidationTarget<DateTime> IsMaximumValue(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v == DateTime.MaxValue,
                                                                          ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                           target.Value,
                                                                                                                           DateTime.MaxValue)));
        }

        public static IValidationTarget<DateTime> IsMinimumValue(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v == DateTime.MinValue,
                                                                          ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                           target.Value,
                                                                                                                           DateTime.MinValue)));
        }

        public static IValidationTarget<DateTime> IsNotMaximumValue(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v != DateTime.MaxValue,
                                                                          ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                           target.Value,
                                                                                                                           DateTime.MaxValue)));
        }

        public static IValidationTarget<DateTime> IsNotMinimumValue(this IValidationTarget<DateTime> target)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v != DateTime.MinValue,
                                                                          ExceptionMessages.NumbersIsMaxValueFailed.Inject(
                                                                                                                           target.Value,
                                                                                                                           DateTime.MinValue)));
        }

        public static IValidationTarget<DateTime> HasRangeBetween(this IValidationTarget<DateTime> target,
                                                                  DateTime minLength,
                                                                  DateTime maxLength)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v > minLength,
                                                                          ExceptionMessages.DateTimeHasRangeBetweenFailedTooEarly.Inject(
                                                                                                                                         minLength,
                                                                                                                                         maxLength,
                                                                                                                                         target
                                                                                                                                             .Value)))
                         .And(new OutOfRangeValidationPredicate<DateTime>(v => v < maxLength,
                                                                          ExceptionMessages.DateTimeHasRangeBetweenFailedTooLate.Inject(
                                                                                                                                        minLength,
                                                                                                                                        maxLength,
                                                                                                                                        target
                                                                                                                                            .Value)));
        }

        public static IValidationTarget<DateTime> HasRangeNotBetween(this IValidationTarget<DateTime> target,
                                                                     DateTime minLength,
                                                                     DateTime maxLength)
        {
            return target.And(new OutOfRangeValidationPredicate<DateTime>(v => v < minLength,
                                                                          ExceptionMessages.DateTimeHasRangeNotBetweenFailedTooEarly.Inject(
                                                                                                                                            minLength,
                                                                                                                                            maxLength,
                                                                                                                                            target
                                                                                                                                                .Value)))
                         .And(new OutOfRangeValidationPredicate<DateTime>(v => v > maxLength,
                                                                          ExceptionMessages.DateTimeHasRangeNotBetweenFailedTooLate.Inject(
                                                                                                                                           minLength,
                                                                                                                                           maxLength,
                                                                                                                                           target
                                                                                                                                               .Value)));
        }
        #endregion
    }
}