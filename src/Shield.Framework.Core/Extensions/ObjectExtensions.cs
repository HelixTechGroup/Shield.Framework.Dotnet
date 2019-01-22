#region Usings
using System;
using Shield.Framework.Validation;
#endregion

namespace Shield.Framework.Extensions
{
    internal static class ObjectExtensions
    {
        #region Methods
        internal static void ThrowIfNull<T>(this T obj)
        {
            if (obj == null)
#pragma warning disable S112 // General exceptions should never be thrown
                throw new NullReferenceException();
#pragma warning restore S112 // General exceptions should never be thrown
        }

        internal static void ThrowIfNull<T>(this T obj, string message)
        {
            if (obj == null)
#pragma warning disable S112 // General exceptions should never be thrown
                throw new NullReferenceException(message);
#pragma warning restore S112 // General exceptions should never be thrown
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