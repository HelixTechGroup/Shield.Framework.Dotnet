#region Usings
using System;
using System.Collections.Generic;
using System.IO;
#endregion

namespace Shield.Framework.Services.IO
{
    public interface IStorageManager : IDispose, IEquatable<IStorageManager>
    {
        #region Properties
        Guid Id { get; }
        bool IsReadOnly { get; }
        IDirectory RootDirectory { get; }
        IDirectory WorkingDirectory { get; }
        #endregion

        #region Methods
        IDirectory ChangeWorkingDirectory(string directoryName);

        bool FileExists(string fileName);

        bool DirectoryExists(string directoryName);

        IFile GetFile(string fileName);

        IEnumerable<IFile> GetFiles(string path, string searchPattern, SearchOption searchOption);

        IDirectory GetDirectory(string directoryName);

        IEnumerable<IDirectory> GetDirectories(string path, string searchPattern, SearchOption searchOption);

        IEnumerable<IStorageEntity> GetEntities(string path, string searchPattern, SearchOption searchOption);

        IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption);

        IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption);

        IEnumerable<string> EnumerateEntities(string path, string searchPattern, SearchOption searchOption);

        void Refresh();
        #endregion
    }

    public interface IStorageManager<TFileSystem> : IStorageManager where TFileSystem : IFileSystem { }
}