#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using Shield.Framework.Validation.Exceptions;
#endregion

namespace Shield.Framework.Exceptions
{
    public static partial class ExceptionFactory
    {
        #region Methods
        public static ArgumentNullException ArgumentNullException(string paramName,
                                                                  string message = null,
                                                                  params KeyValuePair<string, object>[] data)
        {
            return GenerateException(
                () => (ArgumentNullException)Activator.CreateInstance(typeof(ArgumentNullException), paramName, message),
                data);
        }

        public static ArgumentException ArgumentException(string paramName,
                                                          string message = null,
                                                          params KeyValuePair<string, object>[] data)
        {
            return GenerateException(
                () => (ArgumentException)Activator.CreateInstance(typeof(ArgumentException), paramName, message),
                data);
        }

        public static ValidationException ValidationException(string paramName,
                                                              string message = null,
                                                              IEnumerable<Exception> innerExceptions = null,
                                                              params KeyValuePair<string, object>[] data)
        {
            return GenerateException(
                () => (ValidationException)Activator.CreateInstance(typeof(ValidationException), paramName, message, innerExceptions),
                data);
        }

        public static ValidationException ValidationException(string paramName,
                                                              string message = null,
                                                              IEnumerable<string> innerExceptions = null,
                                                              params KeyValuePair<string, object>[] data)
        {
            var exceptions = innerExceptions.Select(error => ArgumentException(paramName,
                                                                               error,
                                                                               data.ToArray()));
            return GenerateException(
                () => (ValidationException)Activator.CreateInstance(typeof(ValidationException), paramName, message, exceptions),
                data);
        }
        #endregion
    }
}