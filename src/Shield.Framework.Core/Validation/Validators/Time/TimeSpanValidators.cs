using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Time
{
    public static class TimeSpanValidators
    {
        public static ValidationRule<TimeSpan> IsAfter(this ValidationRule<TimeSpan> rule, TimeSpan timeSpan)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v > timeSpan,
                                                          ExceptionMessages.DateTimeIsAfterFailed.Inject(rule.Value, timeSpan)));
            return rule;
        }        

        public static ValidationRule<TimeSpan> IsAfterOrSame(this ValidationRule<TimeSpan> rule, TimeSpan timeSpan)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v >= timeSpan,
                                                          ExceptionMessages.DateTimeIsAfterOrSameAsFailed.Inject(rule.Value, timeSpan)));
            return rule;
        }
        
        public static ValidationRule<TimeSpan> IsBefore(this ValidationRule<TimeSpan> rule, TimeSpan timeSpan)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v < timeSpan,
                                                          ExceptionMessages.DateTimeIsBeforeFailed.Inject(rule.Value, timeSpan)));
            return rule;
        }
        
        public static ValidationRule<TimeSpan> IsBeforeOrSame(this ValidationRule<TimeSpan> rule, TimeSpan timeSpan)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v <= timeSpan,
                                                          ExceptionMessages.DateTimeIsBeforeOrSameAsFailed.Inject(rule.Value, timeSpan)));
            return rule;
        }
        
        public static ValidationRule<TimeSpan> IsEqual(this ValidationRule<TimeSpan> rule, TimeSpan timeSpan)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v == timeSpan,
                                                          ExceptionMessages.DateTimeIsSameAsFailed.Inject(rule.Value, timeSpan)));
            return rule;
        }

        public static ValidationRule<TimeSpan> IsNotEqual(this ValidationRule<TimeSpan> rule, TimeSpan timeSpan)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v != timeSpan,
                                                          ExceptionMessages.DateTimeIsNotSameAsFailed.Inject(rule.Value, timeSpan)));
            return rule;
        }
        
        public static ValidationRule<TimeSpan> IsMaximumValue(this ValidationRule<TimeSpan> rule)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v == TimeSpan.MaxValue,
                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, TimeSpan.MaxValue)));

            return rule;
        }

        public static ValidationRule<TimeSpan> IsMinimumValue(this ValidationRule<TimeSpan> rule)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v == TimeSpan.MinValue,
                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, TimeSpan.MinValue)));

            return rule;
        }

        public static ValidationRule<TimeSpan> IsNotMaximumValue(this ValidationRule<TimeSpan> rule)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v != TimeSpan.MaxValue,
                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, TimeSpan.MaxValue)));

            return rule;
        }

        public static ValidationRule<TimeSpan> IsNotMinimumValue(this ValidationRule<TimeSpan> rule)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v != TimeSpan.MinValue,
                                                      ExceptionMessages.NumbersIsMaxValueFailed.Inject(rule.Value, TimeSpan.MinValue)));

            return rule;
        }

        public static ValidationRule<TimeSpan> HasRangeBetween(this ValidationRule<TimeSpan> rule, TimeSpan minLength, TimeSpan maxLength)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v > minLength,
                                                        ExceptionMessages.DateTimeHasRangeBetweenFailedTooEarly.Inject(
                                                            minLength,
                                                            maxLength,
                                                            rule.Value)));

            rule.AddValidator(new RuleValidator<TimeSpan>(v => v < maxLength,
                                                        ExceptionMessages.DateTimeHasRangeBetweenFailedTooLate.Inject(
                                                            minLength,
                                                            maxLength,
                                                            rule.Value)));

            return rule;
        }

        public static ValidationRule<TimeSpan> HasRangeNotBetween(this ValidationRule<TimeSpan> rule, TimeSpan minLength, TimeSpan maxLength)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v < minLength,
                                                          ExceptionMessages.DateTimeHasRangeNotBetweenFailedTooEarly.Inject(
                                                              minLength,
                                                              maxLength,
                                                              rule.Value)));

            rule.AddValidator(new RuleValidator<TimeSpan>(v => v > maxLength,
                                                          ExceptionMessages.DateTimeHasRangeNotBetweenFailedTooLate.Inject(
                                                              minLength,
                                                              maxLength,
                                                              rule.Value)));

            return rule;
        }

        public static ValidationRule<TimeSpan> IsZero(this ValidationRule<TimeSpan> rule)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v == TimeSpan.Zero,
                                                          ExceptionMessages.NumbersIsNonZeroFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<TimeSpan> IsNonZero(this ValidationRule<TimeSpan> rule)
        {
            rule.AddValidator(new RuleValidator<TimeSpan>(v => v != TimeSpan.Zero,
                                                          ExceptionMessages.NumbersIsZeroFailed.Inject(rule.Value)));

            return rule;
        }
    }
}