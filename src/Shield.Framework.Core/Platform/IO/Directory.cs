#region Usings
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#endregion

namespace Shield.Framework.Platform.IO
{
    public class Directory : StorageEntity, IDirectory, IEquatable<Directory>
    {
        #region Properties
        public override bool Exists
        {
            get { return m_fileSystem.DirectoryExists(ExpandedPath); }
        }

        public IDirectory Parent
        {
            get { return m_path == StoragePath.RootPath ? null : new Directory(m_fileSystem, StoragePath.GetDirectory(ExpandedPath)); }
        }

        public IDirectory Root
        {
            get { return m_path == StoragePath.RootPath ? null : new Directory(m_fileSystem, StoragePath.GetRootDirectory(ExpandedPath)); }
        }

        public override StorageEntityType Type
        {
            get { return StorageEntityType.Directory; }
        }
        #endregion

        public Directory(IFileSystem fileSystem, string path) : base(fileSystem, path) { }

        #region Methods
        public void Move(IDirectory destinationDirectory, bool overwrite)
        {
            Throw.If(ExpandedPath == StoragePath.RootPath || destinationDirectory.ExpandedPath == StoragePath.RootPath).UnauthorizedAccessException();
            Throw.If(ExpandedPath == destinationDirectory.ExpandedPath).IOException();

            var newPath = StoragePath.CombinePath(destinationDirectory.ExpandedPath, Name);
            m_fileSystem.MoveDirectory(ExpandedPath, newPath, overwrite);
        }

        public void Move(IDirectory destinationDirectory)
        {
            Move(destinationDirectory, false);
        }

        public void Copy(IDirectory destinationDirectory, bool overwrite)
        {
            Throw.If(ExpandedPath == StoragePath.RootPath || destinationDirectory.ExpandedPath == StoragePath.RootPath).UnauthorizedAccessException();
            Throw.If(ExpandedPath == destinationDirectory.ExpandedPath).IOException();

            var newPath = StoragePath.CombinePath(destinationDirectory.ExpandedPath, Name);
            m_fileSystem.CopyDirectory(ExpandedPath, newPath, overwrite, true);
        }

        public void Copy(IDirectory destinationDirectory)
        {
            Copy(destinationDirectory, false);
        }

        public bool FileExists(string fileName)
        {
            //Throw.If(StoragePath.IsAbsolutePath(fileName)).ArgumentException(nameof(fileName));

            var filePath = NormalizePath(fileName);
            return m_fileSystem.FileExists(filePath);
        }

        public bool DirectoryExists(string directoryName)
        {
            //Throw.If(StoragePath.IsAbsolutePath(directoryName)).ArgumentException(nameof(directoryName));

            var directoryPath = NormalizePath(directoryName);
            return m_fileSystem.FileExists(directoryPath);
        }

        public IFile GetFile(string fileName)
        {
            //Throw.If(StoragePath.IsAbsolutePath(fileName)).ArgumentException(nameof(fileName));

            var filePath = NormalizePath(fileName);
            return new File(m_fileSystem, filePath);
        }

        public IEnumerable<IFile> GetFiles(string searchPattern, SearchOption searchOption)
        {
            var files = m_fileSystem.EnumeratePath(ExpandedPath, searchPattern, searchOption, StorageSearchTarget.File);
            foreach (var file in files)
            {
                var path = NormalizePath(m_fileSystem.ConvertFromInternal(file));
                yield return new File(m_fileSystem, path);
            }
        }

        public IEnumerable<IFile> GetFiles()
        {
            return GetFiles(null, SearchOption.AllDirectories);
        }

        public IDirectory GetDirectory(string directoryName)
        {
            //Throw.If(StoragePath.IsAbsolutePath(directoryName)).ArgumentException(nameof(directoryName));

            var directoryPath = NormalizePath(directoryName);
            return new Directory(m_fileSystem, directoryPath);
        }

        public IEnumerable<IDirectory> GetDirectories(string searchPattern, SearchOption searchOption)
        {
            return m_fileSystem.EnumeratePath(ExpandedPath, searchPattern, searchOption, StorageSearchTarget.Directory)
                .Select(directory => NormalizePath(m_fileSystem.ConvertFromInternal(directory))).Select(path => new Directory(m_fileSystem, path));
        }

        public IEnumerable<IDirectory> GetDirectories()
        {
            return GetDirectories(null, SearchOption.AllDirectories);
        }

        public IEnumerable<IStorageEntity> GetEntities(string searchPattern, SearchOption searchOption)
        {
            foreach (var entity in m_fileSystem.EnumeratePath(ExpandedPath, searchPattern, searchOption, StorageSearchTarget.Both))
            {
                var path = NormalizePath(m_fileSystem.ConvertFromInternal(entity));
                if (FileExists(path))
                    yield return new File(m_fileSystem, path);
                else
                    yield return new Directory(m_fileSystem, path);
            }
        }

        public IEnumerable<IStorageEntity> GetEntities()
        {
            return GetEntities(null, SearchOption.AllDirectories);
        }

        public IEnumerable<string> EnumerateFiles(string searchPattern, SearchOption searchOption)
        {
            return m_fileSystem.EnumeratePath(ExpandedPath, searchPattern, searchOption, StorageSearchTarget.File);
        }

        public IEnumerable<string> EnumerateFiles()
        {
            return EnumerateFiles(null, SearchOption.AllDirectories);
        }

        public IEnumerable<string> EnumerateDirectories(string searchPattern, SearchOption searchOption)
        {
            return m_fileSystem.EnumeratePath(ExpandedPath, searchPattern, searchOption, StorageSearchTarget.Directory);
        }

        public IEnumerable<string> EnumerateDirectories()
        {
            return EnumerateDirectories(null, SearchOption.AllDirectories);
        }

        public IEnumerable<string> EnumerateEntities(string searchPattern, SearchOption searchOption)
        {
            return m_fileSystem.EnumeratePath(ExpandedPath, searchPattern, searchOption, StorageSearchTarget.Both);
        }

        public IEnumerable<string> EnumerateEntities()
        {
            return EnumerateEntities(null, SearchOption.AllDirectories);
        }

        public IFile CreateFile(string fileName)
        {
            //Throw.If(StoragePath.IsAbsolutePath(fileName)).ArgumentException(nameof(fileName));

            var file = new File(m_fileSystem, StoragePath.CombineAndNormalizePath(ExpandedPath, fileName));
            file.Create();

            return file;
        }

        public IDirectory CreateDirectory(string directoryName)
        {
            //Throw.If(StoragePath.IsAbsolutePath(directoryName)).ArgumentException(nameof(directoryName));

            var directory = new Directory(m_fileSystem, StoragePath.CombineAndNormalizePath(ExpandedPath, directoryName));
            directory.Create();

            return directory;
        }

        public void Delete(bool recursive)
        {
            m_fileSystem.DeleteDirectory(ExpandedPath, recursive);
        }

        public override void Delete()
        {
            Delete(true);
        }

        public override void Move(string destinationPath, bool overwrite)
        {
            var normalDestPath = NormalizePath(destinationPath);

            Throw.If(ExpandedPath == StoragePath.RootPath || normalDestPath == StoragePath.RootPath).UnauthorizedAccessException();
            Throw.If(ExpandedPath == destinationPath || m_path == destinationPath).IOException();

            m_fileSystem.MoveDirectory(ExpandedPath, normalDestPath, overwrite);
            m_path = normalDestPath;
        }

        public override void Move(string destinationPath)
        {
            Move(destinationPath, false);
        }

        public override void Copy(string destinationPath, bool overwrite)
        {
            var normalDestPath = NormalizePath(destinationPath);

            Throw.If(ExpandedPath == StoragePath.RootPath || normalDestPath == StoragePath.RootPath).UnauthorizedAccessException();
            Throw.If(ExpandedPath == destinationPath || m_path == destinationPath).IOException();

            m_fileSystem.CopyDirectory(ExpandedPath, normalDestPath, overwrite, true);
        }

        public override void Copy(string destinationPath)
        {
            Copy(destinationPath, false);
        }

        public override void Rename(string newName)
        {
            //Throw.If(StoragePath.IsAbsolutePath(newName)).ArgumentException(nameof(newName));
            Throw.If(Name == StoragePath.RootPath || newName == StoragePath.RootPath).UnauthorizedAccessException();
            Throw.If(Name == newName).IOException();

            var newPath = StoragePath.CombinePath(Parent.ExpandedPath, newName);
            m_fileSystem.MoveDirectory(ExpandedPath, newPath, false);
            m_path = newPath;
        }

        public override void Create()
        {
            m_fileSystem.CreateDirectory(ExpandedPath);
        }

        public bool Equals(IDirectory other)
        {
            if (other is null)
                return false;

            return m_path.Equals(other.Path)
                   && Parent.Equals(other.Parent);
        }

        public bool Equals(Directory other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return base.Equals(other)
                   && Parent.Equals(other.Parent);
        }

        public override bool Equals(StorageEntity other)
        {
            if (other is Directory a)
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
                   && Equals((Directory)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (Parent != null ? Parent.GetHashCode() : 0);
            }
        }
        #endregion

        protected string NormalizePath(string entityPath)
        {
            return entityPath.StartsWith(ExpandedPath) || entityPath.StartsWith(m_path)
                       ? StoragePath.ValidateAndNormalizePath(entityPath)
                       : StoragePath.CombineAndNormalizePath(m_path, entityPath);
        }
    }
}