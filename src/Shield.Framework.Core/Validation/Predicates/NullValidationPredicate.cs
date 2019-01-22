using System;

namespace Shield.Framework.Validation.Predicates
{
    public sealed class NullValidationPredicate<TType> : ValidationPredicate<TType, ArgumentNullException>
    {
        public NullValidationPredicate(Predicate<TType> predicate) : base(predicate) { }

        public NullValidationPredicate(Predicate<TType> predicate, string message) : base(predicate, message) { }
    }
}
