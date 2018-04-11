using System.Collections;
using System.Collections.Generic;

namespace Shield.Framework.Validation
{
    public class NumbersGuard
    {
        public ValidationRule<short> Int16 
        {
            get { return new ValidationRule<short>(); }
        }

        public ValidationRule<int> Int32
        {
            get { return new ValidationRule<int>(); }
        }

        public ValidationRule<long> Int64
        {
            get { return new ValidationRule<long>(); }
        }

        public ValidationRule<float> Single
        {
            get { return new ValidationRule<float>(); }
        }

        public ValidationRule<double> Double
        {
            get { return new ValidationRule<double>(); }
        }

        public ValidationRule<decimal> Decimal
        {
            get { return new ValidationRule<decimal>(); }
        }

        public ValidationRule<byte> Byte
        {
            get { return new ValidationRule<byte>(); }
        }

        public ValidationRule<sbyte> SignedByte
        {
            get { return new ValidationRule<sbyte>(); }
        }
    }
}