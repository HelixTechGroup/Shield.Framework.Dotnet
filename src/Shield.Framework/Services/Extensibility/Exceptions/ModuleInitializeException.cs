using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Shield.Framework.Services.Extensibility.Exceptions
{
    [Serializable]
    internal class ModuleInitializeException : ModuleException
    {
        public ModuleInitializeException()
        {
        }

        public ModuleInitializeException(string message) : base(message)
        {
        }

        public ModuleInitializeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ModuleInitializeException(string moduleName, string moduleAssembly, string message)
            : this(moduleName, message, moduleAssembly, null)
        {
        }

        public ModuleInitializeException(string moduleName, string moduleAssembly, string message, Exception innerException)
            : base(
                moduleName,
                string.Format(CultureInfo.CurrentCulture, 
                    "An exception occurred while initializing module \'{0}\'. \r\n    - The exception message was: {2}\r\n    - The Assembly that the module was trying to be loaded from was:{1}\r\n    Check the InnerException property of the exception for more information. If the exception occurred while creating an object in a DI container, you can exception.GetRootException() to help locate the root cause of the problem. ", 
                    moduleName, moduleAssembly, message),
                innerException)
        {
        }

        protected ModuleInitializeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}