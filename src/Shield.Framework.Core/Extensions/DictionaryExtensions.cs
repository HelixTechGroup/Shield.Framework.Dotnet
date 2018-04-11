using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Shield.Framework.Extensions
{
    public static class DictionaryExtensions
    {
        public static IDictionary AddRange<TKey, TValue>(this IDictionary dictionary, params KeyValuePair<TKey, TValue>[] items)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var each in items)
            {
                if (dictionary.Contains(each.Key))
                    continue;

                dictionary.Add(each.Key, each.Value);
            }

            return dictionary;
        }

        public static IDictionary AddRange<TKey, TValue>(this IDictionary dictionary, IDictionary<TKey, TValue> items)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var each in items)
            {
                if (dictionary.Contains(each.Key))
                    continue;

                dictionary.Add(each.Key, each.Value);
            }

            return dictionary;
        }
    }
}