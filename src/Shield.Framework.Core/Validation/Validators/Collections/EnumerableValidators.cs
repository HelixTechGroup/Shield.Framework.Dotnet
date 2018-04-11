using System.Collections.Generic;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Collections
{
    public static class EnumerableValidators
    {
        public static ValidationRule<IEnumerable<TType>> IsEmpty<TType>(this ValidationRule<IEnumerable<TType>> rule)
        {
            rule.AddValidator(new RuleValidator<IEnumerable<TType>>(v => v.IsEmpty(), 
                                                                    ExceptionMessages.CollectionsIsEmptyFailed));

            return rule;
        }

        public static ValidationRule<IEnumerable<TType>> IsNotEmpty<TType>(this ValidationRule<IEnumerable<TType>> rule)
        {
            rule.AddValidator(new RuleValidator<IEnumerable<TType>>(v => !v.IsEmpty(), 
                                                                    ExceptionMessages.CollectionsHasItemsFailed));

            return rule;
        }        
    }
}