using System;
using System.Runtime.Serialization;

namespace Shield.Framework.IoC.Exceptions
{
    [Serializable]
    public class NinjectModuleNotFoundException : Exception
    {
        public NinjectModuleNotFoundException()
        {
        }

        public NinjectModuleNotFoundException(string message) : base(message)
        {
        }

        public NinjectModuleNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NinjectModuleNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
