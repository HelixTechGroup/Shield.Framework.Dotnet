using System;
using Shield.Framework.Validation;

namespace Shield.Framework
{
    public static class Guard
    {
        public static ValidationRule<T> Type<T>()
        {
            return new ValidationRule<T>();
        }

        public static ValidationRule<bool> Bool
        {
            get { return new ValidationRule<bool>(); }
        }

        public static CollectionsGuard Collections
        {
            get { return new CollectionsGuard(); }
        }                

        public static ValidationRule<Guid> Guid
        {
            get { return new ValidationRule<Guid>(); }
        }

        public static NumbersGuard Numbers
        {
            get { return new NumbersGuard(); }
        }

        public static ValidationRule<string> String
        {
            get { return new ValidationRule<string>(); }
        }

        public static TimeGuard Time
        {
            get { return new TimeGuard(); }
        }
    }
}