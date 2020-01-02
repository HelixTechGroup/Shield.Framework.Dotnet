#region Usings
using System.Collections.Generic;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators.Collections
{
    public static class EnumerableValidators
    {
        #region Methods
        public static IValidationTarget<IEnumerable<TType>> IsEmpty<TType>(this IValidationTarget<IEnumerable<TType>> target)
        {
            return target.And(new DefaultValidationPredicate<IEnumerable<TType>>(v => v.IsEmpty(),
                                                                                 ExceptionMessages.CollectionsIsEmptyFailed));
        }

        public static IValidationTarget<IEnumerable<TType>> IsNotEmpty<TType>(this IValidationTarget<IEnumerable<TType>> target)
        {
            return target.And(new DefaultValidationPredicate<IEnumerable<TType>>(v => !v.IsEmpty(),
                                                                                 ExceptionMessages.CollectionsHasItemsFailed));
        }
        #endregion
    }
}