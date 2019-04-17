#region Usings
using System;
using System.Runtime.Serialization;
#endregion

namespace Shield.Framework.Platform.IO.Exceptions
{
    [Serializable]
    public class MountPointException : Exception
    {
        #region Properties
        public string MountPoint { get; set; }

        public string StorageRootDirectory { get; set; }
        #endregion

        public MountPointException()
            : this(null) { }

        public MountPointException(string message)
            : this(null, null, message) { }

        public MountPointException(string message, Exception innerException)
            : this(null, null, message, innerException) { }

        public MountPointException(string moduleName, string storageRootDirectory, string message)
            : this(moduleName, storageRootDirectory, message, null) { }

        public MountPointException(string moduleName, string storageRootDirectory, string message, Exception innerException)
            : base(message, innerException)
        {
            MountPoint = moduleName;
            StorageRootDirectory = storageRootDirectory;
        }

        protected MountPointException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}