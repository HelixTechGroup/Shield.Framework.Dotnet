#region Usings
using System;
using System.IO;
#endregion

namespace Shield.Framework.Services.IO
{
    public interface IStorageEntity : IEquatable<IStorageEntity>
    {
        #region Properties
        FileAttributes Attributes { get; set; }
        DateTime CreationTime { get; set; }
        bool Exists { get; }
        string ExpandedPath { get; }
        string FileSystemPath { get; }
        DateTime LastAccessTime { get; set; }
        DateTime LastWriteTime { get; set; }
        string Name { get; }
        string Path { get; }
        StoragePathType PathType { get; }
        StorageEntityType Type { get; }
        #endregion

        #region Methods
        void Create();

        void Delete();

        void Move(string destinationPath, bool overwrite);

        void Move(string destinationPath);

        void Copy(string destinationPath, bool overwrite);

        void Copy(string destinationPath);

        void Rename(string newName);
        #endregion
    }
}