using System;
using System.Collections.Generic;
using System.Text;

namespace Shield.Framework.Extensions
{
    internal static class ObjectExtensions
    {
        internal static void ThrowIfNull<T>(this T obj)
        {
            if (obj == null)
                throw new NullReferenceException();
        }

        internal static void ThrowIfNull<T>(this T obj, string message)
        {
            if (obj == null)
                throw new NullReferenceException(message);
        }
    }
}
