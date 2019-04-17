#region Usings
using System;
using Shield.Framework.IoC;
using Shield.Framework.Platform.IO.FileSystems;
#endregion

namespace Shield.Framework.Platform.IO.Managers
{
    public class LocalApplicationStorageManager : StorageManager, ILocalApplicationStorageManager, IEquatable<LocalApplicationStorageManager>
    {
        [InjectConstructor]
        public LocalApplicationStorageManager(ILocalApplicationFileSystem fileSystem) : base(fileSystem) { }

        public LocalApplicationStorageManager(ILocalApplicationFileSystem fileSystem, string rootDirectory) : base(fileSystem, rootDirectory) { }

        #region Methods
        public bool Equals(ILocalApplicationStorageManager other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(LocalApplicationStorageManager other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((LocalApplicationStorageManager)obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}