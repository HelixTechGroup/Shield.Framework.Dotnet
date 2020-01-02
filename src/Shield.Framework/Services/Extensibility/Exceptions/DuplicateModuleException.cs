using System;
using System.Runtime.Serialization;

namespace Shield.Framework.Services.Extensibility.Exceptions
{
    [Serializable]
    public class DuplicateModuleException : ModuleException
    {
        public DuplicateModuleException() : base()
        {
        }

        public DuplicateModuleException(string message) : base(message)
        {
        }

        public DuplicateModuleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DuplicateModuleException(string moduleName, string message)
            : base(moduleName, message)
        {
        }
        
        public DuplicateModuleException(string moduleName, string message, Exception innerException)
            : base(moduleName, message, innerException)
        {
        }

        protected DuplicateModuleException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
