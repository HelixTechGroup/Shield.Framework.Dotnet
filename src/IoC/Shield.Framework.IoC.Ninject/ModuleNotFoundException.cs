using System;
using System.Runtime.Serialization;

namespace Shield.Framework.IoC.Ninject
{
    public class ModuleNotFoundException : Exception
    {
        public ModuleNotFoundException()
        {
        }

        public ModuleNotFoundException(string message) : base(message)
        {
        }

        public ModuleNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModuleNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
