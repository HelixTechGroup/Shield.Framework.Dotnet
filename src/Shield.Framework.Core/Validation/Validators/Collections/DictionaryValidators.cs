#region Usings
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators.Collections
{
    public static class DictionaryValidators
    {
        #region Methods
        public static IValidationTarget<TType> ContainsValue<TType>(this IValidationTarget<TType> target, object value)
            where TType : IDictionary
        {
            return target.And(new DefaultValidationPredicate<TType>(v => v.Contains(value),
                                                                    ExceptionMessages.CollectionsContainsValueFailed.Inject(value)));
        }

        public static IValidationTarget<TType> DoesNotContainValue<TType>(this IValidationTarget<TType> target, object value)
            where TType : IDictionary
        {
            return target.And(new DefaultValidationPredicate<TType>(v => !v.Contains(value),
                                                                    ExceptionMessages.CollectionsDoesNotContainValueFailed.Inject(value)));
        }

        public static IValidationTarget<TType> ContainsValue<TType>(this IValidationTarget<TType> target, Func<object, bool> predicate)
            where TType : IDictionary
        {
            return target.And(new DefaultValidationPredicate<TType>(v => v.Cast<object>().Any(predicate),
                                                                    ExceptionMessages.CollectionsAnyFailed));
        }

        public static IValidationTarget<TType> DoesNotContainValue<TType>(this IValidationTarget<TType> target, Func<object, bool> predicate)
            where TType : IDictionary
        {
            return target.And(new DefaultValidationPredicate<TType>(v => !v.Cast<object>().Any(predicate),
                                                                    ExceptionMessages.CollectionsNotAnyFailed));
        }

        public static IValidationTarget<TType> ContainsKey<TType>(this IValidationTarget<TType> target, object key) where TType : IDictionary
        {
            return target.And(new DefaultValidationPredicate<TType>(v => { return v.Keys.Cast<object>().Any(k => k == key); },
                                                                    ExceptionMessages.DictionariesContainsKeyFailed.Inject(key)));
        }

        public static IValidationTarget<TType> DoesNotContainKey<TType>(this IValidationTarget<TType> target, object key)
            where TType : IDictionary
        {
            return target.And(new DefaultValidationPredicate<TType>(v => { return v.Keys.Cast<object>().All(k => k != key); },
                                                                    ExceptionMessages.DictionariesDoesNotContainKeyFailed.Inject(key)));
        }

        public static IValidationTarget<TType> ContainsKey<TType>(this IValidationTarget<TType> target, Func<object, bool> predicate)
            where TType : IDictionary
        {
            return target.And(new DefaultValidationPredicate<TType>(v => v.Keys.Cast<object>().Any(predicate),
                                                                    ExceptionMessages.DictionaryAnyKeyFailed));
        }

        public static IValidationTarget<TType> DoesNotContainKey<TType>(this IValidationTarget<TType> target, Func<object, bool> predicate)
            where TType : IDictionary
        {
            return target.And(new DefaultValidationPredicate<TType>(v => !v.Keys.Cast<object>().All(predicate),
                                                                    ExceptionMessages.DictionaryNotAnyKeyFailed));
        }

        public static IValidationTarget<IDictionary<TKey, TType>> ContainsValue<TKey, TType>(
            this IValidationTarget<IDictionary<TKey, TType>> target,
            TType value)
        {
            return target.And(new DefaultValidationPredicate<IDictionary<TKey, TType>>(v => v.Values.Contains(value),
                                                                                       ExceptionMessages.CollectionsContainsValueFailed
                                                                                           .Inject(value)));
        }

        public static IValidationTarget<IDictionary<TKey, TType>> DoesNotContainValue<TKey, TType>(
            this IValidationTarget<IDictionary<TKey, TType>> target,
            TType value)
        {
            return target.And(new DefaultValidationPredicate<IDictionary<TKey, TType>>(v => !v.Values.Contains(value),
                                                                                       ExceptionMessages
                                                                                           .CollectionsDoesNotContainValueFailed
                                                                                           .Inject(value)));
        }

        public static IValidationTarget<IDictionary<TKey, TType>> ContainsValue<TKey, TType>(
            this IValidationTarget<IDictionary<TKey, TType>> target,
            Func<TType, bool> predicate)
        {
            return target.And(new DefaultValidationPredicate<IDictionary<TKey, TType>>(v => v.Values.Any(predicate),
                                                                                       ExceptionMessages.CollectionsAnyFailed));
        }

        public static IValidationTarget<IDictionary<TKey, TType>> DoesNotContainValue<TKey, TType>(
            this IValidationTarget<IDictionary<TKey, TType>> target,
            Func<TType, bool> predicate)
        {
            return target.And(new DefaultValidationPredicate<IDictionary<TKey, TType>>(v => !v.Values.Any(predicate),
                                                                                       ExceptionMessages.CollectionsNotAnyFailed));
        }

        public static IValidationTarget<IDictionary<TKey, TType>> ContainsKey<TKey, TType>(
            this IValidationTarget<IDictionary<TKey, TType>> target,
            TKey key)
        {
            return target.And(new DefaultValidationPredicate<IDictionary<TKey, TType>>(v => { return v.Keys.Any(k => k.Equals(key)); },
                                                                                       ExceptionMessages.DictionariesContainsKeyFailed
                                                                                           .Inject(key)));
        }

        public static IValidationTarget<IDictionary<TKey, TType>> DoesNotContainKey<TKey, TType>(
            this IValidationTarget<IDictionary<TKey, TType>> target,
            TKey key)
        {
            return target.And(new DefaultValidationPredicate<IDictionary<TKey, TType>>(v => { return v.Keys.All(k => !k.Equals(key)); },
                                                                                       ExceptionMessages.DictionariesDoesNotContainKeyFailed
                                                                                           .Inject(key)));
        }

        public static IValidationTarget<IDictionary<TKey, TType>> ContainsKey<TKey, TType>(
            this IValidationTarget<IDictionary<TKey, TType>> target,
            Func<TKey, bool> predicate)
        {
            return target.And(new DefaultValidationPredicate<IDictionary<TKey, TType>>(v => v.Keys.Any(predicate),
                                                                                       ExceptionMessages.DictionaryAnyKeyFailed));
        }

        public static IValidationTarget<IDictionary<TKey, TType>> DoesNotContainKey<TKey, TType>(
            this IValidationTarget<IDictionary<TKey, TType>> target,
            Func<TKey, bool> predicate)
        {
            return target.And(new DefaultValidationPredicate<IDictionary<TKey, TType>>(v => !v.Keys.All(predicate),
                                                                                       ExceptionMessages.DictionaryNotAnyKeyFailed));
        }
        #endregion
    }
}