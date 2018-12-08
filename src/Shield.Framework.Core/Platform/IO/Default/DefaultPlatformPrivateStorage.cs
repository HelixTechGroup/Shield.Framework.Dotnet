using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using Shield.Framework.Utility;

namespace Shield.Framework.Platform.IO.Default
{
    public class DefaultPlatformPrivateStorage : PlatformStorageManager
    {
        private IsolatedStorageFile m_container;

        public DefaultPlatformPrivateStorage()
        {
            OpenContainer();
        }

        #region Methods
        public override bool FileExists(string fileName, string path = "")
        {
            var fullPath = FilePathUtility.SetFullFilePath(fileName, path);
            return m_container.FileExists(fullPath);
        }

        public override bool DirectoryExists(string directoryName, string path = "")
        {
            var fullPath = FilePathUtility.SetFullFilePath(directoryName, path);
            return m_container.DirectoryExists(fullPath);
        }

        public override bool FileInUse(string fileName, string path = "")
        {
            IsolatedStorageFileStream stream = null;
            var fullPath = FilePathUtility.SetFullFilePath(fileName, path);

            if (FileExists(fileName, path))
            {
                try { stream = m_container.OpenFile(fullPath, FileMode.Open); }
                catch (IOException)
                {
                    //the file is unavailable because it is:
                    //still being written to
                    //or being processed by another thread
                    //or does not exist (has already been processed)
                    return true;
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
            }

            return false;
        }

        public override FileInfo GetFile(string fileName, string path = "")
        {
            FileInfo file = null;
            var fullPath = FilePathUtility.SetFullFilePath(fileName, path);

            if (m_container.FileExists(fullPath))
                file = new FileInfo(fullPath);

            return file;
        }

        public override IEnumerable<FileInfo> GetFiles(string path = "")
        {
            var files = m_container.GetFileNames(path);
            return files.Select(f => GetFile(f));
        }

        public override DirectoryInfo GetDirectory(string directoryName, string path = "")
        {
            DirectoryInfo dir = null;
            var fullPath = FilePathUtility.SetFullFilePath(directoryName, path);

            if (m_container.DirectoryExists(fullPath))
                dir = new DirectoryInfo(fullPath);

            return dir;
        }

        public override IEnumerable<DirectoryInfo> GetDirectories(string path = "")
        {
            var dirs = m_container.GetDirectoryNames(path);
            return dirs.Select(d => GetDirectory(d));
        }

        public override Stream OpenFile(string fileName, string path = "")
        {
            var fullPath = FilePathUtility.SetFullFilePath(fileName, path);
            var stream = m_container.OpenFile(fullPath, FileMode.OpenOrCreate);
            m_streams.Add(stream);

            return stream;
        }

        public override void CloseFile(Stream stream)
        {
            stream.Close();
            m_streams.Remove(stream);
            stream.Dispose();
        }

        public override Stream CreateFileStream(string fileName, string path = "")
        {
            var fullPath = FilePathUtility.SetFullFilePath(fileName, path);
            var stream = m_container.CreateFile(fullPath);
            m_streams.Add(stream);

            return stream;
        }

        public override void CreateFile(string fileName, string path = "")
        {
            IsolatedStorageFileStream stream = null;
            var fullPath = FilePathUtility.SetFullFilePath(fileName, path);

            try { stream = m_container.CreateFile(fullPath); }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        public override void CreateDirectory(string directoryName, string path = "")
        {
            var fullPath = FilePathUtility.SetFullFilePath(directoryName, path);
            if (!DirectoryExists(directoryName, path))
                m_container.CreateDirectory(fullPath);
        }

        public override void DeleteFile(string fileName, string path = "")
        {
            var fullPath = FilePathUtility.SetFullFilePath(fileName, path);
            if (!FileInUse(fileName, path))
                m_container.DeleteFile(fullPath);
            else
                throw new AccessViolationException("File: " + fileName + ", is in use.");
        }

        public override void DeleteDirectory(string directoryName, string path = "")
        {
            var fullPath = FilePathUtility.SetFullFilePath(directoryName, path);
            var files = m_container.GetFileNames(directoryName);
            if (files.Any(fileName => FileInUse(fileName, fullPath)))
                throw new AccessViolationException("Directory: " + directoryName + ", is in use.");

            m_container.DeleteDirectory(fullPath);
        }

        public override void MoveFile(string fileName, string sourcePath, string destinationPath)
        {
            if (sourcePath == destinationPath)
                throw new ArgumentException("Source and Destination path cannot be the same.");

            if (!FileExists(fileName, sourcePath))
                throw new InvalidOperationException("File: " + fileName + ", does not exist.");

            if (FileInUse(fileName, sourcePath))
                throw new AccessViolationException("File: " + fileName + ", is in use.");

            var sourceFullPath = FilePathUtility.SetFullFilePath(fileName, sourcePath);
            var destinationFullPath = FilePathUtility.SetFullFilePath(fileName, destinationPath);

            if (!DirectoryExists(destinationPath))
                CreateDirectory(destinationPath);

            if (FileExists(fileName, destinationPath))
                DeleteFile(fileName, destinationPath);

            m_container.MoveFile(sourceFullPath, destinationFullPath);
        }

        public override void MoveDirectory(string directoryName, string sourcePath, string destinationPath)
        {
            if (sourcePath == destinationPath)
                throw new ArgumentException("Source and Destination path cannot be the same.");

            if (!DirectoryExists(directoryName, sourcePath))
                throw new InvalidOperationException("Directory: " + directoryName + ", does not exist.");

            var sourceFullPath = FilePathUtility.SetFullFilePath(directoryName, sourcePath);
            var destinationFullPath = FilePathUtility.SetFullFilePath(directoryName, destinationPath);

            if (!DirectoryExists(destinationPath))
                CreateDirectory(destinationPath);

            if (DirectoryExists(directoryName, destinationPath))
                DeleteDirectory(directoryName, destinationPath);

            m_container.MoveDirectory(sourceFullPath, destinationFullPath);
        }

        public override void CopyFile(string fileName, string sourcePath, string destinationPath, bool overwrite = false)
        {
            if (!FileExists(fileName, sourcePath))
                throw new InvalidOperationException("File: " + fileName + ", does not exist.");

            if (FileInUse(fileName, sourcePath))
                throw new AccessViolationException("File: " + fileName + ", is in use.");

            var sourceFullPath = FilePathUtility.SetFullFilePath(fileName, sourcePath);
            var destinationFullPath = FilePathUtility.SetFullFilePath(fileName, destinationPath);

            if (!DirectoryExists(destinationPath))
                CreateDirectory(destinationPath);

            m_container.CopyFile(sourceFullPath, destinationFullPath, overwrite);
        }

        private void OpenContainer()
        {
            if (m_container == null)
                m_container = IsolatedStorageFile.GetUserStoreForAssembly();
        }

        private void CloseContainer()
        {
            if (m_container != null)
            {
                m_container.Close();
                m_container.Dispose();
                m_container = null;
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                    CloseContainer();
            }
            base.Dispose(disposing);
        }

        public override long GetFileLength(string fileName, string path = "")
        {
            throw new NotImplementedException();
        }

        public override FileAttributes GetAttributes(string name, string path)
        {
            throw new NotImplementedException();
        }

        public override void SetAttributes(string name, string path, FileAttributes attributes)
        {
            throw new NotImplementedException();
        }
    }
}