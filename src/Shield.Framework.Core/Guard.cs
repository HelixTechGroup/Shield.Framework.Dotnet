#region Usings
using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Shield.Framework.Validation;
#endregion

namespace Shield.Framework
{
    public static class Guard
    {
        #region Methods
        public static IValidationTarget<T> Target<T>(T value,
                                                     [CallerFilePath] string file = null,
                                                     [CallerMemberName] string source = null,
                                                     [CallerLineNumber] int line = -1)
        {
            return Target(value, typeof(T).Name, file, source, line);
        }

        public static IValidationTarget<T> Target<T>(T value,
                                                     string name,
                                                     [CallerFilePath] string file = null,
                                                     [CallerMemberName] string source = null,
                                                     [CallerLineNumber] int line = -1)
        {
            return new ValidationTarget<T>(value, name, file, source, line);
        }

        public static IValidationTarget<T> Target<T>(Expression<Func<T>> memberExpression,
                                                     [CallerFilePath] string file = null,
                                                     [CallerMemberName] string source = null,
                                                     [CallerLineNumber] int line = -1)
        {
            return new ValidationTarget<T>(memberExpression, file, source, line);
        }
        #endregion
    }
}