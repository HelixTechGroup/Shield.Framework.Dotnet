#region Usings
using System;
using System.IO;
using Shield.Framework.Services.IO;
#endregion

namespace Shield.Framework.Platform.IO.FileSystems
{
    public class PhysicalFileSystem : FileSystem, IPhysicalFileSystem, IEquatable<PhysicalFileSystem>
    {
        #region Methods
        public bool Equals(PhysicalFileSystem other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IPhysicalFileSystem other)
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
            return Equals((PhysicalFileSystem)obj);
        }

        public override void CopyDirectory(string sourcePath, string destinationPath, bool overwrite, bool isRecursive)
        {
            throw new NotImplementedException();
        }

        public override void ReplaceFile(string sourcePath, string destinationPath, string destinationBackupPath, bool ignoreMetadataErrors)
        {
            throw new NotImplementedException();
        }

        public override long GetFileLength(string filePath)
        {
            throw new NotImplementedException();
        }

        public override FileAttributes GetAttributes(string path)
        {
            throw new NotImplementedException();
        }

        public override void SetAttributes(string path, FileAttributes attributes)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetCreationTime(string path)
        {
            throw new NotImplementedException();
        }

        public override void SetCreationTime(string path, DateTime time)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetLastAccessTime(string path)
        {
            throw new NotImplementedException();
        }

        public override void SetLastAccessTime(string path, DateTime time)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetLastWriteTime(string path)
        {
            throw new NotImplementedException();
        }

        public override void SetLastWriteTime(string path, DateTime time)
        {
            throw new NotImplementedException();
        }

        public override bool CanWatch(string path)
        {
            throw new NotImplementedException();
        }

        public override IFileSystemWatcher Watch(string path)
        {
            throw new NotImplementedException();
        }

        public override string ConvertPathToInternal(string path)
        {
            throw new NotImplementedException();
        }

        public override string ConvertFromInternal(string fileSystemPath)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}