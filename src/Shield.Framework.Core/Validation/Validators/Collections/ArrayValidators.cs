using System;
using Shield.Framework.Extensions;
using Shield.Framework.Validation.Exceptions;

namespace Shield.Framework.Validation.Validators.Collections
{
    public static class ArrayValidators
    {
        public static ValidationRule<TType[]> IsNotReadOnly<TType>(this ValidationRule<TType[]> rule)
        {
            rule.AddValidator(new RuleValidator<TType[]>(v => !v.IsReadOnly, 
                                                         ExceptionMessages.CollectionsIsNotReadOnlyFailed));

            return rule;
        }

        public static ValidationRule<TType[]> IsReadOnly<TType>(this ValidationRule<TType[]> rule)
        {
            rule.AddValidator(new RuleValidator<TType[]>(v => v.IsReadOnly,
                                                         ExceptionMessages.CollectionsIsReadOnlyFailed));

            return rule;
        }

        public static ValidationRule<TType[]> IsNotFixedSize<TType>(this ValidationRule<TType[]> rule)
        {
            rule.AddValidator(new RuleValidator<TType[]>(v => !v.IsFixedSize, 
                                                         ExceptionMessages.CollectionsIsNotFixedSizeFailed.Inject(rule.Value.Length)));

            return rule;
        }

        public static ValidationRule<TType[]> IsFixedSize<TType>(this ValidationRule<TType[]> rule)
        {
            rule.AddValidator(new RuleValidator<TType[]>(v => v.IsFixedSize,
                                                         ExceptionMessages.CollectionsIsFixedSizeFailed));

            return rule;
        }

        public static ValidationRule<TType[]> ContainsValue<TType>(this ValidationRule<TType[]> rule, Predicate<TType> predicate)
        {
            rule.AddValidator(new RuleValidator<TType[]>(v => Array.Exists(v, predicate), 
                                                         ExceptionMessages.CollectionsAnyFailed));

            return rule;
        }

        public static ValidationRule<TType[]> DoesNotContainValue<TType>(this ValidationRule<TType[]> rule, Predicate<TType> predicate)
        {
            rule.AddValidator(new RuleValidator<TType[]>(v => !Array.Exists(v, predicate),
                                                         ExceptionMessages.CollectionsNotAnyFailed));

            return rule;
        }
    }
}