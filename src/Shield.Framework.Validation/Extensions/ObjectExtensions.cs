#region Usings
using System.Runtime.CompilerServices;
#endregion

namespace Shield.Framework.Validation.Extensions
{
    public static class ObjectExtensions
    {
        #region Methods
        public static IValidationTarget<T> Guard<T>(this T obj,
                                                    [CallerFilePath] string file = null,
                                                    [CallerMemberName] string source = null,
                                                    [CallerLineNumber] int line = -1)
        {
            return new ValidationTarget<T>(obj, typeof(T).Name, file, source, line);
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