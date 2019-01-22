#region Usings
using Shield.Framework.Validation.Exceptions;
using Shield.Framework.Validation.Predicates;
#endregion

namespace Shield.Framework.Validation.Validators
{
    public static class BooleanValidators
    {
        #region Methods
        public static IValidationTarget<bool> IsTrue(this IValidationTarget<bool> target)
        {
            return target.And(new DefaultValidationPredicate<bool>(v => v, ExceptionMessages.BooleansIsTrueFailed));
        }

        public static IValidationTarget<bool> IsFalse(this IValidationTarget<bool> target)
        {
            return target.And(new DefaultValidationPredicate<bool>(v => !v, ExceptionMessages.BooleansIsFalseFailed));
        }

        public static IValidationTarget<bool?> IsTrue(this IValidationTarget<bool?> target)
        {
            return target.And(new DefaultValidationPredicate<bool?>(v => v.HasValue && (bool)v, ExceptionMessages.BooleansIsTrueFailed));
        }

        public static IValidationTarget<bool?> IsFalse(this IValidationTarget<bool?> target)
        {
            return target.And(new DefaultValidationPredicate<bool?>(v => v.HasValue && (bool)!v, ExceptionMessages.BooleansIsFalseFailed));
        }
        #endregion
    }
}