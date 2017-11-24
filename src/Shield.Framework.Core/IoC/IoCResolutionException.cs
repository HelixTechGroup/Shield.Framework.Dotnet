using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shield.Framework.IoC
{
    [Serializable]
    public class IoCResolutionException : Exception
    {
        public IoCResolutionException() { }

        public IoCResolutionException(string message) : base(message) { }

        public IoCResolutionException(string message, Exception innerException)
            : base(message, innerException) { }

        protected IoCResolutionException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
}
