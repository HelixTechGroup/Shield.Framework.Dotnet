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
    public static class CollectionValidators
    {
        #region Methods
        public static IValidationTarget<TType> IsEmpty<TType>(this IValidationTarget<TType> target) where TType : ICollection
        {
            return target.And(new DefaultValidationPredicate<TType>(v => v.Count == 0,
                                                                    ExceptionMessages.CollectionsIsEmptyFailed));
        }

        public static IValidationTarget<TType> IsNotEmpty<TType>(this IValidationTarget<TType> target) where TType : ICollection
        {
            return target.And(new DefaultValidationPredicate<TType>(v => v.Count > 0,
                                                                    ExceptionMessages.CollectionsHasItemsFailed));
        }

        public static IValidationTarget<TType> IsSize<TType>(this IValidationTarget<TType> target, int expected) where TType : ICollection
        {
            return target.And(new DefaultValidationPredicate<TType>(v => v.Count == expected,
                                                                    ExceptionMessages.CollectionsSizeIsFailed.Inject(
                                                                                                                     expected,
                                                                                                                     target.Value.Count)));
        }

        public static IValidationTarget<TType> HasMininumCount<TType>(this IValidationTarget<TType> target, int minCount)
            where TType : ICollection
        {
            return target.And(new OutOfRangeValidationPredicate<TType>(v => v.Count >= minCount,
                                                                       ExceptionMessages.CollectionsMinimumSizeIsFailed.Inject(
                                                                                                                               minCount,
                                                                                                                               target.Value.Count)));
        }

        public static IValidationTarget<TType> HasMaximumCount<TType>(this IValidationTarget<TType> target, int maxCount)
            where TType : ICollection
        {
            return target.And(new OutOfRangeValidationPredicate<TType>(v => v.Count <= maxCount,
                                                                       ExceptionMessages.CollectionsMaximumSizeIsFailed.Inject(
                                                                                                                               maxCount,
                                                                                                                               target.Value.Count)));
        }

        public static IValidationTarget<TType> HasCountBetween<TType>(this IValidationTarget<TType> target, int minCount, int maxCount)
            where TType : ICollection
        {
            return target.And(new OutOfRangeValidationPredicate<TType>(v => v.Count < minCount,
                                                                       ExceptionMessages.CollectionsHasSizeBetweenFailedToShort.Inject(
                                                                                                                                       minCount,
                                                                                                                                       maxCount,
                                                                                                                                       target
                                                                                                                                           .Value
                                                                                                                                           .Count)))
                         .And(new OutOfRangeValidationPredicate<TType>(v => v.Count > maxCount,
                                                                       ExceptionMessages.CollectionsHasSizeBetweenFailedToLong.Inject(
                                                                                                                                      minCount,
                                                                                                                                      maxCount,
                                                                                                                                      target
                                                                                                                                          .Value
                                                                                                                                          .Count)));
        }

        public static IValidationTarget<TType> ContainsValue<TType>(this IValidationTarget<TType> target, Func<object, bool> predicate)
            where TType : ICollection
        {
            return target.And(new DefaultValidationPredicate<TType>(v => v.Cast<object>().Any(predicate),
                                                                    ExceptionMessages.CollectionsAnyFailed));
        }

        public static IValidationTarget<TType> DoesNotContainValue<TType>(this IValidationTarget<TType> target, Func<object, bool> predicate)
            where TType : ICollection
        {
            return target.And(new DefaultValidationPredicate<TType>(v => !v.Cast<object>().Any(predicate),
                                                                    ExceptionMessages.CollectionsNotAnyFailed));
        }

        public static IValidationTarget<TType> IsSynchronized<TType>(this IValidationTarget<TType> target) where TType : ICollection
        {
            return target.And(new DefaultValidationPredicate<TType>(v => v.IsSynchronized,
                                                                    ExceptionMessages.CollectionsSynchronizationFailed));
        }

        public static IValidationTarget<ICollection<TType>> IsEmpty<TType>(this IValidationTarget<ICollection<TType>> target)
        {
            return target.And(new DefaultValidationPredicate<ICollection<TType>>(v => v.Count == 0,
                                                                                 ExceptionMessages.CollectionsIsEmptyFailed));
        }

        public static IValidationTarget<ICollection<TType>> IsNotEmpty<TType>(this IValidationTarget<ICollection<TType>> target)
        {
            return target.And(new DefaultValidationPredicate<ICollection<TType>>(v => v.Count > 0,
                                                                                 ExceptionMessages.CollectionsHasItemsFailed));
        }

        public static IValidationTarget<ICollection<TType>> IsSize<TType>(this IValidationTarget<ICollection<TType>> target, int expected)
        {
            return target.And(new DefaultValidationPredicate<ICollection<TType>>(v => v.Count == expected,
                                                                                 ExceptionMessages.CollectionsSizeIsFailed.Inject(
                                                                                                                                  expected,
                                                                                                                                  target
                                                                                                                                      .Value.Count)));
        }

        public static IValidationTarget<ICollection<TType>> HasMininumCount<TType>(this IValidationTarget<ICollection<TType>> target,
                                                                                   int minCount)
        {
            return target.And(new OutOfRangeValidationPredicate<ICollection<TType>>(v => v.Count >= minCount,
                                                                                    ExceptionMessages.CollectionsMinimumSizeIsFailed.Inject(
                                                                                                                                            minCount,
                                                                                                                                            target
                                                                                                                                                .Value
                                                                                                                                                .Count)));
        }

        public static IValidationTarget<ICollection<TType>> HasMaximumCount<TType>(this IValidationTarget<ICollection<TType>> target,
                                                                                   int maxCount)
        {
            return target.And(new OutOfRangeValidationPredicate<ICollection<TType>>(v => v.Count <= maxCount,
                                                                                    ExceptionMessages.CollectionsMaximumSizeIsFailed.Inject(
                                                                                                                                            maxCount,
                                                                                                                                            target
                                                                                                                                                .Value
                                                                                                                                                .Count)));
        }

        public static IValidationTarget<ICollection<TType>> HasCountBetween<TType>(this IValidationTarget<ICollection<TType>> target,
                                                                                   int minCount,
                                                                                   int maxCount)
        {
            return target.And(new OutOfRangeValidationPredicate<ICollection<TType>>(v => v.Count < minCount,
                                                                                    ExceptionMessages.CollectionsHasSizeBetweenFailedToShort
                                                                                                     .Inject(minCount, maxCount, target.Value.Count)))
                         .And(new OutOfRangeValidationPredicate<ICollection<TType>>(v => v.Count > maxCount,
                                                                                    ExceptionMessages.CollectionsHasSizeBetweenFailedToLong.Inject(
                                                                                                                                                   minCount,
                                                                                                                                                   maxCount,
                                                                                                                                                   target
                                                                                                                                                       .Value
                                                                                                                                                       .Count)));
        }

        public static IValidationTarget<ICollection<TType>> ContainsValue<TType>(this IValidationTarget<ICollection<TType>> target,
                                                                                 Func<TType, bool> predicate)
        {
            return target.And(new DefaultValidationPredicate<ICollection<TType>>(v => v.Any(predicate),
                                                                                 ExceptionMessages.CollectionsAnyFailed));
        }

        public static IValidationTarget<ICollection<TType>> DoesNotContainValue<TType>(
            this IValidationTarget<ICollection<TType>> target,
            Func<TType, bool> predicate)
        {
            return target.And(new DefaultValidationPredicate<ICollection<TType>>(v => !v.Any(predicate),
                                                                                 ExceptionMessages.CollectionsNotAnyFailed));
        }

        public static IValidationTarget<ICollection<TType>> ContainsValue<TType>(this IValidationTarget<ICollection<TType>> target,
                                                                                 TType value)
        {
            return target.And(new DefaultValidationPredicate<ICollection<TType>>(v => v.Contains(value),
                                                                                 ExceptionMessages.CollectionsContainsValueFailed.Inject(
                                                                                                                                         value)));
        }

        public static IValidationTarget<ICollection<TType>> DoesNotContainValue<TType>(
            this IValidationTarget<ICollection<TType>> target,
            TType value)
        {
            return target.And(new DefaultValidationPredicate<ICollection<TType>>(v => !v.Contains(value),
                                                                                 ExceptionMessages.CollectionsDoesNotContainValueFailed));
        }

        public static IValidationTarget<ICollection<TType>> IsReadOnly<TType>(this IValidationTarget<ICollection<TType>> target)
        {
            return target.And(new DefaultValidationPredicate<ICollection<TType>>(v => v.IsReadOnly,
                                                                                 ExceptionMessages.CollectionsIsReadOnlyFailed));
        }

        public static IValidationTarget<ICollection<TType>> IsNotReadOnly<TType>(this IValidationTarget<ICollection<TType>> target)
        {
            return target.And(new DefaultValidationPredicate<ICollection<TType>>(v => !v.IsReadOnly,
                                                                                 ExceptionMessages.CollectionsIsNotReadOnlyFailed));
        }
        #endregion
    }
}