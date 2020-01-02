#region Usings
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shield.Framework.Services.IO;
#endregion

namespace Shield.Framework.Platform.IO
{
    public abstract class FileSystem : IFileSystem, IEquatable<FileSystem>
    {
        #region Delegates
        protected delegate void CopyFileDelegate(string sourcePath, string destinationPath, bool overwrite);

        protected delegate void CreateDirectoryDelegate(string path);

        protected delegate Stream CreateFileDelegate(string path);

        protected delegate void DeleteDirectoryDelegate(string path);

        protected delegate void DeleteFileDelegate(string path);

        protected delegate bool DirectoryExistsDelegate(string path);

        protected delegate bool FileExistsDelegate(string path);

        protected delegate string[] GetDirectoriesDelegate(string pattern);

        protected delegate string[] GetFilesDelegate(string pattern);

        protected delegate void MoveDirectoryDelegate(string sourcePath, string destinationPath);

        protected delegate void MoveFileDelegate(string sourcePath, string destinationPath);

        protected delegate Stream OpenFileDelegate(string path, FileMode mode, FileAccess access, FileShare share);
        #endregion

        #region Events
        public event Action<IDispose> OnDispose;
        #endregion

        #region Members
        private readonly Guid m_id;
        protected CopyFileDelegate m_copyFile;
        protected CreateDirectoryDelegate m_createDirectory;
        protected CreateFileDelegate m_createFile;
        protected DeleteDirectoryDelegate m_deleteDirectory;
        protected DeleteFileDelegate m_deleteFile;
        protected DirectoryExistsDelegate m_directoryExists;
        protected bool m_disposed;
        protected FileExistsDelegate m_fileExists;
        protected GetDirectoriesDelegate m_getDirectories;
        protected GetFilesDelegate m_getFiles;
        protected bool m_isReadOnly;
        protected MoveDirectoryDelegate m_moveDirectory;
        protected MoveFileDelegate m_moveFile;
        protected OpenFileDelegate m_openFile;
        protected string m_rootDirectory;
        #endregion

        #region Properties
        public bool Disposed
        {
            get { return m_disposed; }
        }

        public Guid Id
        {
            get { return m_id; }
        }

        public bool IsReadOnly
        {
            get { return m_isReadOnly; }
        }

        public string Root
        {
            get { return m_rootDirectory; }
        }
        #endregion

        protected FileSystem()
        {
            m_id = Guid.NewGuid();
            InitializeFileSystem();
        }

        ~FileSystem()
        {
            Dispose(false);
        }

        #region Methods
        public static bool operator ==(FileSystem left, FileSystem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FileSystem left, FileSystem right)
        {
            return !Equals(left, right);
        }

        public static bool Equals(IStorageManager left, IStorageManager right)
        {
            return left.Equals(right);
        }

        public static bool Equals(StorageManager left, StorageManager right)
        {
            return left.Equals(right);
        }

        public abstract void CopyDirectory(string sourcePath, string destinationPath, bool overwrite, bool isRecursive);

        public abstract void ReplaceFile(string sourcePath, string destinationPath, string destinationBackupPath, bool ignoreMetadataErrors);

        public abstract long GetFileLength(string filePath);

        public abstract FileAttributes GetAttributes(string path);

        public abstract void SetAttributes(string path, FileAttributes attributes);

        public abstract DateTime GetCreationTime(string path);

        public abstract void SetCreationTime(string path, DateTime time);

        public abstract DateTime GetLastAccessTime(string path);

        public abstract void SetLastAccessTime(string path, DateTime time);

        public abstract DateTime GetLastWriteTime(string path);

        public abstract void SetLastWriteTime(string path, DateTime time);

        public abstract bool CanWatch(string path);

        public abstract IFileSystemWatcher Watch(string path);

        public abstract string ConvertPathToInternal(string path);

        public abstract string ConvertFromInternal(string fileSystemPath);

        protected abstract void MapDelegates();

        public virtual void CreateDirectory(string directoryPath)
        {
            Throw.IfNullOrEmpty(directoryPath).ArgumentNullException(nameof(directoryPath));

            m_createDirectory(directoryPath);
        }

        public virtual bool DirectoryExists(string directoryPath)
        {
            Throw.IfNullOrEmpty(directoryPath).ArgumentNullException(nameof(directoryPath));

            return m_directoryExists(directoryPath);
        }

        public virtual void MoveDirectory(string sourcePath, string destinationPath, bool overwrite)
        {
            Throw.IfNullOrEmpty(sourcePath).ArgumentNullException(nameof(sourcePath));
            Throw.IfNullOrEmpty(destinationPath).ArgumentNullException(nameof(destinationPath));
            Throw.If(!DirectoryExists(sourcePath)).InvalidOperationException();
            Throw.If(!overwrite && DirectoryExists(destinationPath)).InvalidOperationException();

            m_moveDirectory(sourcePath, destinationPath);
        }

        public virtual void DeleteDirectory(string directoryPath, bool isRecursive)
        {
            Throw.IfNullOrEmpty(directoryPath).ArgumentNullException(nameof(directoryPath));
            Throw.If(!DirectoryExists(directoryPath)).InvalidOperationException();

            m_deleteDirectory(directoryPath);
        }

        public virtual void CopyFile(string sourcePath, string destinationPath, bool overwrite)
        {
            Throw.IfNullOrEmpty(sourcePath).ArgumentNullException(nameof(sourcePath));
            Throw.IfNullOrEmpty(destinationPath).ArgumentNullException(nameof(destinationPath));
            Throw.If(!FileExists(sourcePath)).InvalidOperationException();

            m_copyFile(sourcePath, destinationPath, overwrite);
        }

        public virtual bool FileIsInUse(string filePath)
        {
            Throw.IfNullOrEmpty(filePath).ArgumentNullException(nameof(filePath));

            Stream stream = null;

            if (!FileExists(filePath))
                return false;

            try
            {
                stream = m_openFile(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
            }
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
                stream?.Close();
            }

            return false;
        }

        public virtual bool FileExists(string filePath)
        {
            Throw.IfNullOrEmpty(filePath).ArgumentNullException(nameof(filePath));

            return m_fileExists(filePath);
        }

        public virtual void MoveFile(string sourcePath, string destinationPath, bool overwrite)
        {
            Throw.IfNullOrEmpty(sourcePath).ArgumentNullException(nameof(sourcePath));
            Throw.IfNullOrEmpty(destinationPath).ArgumentNullException(nameof(destinationPath));
            Throw.If(!FileExists(sourcePath)).InvalidOperationException();
            Throw.If(!overwrite && FileExists(destinationPath)).InvalidOperationException();

            m_moveFile(sourcePath, destinationPath);
        }

        public virtual void DeleteFile(string filePath)
        {
            Throw.IfNullOrEmpty(filePath).ArgumentNullException(nameof(filePath));
            Throw.If(!FileExists(filePath)).InvalidOperationException();

            m_deleteFile(filePath);
        }

        public virtual Stream OpenFile(string filePath, FileMode mode, FileAccess access, FileShare share)
        {
            Throw.IfNullOrEmpty(filePath).ArgumentNullException(nameof(filePath));
            Throw.If(!(mode.HasFlag(FileMode.Create) || mode.HasFlag(FileMode.CreateNew) || mode.HasFlag(FileMode.OpenOrCreate))
                     && !FileExists(filePath))
                 .InvalidOperationException();

            return m_openFile(filePath, mode, access, share);
        }

        public virtual Stream CreateAndOpenFile(string filePath)
        {
            Throw.IfNullOrEmpty(filePath).ArgumentNullException(nameof(filePath));
            //Throw.If(FileExists(filePath)).InvalidOperationException();

            return m_createFile(filePath);
        }

        public virtual void CreateFile(string filePath)
        {
            CreateAndOpenFile(filePath).Close();
        }

        public virtual IEnumerable<string> EnumeratePath(string path,
                                                         string searchPattern,
                                                         SearchOption searchOption,
                                                         StorageSearchTarget searchTarget)
        {
            Throw.IfNullOrEmpty(path).ArgumentException(nameof(path));

            if (string.IsNullOrWhiteSpace(searchPattern))
                searchPattern = "*";

            var root = Path.GetDirectoryName(searchPattern);
            if (root != "")
                root += "/";

            var results = new ConcurrentList<string>();
            if (searchTarget == StorageSearchTarget.Directory || searchTarget == StorageSearchTarget.Both)
                results.AddRange(m_getDirectories(path + searchPattern));
            if (searchTarget == StorageSearchTarget.File || searchTarget == StorageSearchTarget.Both)
                results.AddRange(m_getFiles(path + searchPattern));

            if (searchOption != SearchOption.AllDirectories)
                return results;

            var directoryList = new ConcurrentList<string>(m_getDirectories(path + searchPattern));
            for (int i = 0, max = directoryList.Count; i < max; i++)
            {
                var directory = directoryList[i] + "/";
                var more = EnumeratePath(root + directory, searchPattern, searchOption, searchTarget).ToList();


                for (var j = 0; j < more.Count; j++)
                    more[j] = directory + more[j];

                results.InsertRange(i + 1, more);
                i += more.Count;
                max += more.Count;
            }

            return results;
        }

        protected virtual void Dispose(bool disposing)
        {
            OnDispose?.Invoke(this);
            m_disposed = true;
        }

        protected virtual string NormalizeStoragePathToInternal(string path)
        {
            return path.TrimStart(StoragePath.PathSeparatorChar).Replace(StoragePath.PathSeparatorChar, Path.DirectorySeparatorChar);
        }

        protected virtual DirectoryInfo GetDirectory(string path)
        {
            Throw.IfNullOrEmpty(path).ArgumentNullException(nameof(path));

            return !DirectoryExists(path) ? null : new DirectoryInfo(Path.Combine(m_rootDirectory, NormalizeStoragePathToInternal(path)));
        }

        protected virtual FileInfo GetFile(string path)
        {
            Throw.IfNullOrEmpty(path).ArgumentNullException(nameof(path));

            return !FileExists(path) ? null : new FileInfo(Path.Combine(m_rootDirectory, NormalizeStoragePathToInternal(path)));
        }

        protected virtual void CreateFileSystem() { }

        protected virtual void PreinitializeFileSystem() { }

        protected virtual void PostinitializeFileSystem() { }

        public override int GetHashCode()
        {
            unchecked
            {
                return (m_id.GetHashCode() + Root.GetHashCode()) * 397;
            }
        }

        public override bool Equals(object obj)
        {
            var p = obj as FileSystem;
            return p != null && Equals(p);
        }

        public long GetDirectoryLenth(string directoryPath, bool isRecursive)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Equals(IFileSystem other)
        {
            if (other == null)
                return false;

            return m_id == other.Id && m_rootDirectory == other.Root;
        }

        public bool Equals(FileSystem other)
        {
            if (other == null)
                return false;

            return m_id == other.Id && m_rootDirectory == other.Root;
        }

        protected void InitializeFileSystem()
        {
            CreateFileSystem();
            PreinitializeFileSystem();
            MapDelegates();
            PostinitializeFileSystem();
        }
        #endregion
    }
}