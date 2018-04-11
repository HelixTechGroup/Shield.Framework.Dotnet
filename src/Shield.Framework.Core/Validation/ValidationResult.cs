using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shield.Framework.Validation
{
    public class ValidationResult
    {
        protected bool m_isValid;
        protected IEnumerable<string> m_errors;

        public bool IsValid
        {
            get { return m_isValid; }
        }

        public IEnumerable<string> ErrorMessages
        {
            get { return m_errors; }
        }

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

        public static ValidationResult operator &(ValidationResult result1, ValidationResult result2)
        {
            var succeed = result1.IsValid && result2.IsValid;
            var errors = result1.ErrorMessages.Concat(result2.ErrorMessages);            
            return succeed ? Success : Fail(errors);
        }

        public static readonly ValidationResult Success = new ValidationResult();

        internal static ValidationResult Fail(IEnumerable<string> errors)
        {
            return new ValidationResult(errors);
        } 
    }
}
