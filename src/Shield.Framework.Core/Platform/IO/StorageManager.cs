#region Usings
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shield.Framework.Collections;
using Shield.Framework.IoC;
using Shield.Framework.IoC.DependencyInjection;
using Shield.Framework.Services.IO;
#endregion

namespace Shield.Framework.Platform.IO
{
    public class StorageManager : IStorageManager, IEquatable<StorageManager>
    {
        #region Events
        public event Action<IDispose> OnDispose;
        #endregion

        #region Members
        protected readonly IFileSystem m_fileSystem;
        private readonly Guid m_id;
        protected readonly IDirectory m_rootDirectory;
        protected readonly IList<Stream> m_streams;
        protected IDirectory m_currentWorkingDirectory;
        protected bool m_disposed;
        protected bool m_readOnly;
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
            get { return m_readOnly; }
        }

        public IDirectory RootDirectory
        {
            get { return m_rootDirectory; }
        }

        public IDirectory WorkingDirectory
        {
            get { return m_currentWorkingDirectory; }
        }
        #endregion

        [InjectConstructor]
        public StorageManager(IFileSystem fileSystem) : this(fileSystem, StoragePath.RootPath) { }

        public StorageManager(IFileSystem fileSystem, string rootDirectory)
        {
            Throw.IfNull(fileSystem).ArgumentNullException(nameof(fileSystem));
            Throw.IfNullOrEmpty(rootDirectory).ArgumentNullException(nameof(rootDirectory));

            m_fileSystem = fileSystem;
            m_id = Guid.NewGuid();
            m_streams = new ConcurrentList<Stream>();
            m_currentWorkingDirectory = m_rootDirectory = new Directory(fileSystem, rootDirectory);
        }

        ~StorageManager()
        {
            Dispose(false);
        }

        #region Methods
        public static bool operator ==(StorageManager left, StorageManager right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(StorageManager left, StorageManager right)
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

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            if (disposing)
            {
                foreach (var s in m_streams)
                {
                    m_streams.Remove(s);
                    s.Close();
                    s.Dispose();
                }

                m_fileSystem.Dispose();
            }

            OnDispose?.Invoke(this);
            m_disposed = true;
        }

        public IDirectory ChangeWorkingDirectory(string directoryName)
        {
            throw new NotImplementedException();
        }

        public bool FileExists(string fileName)
        {
            var filePath = NormalizePath(fileName);
            return m_fileSystem.FileExists(filePath);
        }

        public bool DirectoryExists(string directoryName)
        {
            var directoryPath = NormalizePath(directoryName);
            return m_fileSystem.DirectoryExists(directoryPath);
        }

        public IFile GetFile(string fileName)
        {
            var filePath = NormalizePath(fileName);
            return new File(m_fileSystem, filePath);
        }

        public IEnumerable<IFile> GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return m_fileSystem.EnumeratePath(NormalizePath(path), null, searchOption, StorageSearchTarget.File)
                .Select(file => NormalizePath(m_fileSystem.ConvertFromInternal(file))).Select(filePath => new File(m_fileSystem, filePath));
        }

        public IDirectory GetDirectory(string directoryName)
        {
            return new Directory(m_fileSystem, directoryName);
        }

        public IEnumerable<IDirectory> GetDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return m_fileSystem.EnumeratePath(NormalizePath(path), null, searchOption, StorageSearchTarget.File)
                .Select(directory => NormalizePath(m_fileSystem.ConvertFromInternal(directory)))
                .Select(directoryPath => new Directory(m_fileSystem, directoryPath));
        }

        public IEnumerable<IStorageEntity> GetEntities(string path, string searchPattern, SearchOption searchOption)
        {
            var results = new ConcurrentList<IStorageEntity>();
            results.AddRange(GetFiles(path, searchPattern, searchOption));
            results.AddRange(GetDirectories(path, searchPattern, searchOption));

            return results;
        }

        public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return m_fileSystem.EnumeratePath(path, searchPattern, searchOption, StorageSearchTarget.Directory);
        }

        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return m_fileSystem.EnumeratePath(path, searchPattern, searchOption, StorageSearchTarget.File);
        }

        public IEnumerable<string> EnumerateEntities(string path, string searchPattern, SearchOption searchOption)
        {
            return m_fileSystem.EnumeratePath(path, searchPattern, searchOption, StorageSearchTarget.Both);
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (m_id.GetHashCode() + m_fileSystem.GetHashCode() * 397);
            }
        }

        public bool Equals(StorageManager other)
        {
            if (other == null)
                return false;

            return m_id == other.Id && m_fileSystem.Equals(other.m_fileSystem);
        }

        public bool Equals(IStorageManager other)
        {
            if (other == null)
                return false;

            return m_id == other.Id;
        }

        public override bool Equals(object obj)
        {
            var p = obj as StorageManager;
            return p != null && Equals(p);
        }
        #endregion

        protected string NormalizePath(string entityPath)
        {
            return entityPath.StartsWith(m_rootDirectory.ExpandedPath)
                       ? StoragePath.ValidateAndNormalizePath(entityPath)
                       : StoragePath.CombineAndNormalizePath(m_rootDirectory.ExpandedPath, entityPath);
        }
    }

    public class StorageManager<TFileSystem> : StorageManager, IStorageManager<TFileSystem> where TFileSystem : IFileSystem
    {
        [InjectConstructor]
        public StorageManager(TFileSystem fileSystem) : base(fileSystem) { }

        public StorageManager(TFileSystem fileSystem, string rootDirectory) : base(fileSystem, rootDirectory) { }
    }
}