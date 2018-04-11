using System;
using System.Runtime.Serialization;

namespace Shield.Framework.Extensibility.Exceptions
{
    [Serializable]
    public class ModuleNotFoundException : ModuleException
    {
        public ModuleNotFoundException()
            : base()
        {
        }

        public ModuleNotFoundException(string message) : base(message)
        {
        }

        public ModuleNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ModuleNotFoundException(string moduleName, string message)
            : base(moduleName, message)
        {
        }

        public ModuleNotFoundException(string moduleName, string message, Exception innerException)
            : base(moduleName, message, innerException)
        {
        }

        protected ModuleNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}