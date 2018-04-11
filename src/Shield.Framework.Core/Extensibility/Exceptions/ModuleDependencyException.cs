using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Shield.Framework.Extensibility.Exceptions
{
    [Serializable]
    public class ModuleDependencyException : ModuleException
    {
        public ModuleDependencyException()
            : base()
        {
        }

        public ModuleDependencyException(string message) : base(message)
        {
        }

        public ModuleDependencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ModuleDependencyException(string moduleName, string message)
            : base(moduleName, message)
        {
        }

        public ModuleDependencyException(string moduleName, string message, Exception innerException)
            : base(moduleName, message, innerException)
        {
        }

        protected ModuleDependencyException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
