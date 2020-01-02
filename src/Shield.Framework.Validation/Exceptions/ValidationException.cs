#region Usings
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
#endregion

namespace Shield.Framework.Validation.Exceptions
{
    [Serializable]
    public class ValidationException : AggregateException
    {
        #region Members
        protected string m_paramName;
        #endregion

        #region Properties
        public virtual string ParamName
        {
            get { return m_paramName; }
        }
        #endregion

        public ValidationException() { }

        public ValidationException(string message, string paramName, IEnumerable<Exception> inner)
            : base(message, inner)
        {
            m_paramName = paramName;
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ValidationException(IEnumerable<Exception> innerExceptions) : base(innerExceptions) { }

        public ValidationException(params Exception[] innerExceptions) : base(innerExceptions) { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, IEnumerable<Exception> innerExceptions) : base(message, innerExceptions) { }

        public ValidationException(string message, Exception innerException) : base(message, innerException) { }

        public ValidationException(string message, params Exception[] innerExceptions) : base(message, innerExceptions) { }
    }
}