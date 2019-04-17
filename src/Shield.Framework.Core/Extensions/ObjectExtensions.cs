#region Usings
using System;
using Shield.Framework.Exceptions;
using Shield.Framework.Validation;
#endregion

namespace Shield.Framework.Extensions
{
    internal static class ObjectExtensions
    {
        #region Methods
        internal static void ThrowIfNull<T, TException>(this T obj, string message = null) where TException : Exception, new()
        {
            if (obj == null)
                throw ExceptionProvider.GenerateException<TException>();
        }

        internal static void ThrowIfNull<T>(this T obj, string message = null)
        {
            obj.ThrowIfNull<T, NullReferenceException>(message);
        }


        public static IValidationTarget<T> Guard<T>(this T obj)
        {
            return Framework.Guard.Target(obj);
        }

        public static bool IsOfType<T>(this object obj, bool throwError = false)
        {
            return ValidationTarget<T>.IsOfType(obj, throwError);
        }

        public static bool IsNotOfType<T>(this object obj, bool throwError = false)
        {
            return ValidationTarget<T>.IsNotOfType(obj, throwError);
        }
        #endregion
    }
}