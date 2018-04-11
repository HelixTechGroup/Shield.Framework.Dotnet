#region Usings
using System;
using System.Text.RegularExpressions;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
#endregion

namespace Shield.Framework.Validation.Validators
{
    public static class StringValidators
    {
        #region Methods
        public static ValidationRule<string> IsNotNullOrWhiteSpace(this ValidationRule<string> rule)
        {
            rule.AddValidator(new RuleValidator<string>(v => !string.IsNullOrWhiteSpace(v),
                                                        ExceptionMessages.StringsIsNotNullOrWhiteSpaceFailed));

            return rule;
        }

        public static ValidationRule<string> IsNotNullOrEmpty(this ValidationRule<string> rule)
        {
            rule.AddValidator(new RuleValidator<string>(v => !string.IsNullOrWhiteSpace(v),
                                                        ExceptionMessages.StringsIsNotNullOrEmptyFailed));

            return rule;
        }

        public static ValidationRule<string> IsNotEmpty(this ValidationRule<string> rule)
        {
            rule.AddValidator(new RuleValidator<string>(v => v.Length > 0,
                                                        ExceptionMessages.StringsIsNotNullOrEmptyFailed));

            return rule;
        }

        public static ValidationRule<string> HasLengthBetween(this ValidationRule<string> rule, int minLength, int maxLength)
        {
            rule.AddValidator(new RuleValidator<string>(v => v.Length > minLength,
                                                        ExceptionMessages.StringsHasLengthBetweenFailedTooShort.Inject(
                                                            minLength,
                                                            maxLength,
                                                            rule.Value.Length)));

            rule.AddValidator(new RuleValidator<string>(v => v.Length < maxLength,
                                                        ExceptionMessages.StringsHasLengthBetweenFailedTooLong.Inject(
                                                            minLength,
                                                            maxLength,
                                                            rule.Value.Length)));

            return rule;
        }

        public static ValidationRule<string> Matches(this ValidationRule<string> rule, string pattern)
        {
            return Matches(rule, new Regex(pattern));
        }

        public static ValidationRule<string> Matches(this ValidationRule<string> rule, Regex pattern)
        {
            rule.AddValidator(new RuleValidator<string>(pattern.IsMatch,
                                                        ExceptionMessages.StringsMatchesFailed.Inject(rule.Value, pattern)));

            return rule;
        }

        public static ValidationRule<string> IsLength(this ValidationRule<string> rule, int size)
        {
            rule.AddValidator(new RuleValidator<string>(v =>
                                                        {
                                                            var length = v.Length;
                                                            return length == size;
                                                        },
                                                        ExceptionMessages.StringsSizeIsFailed.Inject(size, rule.Value.Length)));

            return rule;
        }

        public static ValidationRule<string> IsGuid(this ValidationRule<string> rule)
        {
            rule.AddValidator(new RuleValidator<string>(v => Guid.TryParse(v, out _),
                                                        ExceptionMessages.StringsIsGuidFailed.Inject(rule.Value)));

            return rule;
        }

        public static ValidationRule<string> IsEqualTo(this ValidationRule<string> rule,
                                                       string expected,
                                                       StringComparison? comparison = null)
        {
            rule.AddValidator(new RuleValidator<string>(v => StringEquals(v, expected, comparison),
                                                        ExceptionMessages.StringsIsEqualToFailed.Inject(rule.Value, expected)));

            return rule;
        }

        public static ValidationRule<string> IsNotEqualTo(this ValidationRule<string> rule,
                                                          string expected,
                                                          StringComparison? comparison = null)
        {
            rule.AddValidator(new RuleValidator<string>(v => !StringEquals(v, expected, comparison),
                                                        ExceptionMessages.StringsIsNotEqualToFailed.Inject(rule.Value, expected)));

            return rule;
        }

        private static bool StringEquals(string x, string y, StringComparison? comparison = null)
        {
            return comparison.HasValue
                       ? string.Equals(x, y, comparison.Value)
                       : string.Equals(x, y);
        }
        #endregion
    }
}