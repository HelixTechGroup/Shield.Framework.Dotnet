using System;
using System.Runtime.Serialization;

namespace Shield.Framework.Services.Extensibility.Exceptions
{
    [Serializable]
    public class ModuleLoaderNotFoundException : ModuleException
    {
        public ModuleLoaderNotFoundException()
        {
        }

        public ModuleLoaderNotFoundException(string message)
            : base(message)
        {
        }

        public ModuleLoaderNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ModuleLoaderNotFoundException(string moduleName, string message, Exception innerException)
            : base(moduleName, message, innerException)
        {
        }

        protected ModuleLoaderNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}