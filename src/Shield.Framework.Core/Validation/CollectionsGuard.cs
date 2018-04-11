using System.Collections;
using System.Collections.Generic;

namespace Shield.Framework.Validation
{
    public class CollectionsGuard
    {
        public ValidationRule<IEnumerable<T>> Enumerable<T>()
        {
            return new ValidationRule<IEnumerable<T>>(); 
        }

        public ValidationRule<ICollection<T>> Collection<T>()
        {
            return new ValidationRule<ICollection<T>>();
        }

        public ValidationRule<T[]> Array<T>()
        {
            return new ValidationRule<T[]>();
        }

        public ValidationRule<IList<T>> List<T>()
        {
            return new ValidationRule<IList<T>>();
        }

        public ValidationRule<IDictionary<TKey, TType>> Dictionary<TKey, TType>()
        {
            return new ValidationRule<IDictionary<TKey, TType>>();
        }
    }
}