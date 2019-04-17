#region Usings
using System;
using System.Collections.Generic;
using Shield.Framework.Exceptions;
#endregion

namespace Shield.Framework
{
    internal static partial class Throw
    {
        #region Methods
        public static void If<TException>(Func<bool> predicate,
                                          string message = null,
                                          object[] args = null,
                                          params KeyValuePair<string, object>[] data)
            where TException : Exception, new()
        {
            if (predicate())
                throw ExceptionProvider.GenerateException<TException>(message, args, data);
        }

        public static void If<TException>(bool predicate,
                                          string message = null,
                                          object[] args = null,
                                          params KeyValuePair<string, object>[] data)
            where TException : Exception, new()
        {
            if (predicate)
                throw ExceptionProvider.GenerateException<TException>(message, args, data);
        }

        public static ThrowExceptionSelector If(Func<bool> predicate)
        {
            return new ThrowExceptionSelector(predicate());
        }

        public static ThrowExceptionSelector If(bool predicate)
        {
            return new ThrowExceptionSelector(predicate);
        }

        public static void IfNot<TException>(Func<bool> predicate,
                                             string message = null,
                                             object[] args = null,
                                             params KeyValuePair<string, object>[] data)
            where TException : Exception, new()
        {
            if (!predicate())
                throw ExceptionProvider.GenerateException<TException>(message, args, data);
        }

        public static void IfNot<TException>(bool predicate,
                                             string message = null,
                                             object[] args = null,
                                             params KeyValuePair<string, object>[] data)
            where TException : Exception, new()
        {
            if (!predicate)
                throw ExceptionProvider.GenerateException<TException>(message, args, data);
        }

        public static ThrowExceptionSelector IfNot(Func<bool> predicate)
        {
            return new ThrowExceptionSelector(!predicate());
        }

        public static ThrowExceptionSelector IfNot(bool predicate)
        {
            return new ThrowExceptionSelector(!predicate);
        }
        #endregion
    }
}