#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Shield.Framework.Validation
{
    public sealed class ValidationResult : IEquatable<bool>
    {
        #region Members
        public static readonly ValidationResult Success = new ValidationResult();
        private readonly IEnumerable<string> m_errors;
        private readonly bool m_isValid;
        #endregion

        #region Properties
        public IEnumerable<string> ErrorMessages
        {
            get { return m_errors; }
        }

        public bool IsValid
        {
            get { return m_isValid; }
        }
        #endregion

        public ValidationResult()
        {
            m_isValid = true;
            m_errors = new List<string>();
        }

        private ValidationResult(IEnumerable<string> errors)
        {
            m_isValid = false;
            m_errors = errors;
        }

        #region Methods
        public static ValidationResult operator &(ValidationResult result1, ValidationResult result2)
        {
            var succeed = result1.IsValid && result2.IsValid;
            var errors = result1.ErrorMessages.Concat(result2.ErrorMessages);
            return succeed ? Success : Fail(errors);
        }

        public static implicit operator bool(ValidationResult argument) => argument.IsValid;

        internal static ValidationResult Fail(params string[] errors)
        {
            return new ValidationResult(errors);
        }

        internal static ValidationResult Fail(IEnumerable<string> errors)
        {
            return new ValidationResult(errors);
        }

        public bool Equals(bool other)
        {
            return IsValid == other;
        }
        #endregion
    }
}