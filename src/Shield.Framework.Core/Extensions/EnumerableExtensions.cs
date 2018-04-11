using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Shield.Framework.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty(this IEnumerable enumerable)
        {
            var enumerator = enumerable.GetEnumerator();
            try
            {
                return !enumerator.MoveNext();
            }
            finally
            {
                if (enumerator is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }
}
