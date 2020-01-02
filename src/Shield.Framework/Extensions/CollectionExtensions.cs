using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Shield.Framework.Extensions
{
    public static class CollectionExtensions
    {
        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var each in items)
                collection.Add(each);

            return collection;
        }
    }
}
