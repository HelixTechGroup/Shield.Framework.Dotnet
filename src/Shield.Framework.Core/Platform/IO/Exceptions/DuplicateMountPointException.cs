#region Usings
using System;
using System.Runtime.Serialization;
#endregion

namespace Shield.Framework.Platform.IO.Exceptions
{
    [Serializable]
    public class DuplicateMountPointException : MountPointException
    {
        public DuplicateMountPointException() { }

        public DuplicateMountPointException(string message) : base(message) { }

        public DuplicateMountPointException(string message, Exception innerException) : base(message, innerException) { }

        public DuplicateMountPointException(string mountPoint, string storageRootDirectory, string message)
            : base(mountPoint, storageRootDirectory, message) { }

        public DuplicateMountPointException(string mountPoint, string storageRootDirectory, string message, Exception innerException)
            : base(mountPoint, storageRootDirectory, message, innerException) { }

        protected DuplicateMountPointException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}