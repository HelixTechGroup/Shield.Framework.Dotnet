#region Usings
using System;
using System.IO;
#endregion

namespace Shield.Framework.Services.IO
{
    public interface IFile : IStorageEntity, IEquatable<IFile>
    {
        #region Properties
        IDirectory Directory { get; }
        string Extension { get; }
        bool IsInUse { get; }
        bool IsReadOnly { get; }
        long Length { get; }
        #endregion

        #region Methods
        Stream Open(FileMode mode, FileAccess access, FileShare share);

        Stream Open();

        Stream OpenRead();

        Stream OpenWrite();

        void Replace(string destinationPath, string destinationBackupPath, bool ignoreMetadataErrors);
        #endregion
    }
}