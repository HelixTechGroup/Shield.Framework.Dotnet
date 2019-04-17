#region Usings
using System;
using System.IO;
#endregion

namespace Shield.Framework.Platform.IO
{
    public class File : StorageEntity, IFile, IEquatable<File>
    {
        #region Members
        protected bool m_isReadOnly;
        #endregion

        #region Properties
        public IDirectory Directory
        {
            get { return new Directory(m_fileSystem, StoragePath.GetDirectory(ExpandedPath)); }
        }

        public override bool Exists
        {
            get { return m_fileSystem.FileExists(ExpandedPath); }
        }

        public string Extension
        {
            get { return StoragePath.GetFileExtension(ExpandedPath); }
        }

        public bool IsInUse
        {
            get { return m_fileSystem.FileIsInUse(ExpandedPath); }
        }

        public bool IsReadOnly
        {
            get { return m_isReadOnly; }
        }

        public long Length
        {
            get { return m_fileSystem.GetFileLength(ExpandedPath); }
        }

        public override StorageEntityType Type
        {
            get { return StorageEntityType.File; }
        }
        #endregion

        public File(IFileSystem fileSystem, string path) : base(fileSystem, path)
        {
            m_isReadOnly = m_fileSystem.IsReadOnly;
        }

        #region Methods
        public override void Delete()
        {
            m_fileSystem.DeleteFile(ExpandedPath);
        }

        public override void Move(string destinationPath, bool overwrite)
        {
            throw new NotImplementedException();
        }

        public override void Move(string destinationPath)
        {
            throw new NotImplementedException();
        }

        public override void Copy(string destinationPath, bool overwrite)
        {
            throw new NotImplementedException();
        }

        public override void Copy(string destinationPath)
        {
            throw new NotImplementedException();
        }

        public override void Rename(string newName)
        {
            throw new NotImplementedException();
        }

        public Stream Open(FileMode mode, FileAccess access, FileShare share)
        {
            return m_fileSystem.OpenFile(ExpandedPath, mode, access, share);
        }

        public Stream Open()
        {
            return m_fileSystem.OpenFile(Extension, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }

        public Stream OpenRead()
        {
            return m_fileSystem.OpenFile(Extension, FileMode.Open, FileAccess.Read, FileShare.None);
        }

        public Stream OpenWrite()
        {
            return m_fileSystem.OpenFile(Extension, FileMode.Open, FileAccess.Write, FileShare.None);
        }

        public void Replace(string destinationPath, string destinationBackupPath, bool ignoreMetadataErrors)
        {
            throw new NotImplementedException();
        }

        public override void Create()
        {
            m_fileSystem.CreateFile(ExpandedPath);
        }

        public bool Equals(IFile other)
        {
            if (other is null)
                return false;

            return m_path.Equals(other.Path)
                   && Directory.Equals(other.Directory);
        }

        public bool Equals(File other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return base.Equals(other)
                   && Directory.Equals(other.Directory);
        }

        public override bool Equals(StorageEntity other)
        {
            if (other is File a)
                return Equals(a);

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType()
                   && Equals((File)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (Directory != null ? Directory.GetHashCode() : 0);
            }
        }
        #endregion
    }
}