using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shield.Framework.Validation.Exceptions
{
    [Serializable]
    public class ValidationException : AggregateException
    {
        protected string m_paramName;

        public virtual string ParamName
        {
            get { return m_paramName; }
        }

        public ValidationException() { }

        public ValidationException(string paramName, string message, IEnumerable<Exception> inner)
            : base(message, inner)
        {
            m_paramName = paramName;
        }
        
        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}