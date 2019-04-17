#region Usings
using System;
using System.IO;
#endregion

namespace Shield.Framework.Platform.IO.FileSystems
{
    public abstract class ReadOnlyFileSystem : FileSystem, IEquatable<ReadOnlyFileSystem>
    {
        protected ReadOnlyFileSystem()
        {
            m_isReadOnly = true;
        }

        #region Methods
        public override void MoveDirectory(string sourcePath, string destinationPath, bool overwrite)
        {
            throw new NotSupportedException();
        }

        public override void CreateDirectory(string directoryPath)
        {
            throw new NotSupportedException();
        }

        public override void CopyDirectory(string sourcePath, string destinationPath, bool overwrite, bool isRecursive)
        {
            throw new NotSupportedException();
        }

        public override void DeleteDirectory(string directoryPath, bool isRecursive)
        {
            throw new NotSupportedException();
        }

        public override void MoveFile(string sourcePath, string destinationPath, bool overwrite)
        {
            throw new NotSupportedException();
        }

        public override void CopyFile(string sourcePath, string destinationPath, bool overwrite)
        {
            throw new NotSupportedException();
        }

        public override void ReplaceFile(string sourcePath, string destinationPath, string destinationBackupPath, bool ignoreMetadataErrors)
        {
            throw new NotSupportedException();
        }

        public override void DeleteFile(string filePath)
        {
            throw new NotSupportedException();
        }

        public override void CreateFile(string filePath)
        {
            throw new NotSupportedException();
        }

        public override void SetAttributes(string path, FileAttributes attributes)
        {
            throw new NotSupportedException();
        }

        public override void SetCreationTime(string path, DateTime time)
        {
            throw new NotSupportedException();
        }

        public override void SetLastAccessTime(string path, DateTime time)
        {
            throw new NotSupportedException();
        }

        public override void SetLastWriteTime(string path, DateTime time)
        {
            throw new NotSupportedException();
        }

        public override bool CanWatch(string path)
        {
            return false;
        }

        public override IFileSystemWatcher Watch(string path)
        {
            throw new NotSupportedException();
        }

        public bool Equals(ReadOnlyFileSystem other)
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
            return Equals((ReadOnlyFileSystem)obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}