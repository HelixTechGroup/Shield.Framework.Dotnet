#region Usings
using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators.Collections
{
    public static class ArrayValidators
    {
        #region Methods
        public static IValidationTarget<TType[]> IsNotReadOnly<TType>(this IValidationTarget<TType[]> target)
        {
            return target.And(new DefaultValidationPredicate<TType[]>(v => !v.IsReadOnly,
                                                                      ExceptionMessages.CollectionsIsNotReadOnlyFailed));
        }

        public static IValidationTarget<TType[]> IsReadOnly<TType>(this IValidationTarget<TType[]> target)
        {
            return target.And(new DefaultValidationPredicate<TType[]>(v => v.IsReadOnly,
                                                                      ExceptionMessages.CollectionsIsReadOnlyFailed));
        }

        public static IValidationTarget<TType[]> IsNotFixedSize<TType>(this IValidationTarget<TType[]> target)
        {
            return target.And(new DefaultValidationPredicate<TType[]>(v => !v.IsFixedSize,
                                                                      ExceptionMessages.CollectionsIsNotFixedSizeFailed.Inject(
                                                                                                                               target.Value.Length)));
        }

        public static IValidationTarget<TType[]> IsFixedSize<TType>(this IValidationTarget<TType[]> target)
        {
            return target.And(new DefaultValidationPredicate<TType[]>(v => v.IsFixedSize,
                                                                      ExceptionMessages.CollectionsIsFixedSizeFailed));
        }

        public static IValidationTarget<TType[]> ContainsValue<TType>(this IValidationTarget<TType[]> target, Predicate<TType> predicate)
        {
            return target.And(new DefaultValidationPredicate<TType[]>(v => Array.Exists(v, predicate),
                                                                      ExceptionMessages.CollectionsAnyFailed));
        }

        public static IValidationTarget<TType[]> DoesNotContainValue<TType>(this IValidationTarget<TType[]> target,
                                                                            Predicate<TType> predicate)
        {
            return target.And(new DefaultValidationPredicate<TType[]>(v => !Array.Exists(v, predicate),
                                                                      ExceptionMessages.CollectionsNotAnyFailed));
        }
        #endregion
    }
}