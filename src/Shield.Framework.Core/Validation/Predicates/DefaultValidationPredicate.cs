using System;
using System.Collections.Generic;
using System.Text;

namespace Shield.Framework.Validation.Predicates
{
    public sealed class DefaultValidationPredicate<TType> : ValidationPredicate<TType, ArgumentException>
    {
        public DefaultValidationPredicate(Predicate<TType> predicate) : base(predicate) { }

        public DefaultValidationPredicate(Predicate<TType> predicate, string message) : base(predicate, message) { }
    }
}
