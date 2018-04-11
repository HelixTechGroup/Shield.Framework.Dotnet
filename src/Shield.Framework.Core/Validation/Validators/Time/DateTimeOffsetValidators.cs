using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Time
{
    public static class DateTimeOffsetOffsetValidators
    {
        public static ValidationRule<DateTimeOffset> IsAfter(this ValidationRule<DateTimeOffset> rule, DateTimeOffset offset)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v > offset,
                                                          ExceptionMessages.DateTimeIsAfterFailed.Inject(rule.Value, offset)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsAfterNow(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v > DateTimeOffset.Now,
                                                                ExceptionMessages.DateTimeIsAfterFailed.Inject(rule.Value, DateTimeOffset.Now)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsAfterUtcNow(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v > DateTimeOffset.UtcNow,
                                                                ExceptionMessages.DateTimeIsAfterFailed.Inject(rule.Value, DateTimeOffset.UtcNow)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsAfterToday(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v.Date > DateTimeOffset.Now.Date,
                                                                ExceptionMessages.DateTimeIsAfterFailed.Inject(rule.Value, DateTimeOffset.Now.Date)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsAfterOrSame(this ValidationRule<DateTimeOffset> rule, DateTimeOffset offset)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v >= offset,
                                                          ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(rule.Value, offset)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsAfterOrSameAsNow(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v >= DateTimeOffset.Now,
                                                                ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(rule.Value, DateTimeOffset.Now)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsAfterOrSameAsUtcNow(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v >= DateTimeOffset.UtcNow,
                                                                ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(rule.Value, DateTimeOffset.UtcNow)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsAfterOrSameAsToday(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v.Date >= DateTimeOffset.Now.Date,
                                                                ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(rule.Value, DateTimeOffset.Now.Date)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsBefore(this ValidationRule<DateTimeOffset> rule, DateTimeOffset offset)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v < offset,
                                                          ExceptionMessages.DateTimeIsBeforeFailed.Inject(rule.Value, offset)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsBeforeNow(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v < DateTimeOffset.Now,
                                                                ExceptionMessages.DateTimeIsBeforeFailed.Inject(rule.Value, DateTimeOffset.Now)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsBeforeUtcNow(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v < DateTimeOffset.UtcNow,
                                                                ExceptionMessages.DateTimeIsBeforeFailed.Inject(rule.Value, DateTimeOffset.UtcNow)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsBeforeToday(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v.Date < DateTimeOffset.Now.Date,
                                                                ExceptionMessages.DateTimeIsBeforeFailed.Inject(rule.Value, DateTimeOffset.Now.Date)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsBeforeOrSame(this ValidationRule<DateTimeOffset> rule, DateTimeOffset offset)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v <= offset,
                                                          ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(rule.Value, offset)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsBeforeOrSameAsNow(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v >= DateTimeOffset.Now,
                                                                ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(rule.Value, DateTimeOffset.Now)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsBeforeOrSameAsUtcNow(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v >= DateTimeOffset.UtcNow,
                                                                ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(rule.Value, DateTimeOffset.UtcNow)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsBeforeOrSameAsToday(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v.Date >= DateTimeOffset.Now.Date,
                                                                ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(rule.Value, DateTimeOffset.Now.Date)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsEqual(this ValidationRule<DateTimeOffset> rule, DateTimeOffset offset)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v == offset,
                                                          ExceptionMessages.DateTimeIsSameAsFailed.Inject(rule.Value, offset)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsEqualToNow(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v == DateTimeOffset.Now,
                                                                ExceptionMessages.DateTimeIsSameAsFailed.Inject(rule.Value, DateTimeOffset.Now)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsEqualToUtcNow(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v == DateTimeOffset.UtcNow,
                                                                ExceptionMessages.DateTimeIsSameAsFailed.Inject(rule.Value, DateTimeOffset.UtcNow)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsEqualToToday(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v.Date == DateTimeOffset.Now.Date,
                                                                ExceptionMessages.DateTimeIsSameAsFailed.Inject(rule.Value, DateTimeOffset.Now.Date)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsNotEqual(this ValidationRule<DateTimeOffset> rule, DateTimeOffset offset)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v != offset,
                                                          ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(rule.Value, offset)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsNotEqualToNow(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v != DateTimeOffset.Now,
                                                                ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(rule.Value, DateTimeOffset.Now)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsNotEqualToUtcNow(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v != DateTimeOffset.UtcNow,
                                                                ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(rule.Value, DateTimeOffset.UtcNow)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsNotEqualToToday(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v.Date != DateTimeOffset.Now.Date,
                                                                ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(rule.Value, DateTimeOffset.Now.Date)));
            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsMaximumValue(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v == DateTimeOffset.MaxValue,
                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, DateTimeOffset.MaxValue)));

            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsMinimumValue(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v == DateTimeOffset.MinValue,
                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, DateTimeOffset.MinValue)));

            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsNotMaximumValue(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v != DateTimeOffset.MaxValue,
                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, DateTimeOffset.MaxValue)));

            return rule;
        }

        public static ValidationRule<DateTimeOffset> IsNotMinimumValue(this ValidationRule<DateTimeOffset> rule)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v != DateTimeOffset.MinValue,
                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, DateTimeOffset.MinValue)));

            return rule;
        }

        public static ValidationRule<DateTimeOffset> HasRangeBetween(this ValidationRule<DateTimeOffset> rule, DateTimeOffset minLength, DateTimeOffset maxLength)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v > minLength,
                                                        ExceptionMessages.DateTimeHasRangeBetweenFailedTooEarly.Inject(
                                                            minLength,
                                                            maxLength,
                                                            rule.Value)));

            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v < maxLength,
                                                        ExceptionMessages.DateTimeHasRangeBetweenFailedTooLate.Inject(
                                                            minLength,
                                                            maxLength,
                                                            rule.Value)));

            return rule;
        }

        public static ValidationRule<DateTimeOffset> HasRangeNotBetween(this ValidationRule<DateTimeOffset> rule, DateTimeOffset minLength, DateTimeOffset maxLength)
        {
            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v < minLength,
                                                          ExceptionMessages.DateTimeHasRangeNotBetweenFailedTooEarly.Inject(
                                                              minLength,
                                                              maxLength,
                                                              rule.Value)));

            rule.AddValidator(new RuleValidator<DateTimeOffset>(v => v > maxLength,
                                                          ExceptionMessages.DateTimeHasRangeNotBetweenFailedTooLate.Inject(
                                                              minLength,
                                                              maxLength,
                                                              rule.Value)));

            return rule;
        }
    }
}