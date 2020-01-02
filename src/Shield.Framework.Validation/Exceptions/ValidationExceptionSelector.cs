#region Usings
using System;
#endregion

namespace Shield.Framework.Validation.Exceptions
{
    public class ValidationExceptionSelector<TType>
    {
        #region Members
        private readonly IValidate<TType> m_validator;
        #endregion

        public ValidationExceptionSelector(IValidate<TType> validator)
        {
            m_validator = validator;
        }

        #region Methods
        public IValidate<TType> ArgumentException()
        {
            return m_validator.WithException<ArgumentException>();
        }

        public IValidate<TType> ArgumentNullException()
        {
            return m_validator.WithException<ArgumentNullException>();
        }

        public IValidate<TType> ArgumentOutOfRangeException()
        {
            return m_validator.WithException<ArgumentOutOfRangeException>();
        }

        public IValidate<TType> InvalidOperationException()
        {
            return m_validator.WithException<InvalidOperationException>();
        }
        #endregion
    }
}