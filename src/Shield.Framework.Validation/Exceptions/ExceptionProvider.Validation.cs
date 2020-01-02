#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using Shield.Framework.Validation.Exceptions;
#endregion

namespace Shield.Framework.Exceptions
{
    internal static class ValidationExceptionProvider
    {
        #region Methods
        public static ValidationException ValidationException(string paramName,
                                                              string message = null,
                                                              IEnumerable<Exception> innerExceptions = null,
                                                              params KeyValuePair<string, object>[] data)
        {
            if (string.IsNullOrWhiteSpace(message))
                message = "An exception occurred.";

            return ExceptionProvider.GenerateException(
                                                       () => (ValidationException)Activator.CreateInstance(typeof(ValidationException),
                                                                                                           message,
                                                                                                           paramName,
                                                                                                           innerExceptions),
                                                       data);
        }

        public static ValidationException ValidationException(string paramName,
                                                              string message = null,
                                                              IEnumerable<string> innerExceptions = null,
                                                              params KeyValuePair<string, object>[] data)
        {
            if (string.IsNullOrWhiteSpace(message))
                message = "An exception occurred.";

            var exceptions = innerExceptions == null
                                 ? new ArgumentException[] { }
                                 : innerExceptions.Select(error => new ArgumentException(paramName,
                                                                                         error));
            return ExceptionProvider.GenerateException(
                                                       () => (ValidationException)Activator.CreateInstance(typeof(ValidationException),
                                                                                                           message,
                                                                                                           paramName,
                                                                                                           exceptions),
                                                       data);
        }
        #endregion
    }
}