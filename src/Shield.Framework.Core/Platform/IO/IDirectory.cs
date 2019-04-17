#region Usings
using System;
using System.Collections.Generic;
using System.IO;
#endregion

namespace Shield.Framework.Platform.IO
{
    public interface IDirectory : IStorageEntity, IEquatable<IDirectory>
    {
        #region Properties
        IDirectory Parent { get; }
        IDirectory Root { get; }
        #endregion

        #region Methods
        void Move(IDirectory destinationDirectory, bool overwrite);

        void Move(IDirectory destinationDirectory);

        void Copy(IDirectory destinationDirectory, bool overwrite);

        void Copy(IDirectory destinationDirectory);


        bool FileExists(string fileName);

        bool DirectoryExists(string directoryName);

        IFile CreateFile(string fileName);

        IDirectory CreateDirectory(string directoryName);

        IFile GetFile(string fileName);

        IEnumerable<IFile> GetFiles(string searchPattern, SearchOption searchOption);

        IEnumerable<IFile> GetFiles();

        IDirectory GetDirectory(string directoryName);

        IEnumerable<IDirectory> GetDirectories(string searchPattern, SearchOption searchOption);

        IEnumerable<IDirectory> GetDirectories();

        IEnumerable<IStorageEntity> GetEntities(string searchPattern, SearchOption searchOption);

        IEnumerable<IStorageEntity> GetEntities();

        IEnumerable<string> EnumerateFiles(string searchPattern, SearchOption searchOption);

        IEnumerable<string> EnumerateFiles();

        IEnumerable<string> EnumerateDirectories(string searchPattern, SearchOption searchOption);

        IEnumerable<string> EnumerateDirectories();

        IEnumerable<string> EnumerateEntities(string searchPattern, SearchOption searchOption);

        IEnumerable<string> EnumerateEntities();

        void Delete(bool recursive);
        #endregion
    }
}