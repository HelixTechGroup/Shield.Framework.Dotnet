using System;
using System.Collections.Generic;
using System.Globalization;
using Shield.Framework.Extensions;

namespace Shield.Framework.Exceptions
{
    public static partial class ExceptionFactory
    {
        public static TException GenerateException<TException>(string message = null, object[] args = null, params KeyValuePair<string, string>[] data) where TException : Exception, new()
        {

            return GenerateException<TException>(
                () => (TException)Activator.CreateInstance(typeof(TException), message),
                data);
        }

        public static TException GenerateException<TException>(Func<TException> exceptionFactory, params KeyValuePair<string, string>[] data) where TException : Exception, new()
        {
            if (exceptionFactory == null)
                throw new ArgumentNullException(nameof(exceptionFactory), "No exception factory was specified.");

            var exceptionToThrow = exceptionFactory();
            if (exceptionToThrow == null)
                throw new InvalidOperationException("An exception could not be generated through the given exception factory");

            // Add any additional content that the caller requires to exist in the Exception data.
            exceptionToThrow.Data.AddRange(data);

            // Add a time-stamp for when the exception was thrown.
            exceptionToThrow.Data.AddRange(
                new KeyValuePair<string, string>("Date", DateTime.Now.ToString(CultureInfo.InvariantCulture)));

            return exceptionToThrow;
        }

    }
}