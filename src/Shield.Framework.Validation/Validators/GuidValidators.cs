using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;

namespace Shield.Framework.Validation.Validators
{
    public static class GuidValidators
    {

        public static IValidationTarget<Guid> IsEmpty(this IValidationTarget<Guid> target)
        {
            return target.And(new DefaultValidationPredicate<Guid>(v => v.Equals(Guid.Empty),
                                                      ExceptionMessages.GuidsIsEmptyFailed));
        }

        public static IValidationTarget<Guid> IsNotEmpty(this IValidationTarget<Guid> target)
        {
            return target.And(new NullValidationPredicate<Guid>(v => !v.Equals(Guid.Empty), 
                                                      ExceptionMessages.GuidsIsNotEmptyFailed));
        }

        public static IValidationTarget<Guid> IsEqual(this IValidationTarget<Guid> target, Guid guid)
        {
            return target.And(new DefaultValidationPredicate<Guid>(v => v.Equals(guid),
                                                      ExceptionMessages.GuidsIsEqualToFailed.Inject(target.Value, guid)));
        }

        public static IValidationTarget<Guid> IsNotEqual(this IValidationTarget<Guid> target, Guid guid)
        {
            return target.And(new DefaultValidationPredicate<Guid>(v => !v.Equals(guid),
                                                      ExceptionMessages.GuidsIsNotEqualToFailed.Inject(target.Value, guid)));
        }
    }
}