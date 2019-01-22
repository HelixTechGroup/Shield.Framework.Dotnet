#region Usings
using System;
using System.Collections.Generic;
#endregion

namespace Shield.Framework.Validation
{
    public interface IValidationTarget<TType> : IValidate<TType>, IValidateThrow
    {
        #region Properties
        KeyValuePair<string, object>[] ExceptionData { get; }
        string File { get; }
        int Line { get; }
        string Name { get; }
        IEnumerable<IValidationRule<TType>> Rules { get; }
        string Source { get; }
        TType Value { get; }
        #endregion

        #region Methods
        void AddExceptionData(string key, object value);

        IValidationTarget<TType> And(Predicate<TType> predicate, string message);

        IValidationTarget<TType> And(IValidationPredicate<TType> predicate);

        IValidationTarget<TType> IsNotDefault();

        IValidationTarget<TType> IsNotNull();

        IValidationTarget<TType> IsRequired();

        IValidationTarget<TType> Or();

        IValidationTarget<TType> Or(Predicate<TType> predicate, string message);

        IValidationTarget<TType> Or(IValidationPredicate<TType> predicate);
        #endregion
    }
}