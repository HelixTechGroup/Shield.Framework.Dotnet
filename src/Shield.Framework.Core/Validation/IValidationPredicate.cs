#region Usings
using System;
using System.Collections.Generic;
#endregion

namespace Shield.Framework.Validation
{
    public interface IValidationPredicate<TType> : IValidate<TType>
    {
        #region Properties
        string ErrorMessage { get; }
        Type ExceptionType { get; }
        IValidationRule<TType> Rule { get; }
        #endregion

        #region Methods
        IValidationPredicate<TType> WithRule(IValidationRule<TType> rule);

        Exception GenerateException(string name, KeyValuePair<string, object>[] exceptionData);
        #endregion
    }
}