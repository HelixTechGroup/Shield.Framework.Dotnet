#region Usings
using System;
using Shield.Framework.Validation.Exceptions;
#endregion

namespace Shield.Framework.Validation
{
    public interface IValidateThrow
    {
        #region Methods
        Exception Throw();

        TException Throw<TException>(string message = null) where TException : Exception, new();

        ValidationException ThrowAll();
        #endregion
    }
}