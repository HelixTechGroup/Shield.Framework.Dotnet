#region Usings
using System;
using System.IO;
using Shield.Framework.Services.IO;
#endregion

namespace Shield.Framework.Platform.IO
{
    public abstract class StorageEntity : IStorageEntity, IEquatable<StorageEntity>
    {
        #region Members
        protected readonly IFileSystem m_fileSystem;
        protected string m_path;
        #endregion

        #region Properties
        public FileAttributes Attributes
        {
            get { return m_fileSystem.GetAttributes(m_path); }
            set { m_fileSystem.SetAttributes(m_path, value); }
        }

        public DateTime CreationTime
        {
            get { return m_fileSystem.GetCreationTime(m_path); }
            set { m_fileSystem.SetCreationTime(m_path, value); }
        }

        public abstract bool Exists { get; }

        public string ExpandedPath
        {
            get { return StoragePath.ValidateAndNormalizePath(m_path); }
        }

        public string FileSystemPath
        {
            get { return m_fileSystem.ConvertPathToInternal(ExpandedPath); }
        }

        public DateTime LastAccessTime
        {
            get { return m_fileSystem.GetLastAccessTime(m_path); }
            set { m_fileSystem.SetLastAccessTime(m_path, value); }
        }

        public DateTime LastWriteTime
        {
            get { return m_fileSystem.GetLastWriteTime(m_path); }
            set { m_fileSystem.SetLastWriteTime(m_path, value); }
        }

        public string Name
        {
            get { return m_path == StoragePath.RootPath ? m_path : StoragePath.GetName(m_path); }
        }

        public string Path
        {
            get { return m_path; }
        }

        public StoragePathType PathType
        {
            get { return StoragePath.IsRelativePath(m_path) ? StoragePathType.Relative : StoragePathType.Absolute; }
        }

        public virtual StorageEntityType Type
        {
            get
            {
                try
                {
                    return m_fileSystem.GetAttributes(m_path).HasFlag(FileAttributes.Directory)
                               ? StorageEntityType.Directory
                               : StorageEntityType.File;
                }
                catch
                {
                    return StorageEntityType.Unknown;
                }
            }
        }
        #endregion

        protected StorageEntity(IFileSystem fileSystem, string path)
        {
            Throw.IfNull(fileSystem).ArgumentNullException(nameof(fileSystem));

            m_fileSystem = fileSystem;
            m_path = string.IsNullOrWhiteSpace(path) ? StoragePath.RootPath : path;
        }

        #region Methods
        public static explicit operator string(StorageEntity path)
        {
            return path.ExpandedPath;
        }

        public abstract void Create();

        public abstract void Delete();

        public abstract void Move(string destinationPath, bool overwrite);

        public abstract void Move(string destinationPath);

        public abstract void Copy(string destinationPath, bool overwrite);

        public abstract void Copy(string destinationPath);

        public abstract void Rename(string newName);

        public virtual bool Equals(StorageEntity other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return m_path.Equals(other.Path)
                   && m_fileSystem.Equals(other.m_fileSystem);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Path.GetHashCode() + m_fileSystem.GetHashCode()) * 397;
            }
        }

        public bool Equals(IStorageEntity other)
        {
            if (other is null)
                return false;

            return m_path.Equals(other.Path);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType()
                   && Equals((StorageEntity)obj);
        }
        #endregion
    }
}