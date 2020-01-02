using System;
using System.Runtime.Serialization;

namespace Shield.Framework.Services.Extensibility.Exceptions
{
    [Serializable]
    public class CyclicDependencyException : ModuleDependencyException
    {
        public CyclicDependencyException() : base() { }

        public CyclicDependencyException(string message) : base(message) { }

        public CyclicDependencyException(string message, Exception innerException) : base(message, innerException) { }

        public CyclicDependencyException(string moduleName, string message, Exception innerException)
            : base(moduleName, message, innerException)
        {
        }

        protected CyclicDependencyException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
