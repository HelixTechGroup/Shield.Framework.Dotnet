using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Collections
{
    public static class CollectionValidators
    {
        public static ValidationRule<TType> IsEmpty<TType>(this ValidationRule<TType> rule) where TType : ICollection
        {
            rule.AddValidator(new RuleValidator<TType>(v => v.Count == 0, 
                                                       ExceptionMessages.CollectionsIsEmptyFailed));

            return rule;
        }

        public static ValidationRule<TType> IsNotEmpty<TType>(this ValidationRule<TType> rule) where TType : ICollection
        {
            rule.AddValidator(new RuleValidator<TType>(v => v.Count > 0, 
                                                       ExceptionMessages.CollectionsHasItemsFailed));

            return rule;
        }

        public static ValidationRule<TType> IsSize<TType>(this ValidationRule<TType> rule, int expected) where TType : ICollection
        {
            rule.AddValidator(new RuleValidator<TType>(v => v.Count == expected, 
                                                       ExceptionMessages.CollectionsSizeIsFailed.Inject(expected, rule.Value.Count)));

            return rule;
        }

        public static ValidationRule<TType> HasMininumCount <TType>(this ValidationRule<TType> rule, int minCount) where TType : ICollection
        {
            rule.AddValidator(new RuleValidator<TType>(v => v.Count >= minCount, 
                                                       ExceptionMessages.CollectionsMinimumSizeIsFailed.Inject(minCount, rule.Value.Count)));

            return rule;
        }

        public static ValidationRule<TType> HasMaximumCount<TType>(this ValidationRule<TType> rule, int maxCount) where TType : ICollection
        {
            rule.AddValidator(new RuleValidator<TType>(v => v.Count <= maxCount, 
                                                       ExceptionMessages.CollectionsMaximumSizeIsFailed.Inject(maxCount, rule.Value.Count)));

            return rule;
        }

        public static ValidationRule<TType> HasCountBetween<TType>(this ValidationRule<TType> rule, int minCount, int maxCount) where TType : ICollection
        {
            rule.AddValidator(new RuleValidator<TType>(v => v.Count < minCount, 
                                                       ExceptionMessages.CollectionsHasSizeBetweenFailedToShort.Inject(minCount, maxCount, rule.Value.Count)));

            rule.AddValidator(new RuleValidator<TType>(v => v.Count > maxCount,
                                                       ExceptionMessages.CollectionsHasSizeBetweenFailedToLong.Inject(minCount, maxCount, rule.Value.Count)));

            return rule;
        }

        public static ValidationRule<TType> ContainsValue<TType>(this ValidationRule<TType> rule, Func<object, bool> predicate) where TType : ICollection
        {
            rule.AddValidator(new RuleValidator<TType>(v => v.Cast<object>().Any(predicate), 
                                                       ExceptionMessages.CollectionsAnyFailed));

            return rule;
        }

        public static ValidationRule<TType> DoesNotContainValue<TType>(this ValidationRule<TType> rule, Func<object, bool> predicate) where TType : ICollection
        {
            rule.AddValidator(new RuleValidator<TType>(v => !v.Cast<object>().Any(predicate),
                                                       ExceptionMessages.CollectionsNotAnyFailed));

            return rule;
        }

        public static ValidationRule<TType> IsSynchronized<TType>(this ValidationRule<TType> rule) where TType : ICollection
        {
            rule.AddValidator(new RuleValidator<TType>(v => v.IsSynchronized, 
                                                       ExceptionMessages.CollectionsSynchronizationFailed));     

            return rule;
        }        

        public static ValidationRule<ICollection<TType>> IsEmpty<TType>(this ValidationRule<ICollection<TType>> rule)
        {
            rule.AddValidator(new RuleValidator<ICollection<TType>>(v => v.Count == 0,
                                                                    ExceptionMessages.CollectionsIsEmptyFailed));

            return rule;
        }

        public static ValidationRule<ICollection<TType>> IsNotEmpty<TType>(this ValidationRule<ICollection<TType>> rule)
        {
            rule.AddValidator(new RuleValidator<ICollection<TType>>(v => v.Count > 0,
                                                                    ExceptionMessages.CollectionsHasItemsFailed));

            return rule;
        }

        public static ValidationRule<ICollection<TType>> IsSize<TType>(this ValidationRule<ICollection<TType>> rule, int expected)
        {
            rule.AddValidator(new RuleValidator<ICollection<TType>>(v => v.Count == expected,
                                                                    ExceptionMessages.CollectionsSizeIsFailed.Inject(expected, rule.Value.Count)));

            return rule;
        }

        public static ValidationRule<ICollection<TType>> HasMininumCount<TType>(this ValidationRule<ICollection<TType>> rule, int minCount)
        {
            rule.AddValidator(new RuleValidator<ICollection<TType>>(v => v.Count >= minCount,
                                                                    ExceptionMessages.CollectionsMinimumSizeIsFailed.Inject(minCount, rule.Value.Count)));

            return rule;
        }

        public static ValidationRule<ICollection<TType>> HasMaximumCount<TType>(this ValidationRule<ICollection<TType>> rule, int maxCount)
        {
            rule.AddValidator(new RuleValidator<ICollection<TType>>(v => v.Count <= maxCount,
                                                                    ExceptionMessages.CollectionsMaximumSizeIsFailed.Inject(maxCount, rule.Value.Count)));

            return rule;
        }

        public static ValidationRule<ICollection<TType>> HasCountBetween<TType>(this ValidationRule<ICollection<TType>> rule, int minCount, int maxCount)
        {
            rule.AddValidator(new RuleValidator<ICollection<TType>>(v => v.Count < minCount,
                                                                    ExceptionMessages.CollectionsHasSizeBetweenFailedToShort.Inject(minCount, maxCount, rule.Value.Count)));

            rule.AddValidator(new RuleValidator<ICollection<TType>>(v => v.Count > maxCount,
                                                                    ExceptionMessages.CollectionsHasSizeBetweenFailedToLong.Inject(minCount, maxCount, rule.Value.Count)));

            return rule;
        }

        public static ValidationRule<ICollection<TType>> ContainsValue<TType>(this ValidationRule<ICollection<TType>> rule, Func<TType, bool> predicate)
        {
            rule.AddValidator(new RuleValidator<ICollection<TType>>(v => v.Any(predicate),
                                                       ExceptionMessages.CollectionsAnyFailed));

            return rule;
        }

        public static ValidationRule<ICollection<TType>> DoesNotContainValue<TType>(this ValidationRule<ICollection<TType>> rule, Func<TType, bool> predicate)
        {
            rule.AddValidator(new RuleValidator<ICollection<TType>>(v => !v.Any(predicate),
                                                       ExceptionMessages.CollectionsNotAnyFailed));

            return rule;
        }

        public static ValidationRule<ICollection<TType>> ContainsValue<TType>(this ValidationRule<ICollection<TType>> rule, TType value)
        {
            rule.AddValidator(new RuleValidator<ICollection<TType>>(v => v.Contains(value),
                                                                    ExceptionMessages.CollectionsContainsValueFailed.Inject(value)));

            return rule;
        }

        public static ValidationRule<ICollection<TType>> DoesNotContainValue<TType>(this ValidationRule<ICollection<TType>> rule, TType value)
        {
            rule.AddValidator(new RuleValidator<ICollection<TType>>(v => !v.Contains(value),
                                                                    ExceptionMessages.CollectionsDoesNotContainValueFailed));

            return rule;
        }

        public static ValidationRule<ICollection<TType>> IsReadOnly<TType>(this ValidationRule<ICollection<TType>> rule)
        {
            rule.AddValidator(new RuleValidator<ICollection<TType>>(v => v.IsReadOnly,
                                                                    ExceptionMessages.CollectionsIsReadOnlyFailed));

            return rule;
        }

        public static ValidationRule<ICollection<TType>> IsNotReadOnly<TType>(this ValidationRule<ICollection<TType>> rule)
        {
            rule.AddValidator(new RuleValidator<ICollection<TType>>(v => !v.IsReadOnly,
                                                                    ExceptionMessages.CollectionsIsNotReadOnlyFailed));

            return rule;
        }
    }
}