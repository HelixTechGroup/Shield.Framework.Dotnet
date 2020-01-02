#region Usings
using System;
using System.IO;
using Microsoft.Xna.Framework;
#endregion

namespace Shield.Framework.Platform.IO.FileSystems
{
    public sealed class TitleContainerFileSystem : ReadOnlyFileSystem, ILocalApplicationFileSystem, IEquatable<TitleContainerFileSystem>
    {
        #region Methods
        protected override void MapDelegates()
        {
            //m_copyFile = m_container.CopyFile;
            //m_createFile = m_container.CreateFile;
            //m_createDirectory = m_container.CreateDirectory;
            //m_getDirectories = m_container.GetDirectoryNames;
            //m_getFiles = m_container.GetFileNames;
            //m_deleteDirectory = m_container.DeleteDirectory;
            //m_deleteFile = m_container.DeleteFile;
            //m_directoryExists = m_container.DirectoryExists;
            //m_fileExists = m_container.FileExists;
            //m_moveDirectory = m_container.MoveDirectory;
            //m_moveFile = m_container.MoveFile;
            //m_openFile = TitleContainer.OpenStream;
        }

        public override long GetFileLength(string filePath)
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

        public override string ConvertFromInternal(string fileSystemPath)
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