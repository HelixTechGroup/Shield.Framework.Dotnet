using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Collections
{
    public static class DictionaryValidators
    {
        public static ValidationRule<TType> ContainsValue<TType>(this ValidationRule<TType> rule, object value) where TType : IDictionary
        {
            rule.AddValidator(new RuleValidator<TType>(v => v.Contains(value), 
                                                       ExceptionMessages.CollectionsContainsValueFailed.Inject(value)));

            return rule;
        }

        public static ValidationRule<TType> DoesNotContainValue<TType>(this ValidationRule<TType> rule, object value) where TType : IDictionary
        {
            rule.AddValidator(new RuleValidator<TType>(v => !v.Contains(value),
                                                       ExceptionMessages.CollectionsDoesNotContainValueFailed.Inject(value)));

            return rule;
        }

        public static ValidationRule<TType> ContainsValue<TType>(this ValidationRule<TType> rule, Func<object, bool> predicate) where TType : IDictionary
        {
            rule.AddValidator(new RuleValidator<TType>(v => v.Cast<object>().Any(predicate),
                                                       ExceptionMessages.CollectionsAnyFailed));

            return rule;
        }

        public static ValidationRule<TType> DoesNotContainValue<TType>(this ValidationRule<TType> rule, Func<object, bool> predicate) where TType : IDictionary
        {
            rule.AddValidator(new RuleValidator<TType>(v => !v.Cast<object>().Any(predicate),
                                                       ExceptionMessages.CollectionsNotAnyFailed));

            return rule;
        }

        public static ValidationRule<TType> ContainsKey<TType>(this ValidationRule<TType> rule, object key) where TType : IDictionary
        {
            rule.AddValidator(new RuleValidator<TType>(v =>
            {
                return v.Keys.Cast<object>().Any(k => k == key);
            }, ExceptionMessages.DictionariesContainsKeyFailed.Inject(key)));

            return rule;
        }

        public static ValidationRule<TType> DoesNotContainKey<TType>(this ValidationRule<TType> rule, object key) where TType : IDictionary
        {
            rule.AddValidator(new RuleValidator<TType>(v =>
            {
                return v.Keys.Cast<object>().All(k => k != key);
            }, ExceptionMessages.DictionariesDoesNotContainKeyFailed.Inject(key)));

            return rule;
        }

        public static ValidationRule<TType> ContainsKey<TType>(this ValidationRule<TType> rule, Func<object, bool> predicate) where TType : IDictionary
        {
            rule.AddValidator(new RuleValidator<TType>(v => v.Keys.Cast<object>().Any(predicate), 
                                                       ExceptionMessages.DictionaryAnyKeyFailed));

            return rule;
        }

        public static ValidationRule<TType> DoesNotContainKey<TType>(this ValidationRule<TType> rule, Func<object, bool> predicate) where TType : IDictionary
        {
            rule.AddValidator(new RuleValidator<TType>(v => !v.Keys.Cast<object>().All(predicate), 
                                                       ExceptionMessages.DictionaryNotAnyKeyFailed));

            return rule;
        }

        public static ValidationRule<IDictionary<TKey, TType>> ContainsValue<TKey, TType>(this ValidationRule<IDictionary<TKey, TType>> rule, TType value)
        {
            rule.AddValidator(new RuleValidator<IDictionary<TKey, TType>>(v => v.Values.Contains(value),
                                                       ExceptionMessages.CollectionsContainsValueFailed.Inject(value)));

            return rule;
        }

        public static ValidationRule<IDictionary<TKey, TType>> DoesNotContainValue<TKey, TType>(this ValidationRule<IDictionary<TKey, TType>> rule, TType value)
        {
            rule.AddValidator(new RuleValidator<IDictionary<TKey, TType>>(v => !v.Values.Contains(value),
                                                       ExceptionMessages.CollectionsDoesNotContainValueFailed.Inject(value)));

            return rule;
        }

        public static ValidationRule<IDictionary<TKey, TType>> ContainsValue<TKey, TType>(this ValidationRule<IDictionary<TKey, TType>> rule, Func<TType, bool> predicate)
        {
            rule.AddValidator(new RuleValidator<IDictionary<TKey, TType>>(v => v.Values.Any(predicate),
                                                       ExceptionMessages.CollectionsAnyFailed));

            return rule;
        }

        public static ValidationRule<IDictionary<TKey, TType>> DoesNotContainValue<TKey, TType>(this ValidationRule<IDictionary<TKey, TType>> rule, Func<TType, bool> predicate)
        {
            rule.AddValidator(new RuleValidator<IDictionary<TKey, TType>>(v => !v.Values.Any(predicate),
                                                       ExceptionMessages.CollectionsNotAnyFailed));

            return rule;
        }

        public static ValidationRule<IDictionary<TKey, TType>> ContainsKey<TKey, TType>(this ValidationRule<IDictionary<TKey, TType>> rule, TKey key)
        {
            rule.AddValidator(new RuleValidator<IDictionary<TKey, TType>>(v =>
            {
                return v.Keys.Any(k => k.Equals(key));
            }, ExceptionMessages.DictionariesContainsKeyFailed.Inject(key)));

            return rule;
        }

        public static ValidationRule<IDictionary<TKey, TType>> DoesNotContainKey<TKey, TType>(this ValidationRule<IDictionary<TKey, TType>> rule, TKey key)
        {
            rule.AddValidator(new RuleValidator<IDictionary<TKey, TType>>(v =>
            {
                return v.Keys.All(k => !k.Equals(key));
            }, ExceptionMessages.DictionariesDoesNotContainKeyFailed.Inject(key)));

            return rule;
        }

        public static ValidationRule<IDictionary<TKey, TType>> ContainsKey<TKey, TType>(this ValidationRule<IDictionary<TKey, TType>> rule, Func<TKey, bool> predicate)
        {
            rule.AddValidator(new RuleValidator<IDictionary<TKey, TType>>(v => v.Keys.Any(predicate),
                                                       ExceptionMessages.DictionaryAnyKeyFailed));

            return rule;
        }

        public static ValidationRule<IDictionary<TKey, TType>> DoesNotContainKey<TKey, TType>(this ValidationRule<IDictionary<TKey, TType>> rule, Func<TKey, bool> predicate)
        {
            rule.AddValidator(new RuleValidator<IDictionary<TKey, TType>>(v => !v.Keys.All(predicate),
                                                       ExceptionMessages.DictionaryNotAnyKeyFailed));

            return rule;
        }
    }
}