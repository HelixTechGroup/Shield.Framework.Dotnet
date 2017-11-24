using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shield.Framework.IoC
{
    [Serializable]
    public class IoCRegistrationException : Exception
    {
        public IoCRegistrationException() { }

        public IoCRegistrationException(string message) : base(message) { }

        public IoCRegistrationException(string message, Exception innerException)
            : base(message, innerException) { }

        protected IoCRegistrationException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
}
