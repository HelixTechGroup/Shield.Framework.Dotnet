#region Usings
using System;
using Shield.Framework.Validation.Exceptions;
#endregion

namespace Shield.Framework.Validation
{
    public interface IValidate<TType>
    {
        #region Methods
        ValidationResult Validate();

        IValidate<TType> WithMessage(string message);

        ValidationExceptionSelector<TType> WithException();

        IValidate<TType> WithException<TNewException>() where TNewException : Exception, new();
        #endregion
    }
}