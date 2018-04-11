using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Time
{
    public static class DateTimeValidators
    {
        public static ValidationRule<DateTime> IsAfter(this ValidationRule<DateTime> rule, DateTime dateTime)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v > dateTime,
                                                          ExceptionMessages.DateTimeIsAfterFailed.Inject(rule.Value, dateTime)));
            return rule;
        }

        public static ValidationRule<DateTime> IsAfterNow(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v > DateTime.Now,
                                                                ExceptionMessages.DateTimeIsAfterFailed.Inject(rule.Value, DateTime.Now)));
            return rule;
        }

        public static ValidationRule<DateTime> IsAfterUtcNow(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v > DateTime.UtcNow,
                                                                ExceptionMessages.DateTimeIsAfterFailed.Inject(rule.Value, DateTime.UtcNow)));
            return rule;
        }

        public static ValidationRule<DateTime> IsAfterToday(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v.Date > DateTime.Today.Date,
                                                                ExceptionMessages.DateTimeIsAfterFailed.Inject(rule.Value, DateTime.Now.Date)));
            return rule;
        }

        public static ValidationRule<DateTime> IsAfterOrSame(this ValidationRule<DateTime> rule, DateTime dateTime)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v >= dateTime,
                                                          ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(rule.Value, dateTime)));
            return rule;
        }

        public static ValidationRule<DateTime> IsAfterOrSameAsNow(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v >= DateTime.Now,
                                                                ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(rule.Value, DateTime.Now)));
            return rule;
        }

        public static ValidationRule<DateTime> IsAfterOrSameAsUtcNow(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v >= DateTime.UtcNow,
                                                                ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(rule.Value, DateTime.UtcNow)));
            return rule;
        }

        public static ValidationRule<DateTime> IsAfterOrSameAsToday(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v.Date >= DateTime.Today.Date,
                                                                ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(rule.Value, DateTime.Now.Date)));
            return rule;
        }

        public static ValidationRule<DateTime> IsBefore(this ValidationRule<DateTime> rule, DateTime dateTime)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v < dateTime,
                                                          ExceptionMessages.DateTimeIsBeforeFailed.Inject(rule.Value, dateTime)));
            return rule;
        }

        public static ValidationRule<DateTime> IsBeforeNow(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v < DateTime.Now,
                                                                ExceptionMessages.DateTimeIsBeforeFailed.Inject(rule.Value, DateTime.Now)));
            return rule;
        }

        public static ValidationRule<DateTime> IsBeforeUtcNow(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v < DateTime.UtcNow,
                                                                ExceptionMessages.DateTimeIsBeforeFailed.Inject(rule.Value, DateTime.UtcNow)));
            return rule;
        }

        public static ValidationRule<DateTime> IsBeforeToday(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v.Date < DateTime.Today.Date,
                                                                ExceptionMessages.DateTimeIsBeforeFailed.Inject(rule.Value, DateTime.Now.Date)));
            return rule;
        }

        public static ValidationRule<DateTime> IsBeforeOrSame(this ValidationRule<DateTime> rule, DateTime dateTime)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v <= dateTime,
                                                          ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(rule.Value, dateTime)));
            return rule;
        }

        public static ValidationRule<DateTime> IsBeforeOrSameAsNow(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v >= DateTime.Now,
                                                                ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(rule.Value, DateTime.Now)));
            return rule;
        }

        public static ValidationRule<DateTime> IsBeforeOrSameAsUtcNow(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v >= DateTime.UtcNow,
                                                                ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(rule.Value, DateTime.UtcNow)));
            return rule;
        }

        public static ValidationRule<DateTime> IsBeforeOrSameAsToday(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v.Date >= DateTime.Today.Date,
                                                                ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(rule.Value, DateTime.Now.Date)));
            return rule;
        }

        public static ValidationRule<DateTime> IsEqual(this ValidationRule<DateTime> rule, DateTime dateTime)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v == dateTime,
                                                          ExceptionMessages.DateTimeIsSameAsFailed.Inject(rule.Value, dateTime)));
            return rule;
        }

        public static ValidationRule<DateTime> IsEqualToNow(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v == DateTime.Now,
                                                                ExceptionMessages.DateTimeIsSameAsFailed.Inject(rule.Value, DateTime.Now)));
            return rule;
        }

        public static ValidationRule<DateTime> IsEqualToUtcNow(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v == DateTime.UtcNow,
                                                                ExceptionMessages.DateTimeIsSameAsFailed.Inject(rule.Value, DateTime.UtcNow)));
            return rule;
        }

        public static ValidationRule<DateTime> IsEqualToToday(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v.Date == DateTime.Today.Date,
                                                                ExceptionMessages.DateTimeIsSameAsFailed.Inject(rule.Value, DateTime.Now.Date)));
            return rule;
        }

        public static ValidationRule<DateTime> IsNotEqual(this ValidationRule<DateTime> rule, DateTime dateTime)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v != dateTime,
                                                          ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(rule.Value, dateTime)));
            return rule;
        }

        public static ValidationRule<DateTime> IsNotEqualToNow(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v != DateTime.Now,
                                                                ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(rule.Value, DateTime.Now)));
            return rule;
        }

        public static ValidationRule<DateTime> IsNotEqualToUtcNow(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v != DateTime.UtcNow,
                                                                ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(rule.Value, DateTime.UtcNow)));
            return rule;
        }

        public static ValidationRule<DateTime> IsNotEqualToToday(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v.Date != DateTime.Today.Date,
                                                                ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(rule.Value, DateTime.Now.Date)));
            return rule;
        }

        public static ValidationRule<DateTime> IsMaximumValue(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v == DateTime.MaxValue,
                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, DateTime.MaxValue)));

            return rule;
        }

        public static ValidationRule<DateTime> IsMinimumValue(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v == DateTime.MinValue,
                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, DateTime.MinValue)));

            return rule;
        }

        public static ValidationRule<DateTime> IsNotMaximumValue(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v != DateTime.MaxValue,
                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, DateTime.MaxValue)));

            return rule;
        }

        public static ValidationRule<DateTime> IsNotMinimumValue(this ValidationRule<DateTime> rule)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v != DateTime.MinValue,
                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, DateTime.MinValue)));

            return rule;
        }

        public static ValidationRule<DateTime> HasRangeBetween(this ValidationRule<DateTime> rule, DateTime minLength, DateTime maxLength)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v > minLength,
                                                        ExceptionMessages.DateTimeHasRangeBetweenFailedTooEarly.Inject(
                                                            minLength,
                                                            maxLength,
                                                            rule.Value)));

            rule.AddValidator(new RuleValidator<DateTime>(v => v < maxLength,
                                                        ExceptionMessages.DateTimeHasRangeBetweenFailedTooLate.Inject(
                                                            minLength,
                                                            maxLength,
                                                            rule.Value)));

            return rule;
        }

        public static ValidationRule<DateTime> HasRangeNotBetween(this ValidationRule<DateTime> rule, DateTime minLength, DateTime maxLength)
        {
            rule.AddValidator(new RuleValidator<DateTime>(v => v < minLength,
                                                          ExceptionMessages.DateTimeHasRangeNotBetweenFailedTooEarly.Inject(
                                                              minLength,
                                                              maxLength,
                                                              rule.Value)));

            rule.AddValidator(new RuleValidator<DateTime>(v => v > maxLength,
                                                          ExceptionMessages.DateTimeHasRangeNotBetweenFailedTooLate.Inject(
                                                              minLength,
                                                              maxLength,
                                                              rule.Value)));

            return rule;
        }
    }
}