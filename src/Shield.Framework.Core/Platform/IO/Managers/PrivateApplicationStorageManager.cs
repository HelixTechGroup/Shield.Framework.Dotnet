#region Usings
using Shield.Framework.IoC;
using Shield.Framework.Platform.Services.IO;
using Shield.Framework.Platform.Services.IO.FileSystems;
#endregion

namespace Shield.Framework.Platform.IO.Managers
{
    public sealed class PrivateApplicationStorageManager : StorageManager, IPrivateApplicationStorageManager
    {
        [InjectConstructor]
        public PrivateApplicationStorageManager(IPrivateApplicationFileSystem fileSystem) : base(fileSystem) { }

        public PrivateApplicationStorageManager(IPrivateApplicationFileSystem fileSystem, string rootDirectory) : base(fileSystem, rootDirectory) { }

        #region Methods
        public bool Equals(IPrivateApplicationStorageManager other)
        {
            throw new System.NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj is PrivateApplicationStorageManager && Equals((PrivateApplicationStorageManager)obj);
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }

        private bool Equals(PrivateApplicationStorageManager other)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}