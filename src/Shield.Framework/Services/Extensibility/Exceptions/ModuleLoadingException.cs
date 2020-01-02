using System;
using System.Runtime.Serialization;

namespace Shield.Framework.Services.Extensibility.Exceptions
{
    [Serializable]
    public class ModuleLoadingException : ModuleException
    {
        public ModuleLoadingException()
        {
        }

        public ModuleLoadingException(string message)
            : base(message)
        {
        }

        public ModuleLoadingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ModuleLoadingException(string moduleName, string message, Exception innerException)
            : base(moduleName, message, innerException)
        {
        }

        protected ModuleLoadingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}