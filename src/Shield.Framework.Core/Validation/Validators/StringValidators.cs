#region Usings
using System;
using System.Text.RegularExpressions;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators
{
    public static class StringValidators
    {
        #region Methods
        public static IValidationTarget<string> IsNotNullOrWhiteSpace(this IValidationTarget<string> target)
        {
            return target.And(new NullValidationPredicate<string>(v => !string.IsNullOrWhiteSpace(v),
                                                                  ExceptionMessages.StringsIsNotNullOrWhiteSpaceFailed));
        }

        public static IValidationTarget<string> IsNotNullOrEmpty(this IValidationTarget<string> target)
        {
            return target.And(new NullValidationPredicate<string>(v => !string.IsNullOrWhiteSpace(v),
                                                                  ExceptionMessages.StringsIsNotNullOrEmptyFailed));
        }

        public static IValidationTarget<string> IsNotEmpty(this IValidationTarget<string> target)
        {
            return target.And(new NullValidationPredicate<string>(v => v.Length > 0,
                                                                  ExceptionMessages.StringsIsNotNullOrEmptyFailed));
        }

        public static IValidationTarget<string> HasLengthBetween(this IValidationTarget<string> target, int minLength, int maxLength)
        {
            return target.And(new OutOfRangeValidationPredicate<string>(v => v.Length > minLength,
                                                                        ExceptionMessages.StringsHasLengthBetweenFailedTooShort.Inject(
                                                                            minLength,
                                                                            maxLength,
                                                                            target.Value.Length)))
                .And(new OutOfRangeValidationPredicate<string>(v => v.Length < maxLength,
                                                               ExceptionMessages.StringsHasLengthBetweenFailedTooLong.Inject(
                                                                   minLength,
                                                                   maxLength,
                                                                   target.Value.Length)));
        }

        public static IValidationTarget<string> Matches(this IValidationTarget<string> target, string pattern)
        {
            return Matches(target, new Regex(pattern));
        }

        public static IValidationTarget<string> Matches(this IValidationTarget<string> target, Regex pattern)
        {
            return target.And(new DefaultValidationPredicate<string>(pattern.IsMatch,
                                                                     ExceptionMessages.StringsMatchesFailed.Inject(target.Value, pattern)));
        }

        public static IValidationTarget<string> IsLength(this IValidationTarget<string> target, int size)
        {
            return target.And(new OutOfRangeValidationPredicate<string>(v =>
                                                                        {
                                                                            var length = v.Length;
                                                                            return length == size;
                                                                        },
                                                                        ExceptionMessages.StringsSizeIsFailed.Inject(
                                                                            size,
                                                                            target.Value.Length)));
        }

        public static IValidationTarget<string> IsGuid(this IValidationTarget<string> target)
        {
            return target.And(new DefaultValidationPredicate<string>(v => Guid.TryParse(v, out _),
                                                                     ExceptionMessages.StringsIsGuidFailed.Inject(target.Value)));
        }

        public static IValidationTarget<string> IsEqualTo(this IValidationTarget<string> target,
                                                         string expected,
                                                         StringComparison? comparison = null)
        {
            return target.And(new DefaultValidationPredicate<string>(v => StringEquals(v, expected, comparison),
                                                                     ExceptionMessages.StringsIsEqualToFailed
                                                                         .Inject(target.Value, expected)));
        }

        public static IValidationTarget<string> IsNotEqualTo(this IValidationTarget<string> target,
                                                            string expected,
                                                            StringComparison? comparison = null)
        {
            return target.And(new DefaultValidationPredicate<string>(v => !StringEquals(v, expected, comparison),
                                                                     ExceptionMessages.StringsIsNotEqualToFailed.Inject(
                                                                         target.Value,
                                                                         expected)));
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