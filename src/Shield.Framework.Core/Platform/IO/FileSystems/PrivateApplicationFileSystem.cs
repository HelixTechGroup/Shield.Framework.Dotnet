#region Usings
using System;
using System.IO;
using System.IO.IsolatedStorage;
using Shield.Framework.Services.IO;
#endregion

namespace Shield.Framework.Platform.IO.FileSystems
{
    public sealed class PrivateApplicationFileSystem : FileSystem, IPrivateApplicationFileSystem, IEquatable<PrivateApplicationFileSystem>
    {
        #region Members
        private const string m_isolatedStoreRootDir = "m_RootDir";
        private IsolatedStorageFile m_container;
        #endregion        

        #region Methods
        public override void CopyDirectory(string sourcePath, string destinationPath, bool overwrite, bool isRecursive)
        {
            throw new NotImplementedException();
        }

        public override void ReplaceFile(string sourcePath, string destinationPath, string destinationBackupPath, bool ignoreMetadataErrors)
        {
            throw new NotSupportedException();
        }

        public override long GetFileLength(string filePath)
        {
            Throw.IfNullOrEmpty(filePath).ArgumentException(nameof(filePath));

            return FileExists(filePath) ? GetFile(filePath).Length : 0;
        }

        public override FileAttributes GetAttributes(string path)
        {
            Throw.IfNullOrEmpty(path).ArgumentException(nameof(path));

            return FileExists(path) ? GetFile(path).Attributes : GetDirectory(path).Attributes;
        }

        public override void SetAttributes(string path, FileAttributes attributes)
        {
            throw new NotSupportedException();
        }

        public override DateTime GetCreationTime(string path)
        {
            Throw.IfNullOrEmpty(path).ArgumentException(nameof(path));

            return m_container.GetCreationTime(path).DateTime;
        }

        public override void SetCreationTime(string path, DateTime time)
        {
            throw new NotSupportedException();
        }

        public override DateTime GetLastAccessTime(string path)
        {
            Throw.IfNullOrEmpty(path).ArgumentException(nameof(path));

            return m_container.GetLastAccessTime(path).DateTime;
        }

        public override void SetLastAccessTime(string path, DateTime time)
        {
            throw new NotSupportedException();
        }

        public override DateTime GetLastWriteTime(string path)
        {
            Throw.IfNullOrEmpty(path).ArgumentException(nameof(path));

            return m_container.GetLastWriteTime(path).DateTime;
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

        public override string ConvertPathToInternal(string path)
        {
            return path == StoragePath.RootPath ? m_rootDirectory : Path.Combine(m_rootDirectory, NormalizeStoragePathToInternal(path));
        }

        public override string ConvertFromInternal(string fileSystemPath)
        {
            return fileSystemPath.Replace(m_rootDirectory, StoragePath.RootPath).Replace(Path.DirectorySeparatorChar, StoragePath.PathSeparatorChar);
        }

        protected override void MapDelegates()
        {
            m_copyFile = m_container.CopyFile;
            m_createFile = m_container.CreateFile;
            m_createDirectory = m_container.CreateDirectory;
            m_getDirectories = m_container.GetDirectoryNames;
            m_getFiles = m_container.GetFileNames;
            m_deleteDirectory = m_container.DeleteDirectory;
            m_deleteFile = m_container.DeleteFile;
            m_directoryExists = m_container.DirectoryExists;
            m_fileExists = m_container.FileExists;
            m_moveDirectory = m_container.MoveDirectory;
            m_moveFile = m_container.MoveFile;
            m_openFile = m_container.OpenFile;
        }

        protected override void Dispose(bool disposing)
        {
            if (!m_disposed && disposing)
                CloseContainer();

            base.Dispose(disposing);
        }

        public bool Equals(PrivateApplicationFileSystem other)
        {
            if (other == null)
                return false;

            return m_container.Equals(other.m_container);
        }

        public bool Equals(IPrivateApplicationFileSystem other)
        {
            throw new NotImplementedException();
        }

        private void GetContainerFilePath()
        {
            Throw.IfNull(m_container).InvalidOperationException();

            m_rootDirectory = m_container.GetType()
                                         .GetField(m_isolatedStoreRootDir,
                                                   System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                         .GetValue(m_container)
                                         .ToString();
        }

        private void OpenContainer()
        {
            if (m_container != null)
                return;

            m_container = IsolatedStorageFile.GetUserStoreForAssembly();
        }

        private void CloseContainer()
        {
            if (m_container == null)
                return;

            m_container.Close();
            m_container.Dispose();
            m_container = null;
        }

        protected override void CreateFileSystem()
        {
            base.CreateFileSystem();
            OpenContainer();
            GetContainerFilePath();
        }
        #endregion
    }
}