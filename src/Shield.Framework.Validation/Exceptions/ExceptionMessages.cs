namespace Shield.Framework.Validation.Exceptions
{
    internal static class ExceptionMessages
    {
        #region Common
        internal static string CommonIsRequiredFailed { get; } = "The {0} field is required.";
        internal static string CommonIsNotNullFailed { get; } = "Value can not be null.";
        internal static string CommonIsNotDefaultFailed { get; } = "The param was expected to not be of default value.";
        #endregion

        #region Boolean
        internal static string BooleansIsTrueFailed { get; } = "Expected an expression that evaluates to true.";
        internal static string BooleansIsFalseFailed { get; } = "Expected an expression that evaluates to false.";
        #endregion

        #region Collections
        internal static string CollectionsIsSynchronizedFailed { get; } = "The collection is not synchronized.";
        internal static string CollectionsAnyFailed { get; } = "The predicate did not match any elements in the collection.";
        internal static string CollectionsNotAnyFailed { get; } = "The predicate matched elements in the collection.";
        internal static string CollectionsIsEmptyFailed { get; } = "The collection must be empty.";
        internal static string CollectionsHasItemsFailed { get; } = "Empty collection is not allowed.";
        internal static string CollectionsSizeIsFailed { get; } = "Expected size '{0}' but found '{1}'.";
        internal static string CollectionsMinimumSizeIsFailed { get; } = "Minimum Expected size '{0}' but found '{1}'.";
        internal static string CollectionsMaximumSizeIsFailed { get; } = "Maximum Expected size '{0}' but found '{1}'.";
        internal static string CollectionsHasSizeBetweenFailedToShort { get; } = "The collection is not long enough. Must be between '{0}' and '{1}' but was '{2}' elements long.";
        internal static string CollectionsHasSizeBetweenFailedToLong { get; } = "The collection is too long. Must be between '{0}' and  '{1}'. Must be between '{0}' and '{1}' but was '{2}' elements long.";
        internal static string CollectionsSynchronizationFailed { get; } = "The collection is not synchronized.";
        internal static string CollectionsIsReadOnlyFailed { get; } = "The collection is not read only.";
        internal static string CollectionsIsNotReadOnlyFailed { get; } = "The collection is read only.";
        internal static string CollectionsContainsValueFailed { get; } = "Value '{0}' does not exist in collection.";
        internal static string CollectionsDoesNotContainValueFailed { get; } = "Value '{0}' does exist in collection.";
        internal static string CollectionsIsNotFixedSizeFailed { get; } = "The list has a fixed size of {0}.";
        internal static string CollectionsIsFixedSizeFailed { get; } = "The list does not have a fixed size.";
        #endregion

        #region Dictionary
        internal static string DictionariesContainsKeyFailed { get; } = "Key '{0}' does not exist in collection.";
        internal static string DictionariesDoesNotContainKeyFailed { get; } = "Key '{0}' does not exist in collection.";
        internal static string DictionaryAnyKeyFailed { get; } = "The predicate did not match any keys in the collection.";
        internal static string DictionaryNotAnyKeyFailed { get; } = "The predicate matched keys in the collection.";
        #endregion

        #region Numbers
        internal static string NumbersIsFailed { get; } = "Value '{0}' is not '{1}'.";
        internal static string NumbersIsNotFailed { get; } = "Value '{0}' is '{1}', which was not expected.";
        internal static string NumbersIsLtFailed { get; } = "Value '{0}' is not lower than limit '{1}'.";
        internal static string NumbersIsLteFailed { get; } = "Value '{0}' is not lower than or equal to limit '{1}'.";
        internal static string NumbersIsGtFailed { get; } = "Value '{0}' is not greater than limit '{1}'.";
        internal static string NumbersIsGteFailed { get; } = "Value '{0}' is not greater than or equal to limit '{1}'.";
        internal static string NumbersIsNotInRangeTooLowFailed { get; } = "Value '{0}' is < min '{1}'.";
        internal static string NumbersIsNotInRangeTooHighFailed { get; } = "Value '{0}' is > max '{1}'.";
        internal static string NumbersIsInRangeTooLowFailed { get; } = "Value '{0}' is not > min '{1}'.";
        internal static string NumbersIsInRangeTooHighFailed { get; } = "Value '{0}' is not < max '{1}'.";
        internal static string NumbersIsNegativeFailed { get; } = "Value '{0}' is not a negative number.";
        internal static string NumbersIsPositiveFailed { get; } = "Value '{0}' is not a positive number.";
        internal static string NumbersIsNaNFailed { get; } = "Value '{0}' is not NaN.";
        internal static string NumbersIsNumberFailed { get; } = "Value '{0}' is not a number.";
        internal static string NumbersIsNonZeroFailed { get; } = "Value '{0}' is a zero.";
        internal static string NumbersIsZeroFailed { get; } = "Value '{0}' is not zero.";
        internal static string NumbersIsMaxValueFailed { get; } = "Value '{0}' is not the maximum value of {1}.";
        internal static string NumbersIsMinValueFailed { get; } = "Value '{0}' is not the minimum value of {1}.";
        internal static string NumbersIsNotMaxValueFailed { get; } = "Value '{0}' is the maximum value of {1}.";
        internal static string NumbersIsNotMinValueFailed { get; } = "Value '{0}' is the minimum value of {1}.";
        internal static string NumbersIsEvenFailed { get; } = "Value '{0}' is not even number.";
        internal static string NumbersIsOddFailed { get; } = "Value '{0}' is not odd number.";
        internal static string NumbersIsInfinityFailed { get; } = "Value '{0}' is not infinity.";
        internal static string NumbersIsNotInfinityFailed { get; } = "Value '{0}' is infinity.";
        internal static string NumbersIsNegativeInfinityFailed { get; } = "Value '{0}' is not negative infinity.";
        internal static string NumbersIsNotNegativeInfinityFailed { get; } = "Value '{0}' is negative infinity.";
        internal static string NumbersIsPositiveInfinityFailed { get; } = "Value '{0}' is not positive infinity.";
        internal static string NumbersIsNotPositiveInfinityFailed { get; } = "Value '{0}' is positive infinity.";
        #endregion

        #region Guid
        internal static string GuidsIsEmptyFailed { get; } = "Guid must be empty.";
        internal static string GuidsIsNotEmptyFailed { get; } = "Empty Guid is not allowed.";
        internal static string GuidsIsEqualToFailed { get; } = "Guid '{0}' is not '{1}'.";
        internal static string GuidsIsNotEqualToFailed { get; } = "Guid '{0}' is '{1}', which was not expected.";
        #endregion

        #region String
        internal static string StringsIsEqualToFailed { get; } = "Value '{0}' is not '{1}'.";
        internal static string StringsIsNotEqualToFailed { get; } = "Value '{0}' is '{1}', which was not expected.";
        internal static string StringsSizeIsFailed { get; } = "Expected length '{0}' but found '{1}'.";
        internal static string StringsIsNotNullOrWhiteSpaceFailed { get; } = "The string can't be left empty, null or consist of only whitespaces.";
        internal static string StringsIsNotNullOrEmptyFailed { get; } = "The string can't be null or empty.";
        internal static string StringsHasLengthBetweenFailedTooShort { get; } = "The string is not long enough. Must be between '{0}' and '{1}' but was '{2}' characters long.";
        internal static string StringsHasLengthBetweenFailedTooLong { get; } = "The string is too long. Must be between '{0}' and  '{1}'. Must be between '{0}' and '{1}' but was '{2}' characters long.";
        internal static string StringsMatchesFailed { get; } = "Value '{0}' does not match '{1}'";
        internal static string StringsIsNotEmptyFailed { get; } = "Empty String is not allowed.";
        internal static string StringsIsGuidFailed { get; } = "Value '{0}' is not a valid GUID.";
        #endregion

        #region DateTime
        internal static string DateTimeIsAfterFailed { get; } = "Value '{0}' is not after {1}.";
        internal static string DateTimeIsAfterOrSameAsFailed { get; } = "Value '{0}' is not after or same as {1}.";
        internal static string DateTimeIsBeforeFailed { get; } = "Value '{0}' is not before {1}.";
        internal static string DateTimeIsBeforeOrSameAsFailed { get; } = "Value '{0}' is not before or same as {1}.";
        internal static string DateTimeIsSameAsFailed { get; } = "Value '{0}' is not same as {1}.";
        internal static string DateTimeIsNotSameAsFailed { get; } = "Value '{0}' is the same as {1}.";
        internal static string DateTimeHasRangeBetweenFailedTooEarly { get; } = "The DateTime is too early. Must be between '{0}' and '{1}' but was '{2}'.";
        internal static string DateTimeHasRangeBetweenFailedTooLate { get; } = "The DateTime is too late. Must be between '{0}' and  '{1}'. Must be between '{0}' and '{1}' but was '{2}'.";
        internal static string DateTimeHasRangeNotBetweenFailedTooEarly { get; } = "The DateTime is too late. Must not be between '{0}' and '{1}' but was '{2}'.";
        internal static string DateTimeHasRangeNotBetweenFailedTooLate { get; } = "The DateTime is too early. Must not be between '{0}' and  '{1}'. Must be between '{0}' and '{1}' but was '{2}'.";
        #endregion

        #region Type
        internal static string TypesIsOfTypeFailed { get; } = "The param is not of expected type. Expected: '{0}'. Got: '{1}'.";
        internal static string TypesIsNotOfTypeFailed { get; } = "The param was expected to not be of the type: '{0}'. But it was.";
        internal static string TypesIsClassFailedNull { get; } = "The param was expected to be a class, but was NULL.";
        internal static string TypesIsClassFailed { get; } = "The param was expected to be a class, but was type of: '{0}'.";
        #endregion
    }
}