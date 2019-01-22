using System;
using System.Collections.Generic;
using System.Text;

namespace Shield.Framework.Validation.Predicates
{
    public sealed class OutOfRangeValidationPredicate<TType> : ValidationPredicate<TType, ArgumentOutOfRangeException>
    {
        public OutOfRangeValidationPredicate(Predicate<TType> predicate) : base(predicate) { }

        public OutOfRangeValidationPredicate(Predicate<TType> predicate, string message) : base(predicate, message) { }
    }
}
