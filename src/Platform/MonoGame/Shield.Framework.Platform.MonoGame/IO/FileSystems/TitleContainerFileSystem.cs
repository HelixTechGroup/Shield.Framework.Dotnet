#region Usings
using System;
using System.IO;
#endregion

namespace Shield.Framework.Platform.IO.FileSystems
{
    public sealed class TitleContainerFileSystem : ReadOnlyFileSystem, ILocalApplicationFileSystem, IEquatable<TitleContainerFileSystem>
    {
        #region Methods
        public override bool DirectoryExists(string path)
        {
            throw new NotImplementedException();
        }

        public override long GetFileLength(string path)
        {
            throw new NotImplementedException();
        }

        public override bool FileIsInUse(string path)
        {
            throw new NotImplementedException();
        }

        public override bool FileExists(string path)
        {
            throw new NotImplementedException();
        }

        public override Stream OpenFile(string path, FileMode mode, FileAccess access, FileShare share)
        {
            throw new NotImplementedException();
        }

        public override FileAttributes GetAttributes(string path)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetCreationTime(string path)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetLastAccessTime(string path)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetLastWriteTime(string path)
        {
            throw new NotImplementedException();
        }

        public override string ConvertPathToInternal(string path)
        {
            throw new NotImplementedException();
        }

        public override string ConvertFromInternal(string systemPath)
        {
            throw new NotImplementedException();
        }

        public bool Equals(ILocalApplicationFileSystem other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(TitleContainerFileSystem other)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}