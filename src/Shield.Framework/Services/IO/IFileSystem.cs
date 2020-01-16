#region Usings
using System;
using System.Collections.Generic;
using System.IO;
using Shin.Framework;
#endregion

namespace Shield.Framework.Services.IO
{
    public interface IFileSystem : IDispose, IEquatable<IFileSystem>
    {
        #region Properties
        Guid Id { get; }
        bool IsReadOnly { get; }
        string Root { get; }
        #endregion

        #region Methods
        void CopyDirectory(string sourcePath, string destinationPath, bool overwrite, bool isRecursive);

        void CreateDirectory(string directoryPath);

        bool DirectoryExists(string directoryPath);

        long GetDirectoryLenth(string directoryPath, bool isRecursive);

        void MoveDirectory(string sourcePath, string destinationPath, bool overwrite);

        void DeleteDirectory(string directoryPath, bool isRecursive);

        void CopyFile(string sourcePath, string destinationPath, bool overwrite);

        void ReplaceFile(string sourcePath, string destinationPath, string destinationBackupPath, bool ignoreMetadataErrors);

        long GetFileLength(string filePath);

        bool FileIsInUse(string filePath);

        bool FileExists(string filePath);

        void MoveFile(string sourcePath, string destinationPath, bool overwrite);

        void DeleteFile(string filePath);

        Stream OpenFile(string filePath, FileMode mode, FileAccess access, FileShare share);

        Stream CreateAndOpenFile(string filePath);

        void CreateFile(string filePath);

        FileAttributes GetAttributes(string path);

        void SetAttributes(string path, FileAttributes attributes);

        DateTime GetCreationTime(string path);

        void SetCreationTime(string path, DateTime time);

        DateTime GetLastAccessTime(string path);

        void SetLastAccessTime(string path, DateTime time);

        DateTime GetLastWriteTime(string path);

        void SetLastWriteTime(string path, DateTime time);

        IEnumerable<string> EnumeratePath(string path, string searchPattern, SearchOption searchOption, StorageSearchTarget searchTarget);

        bool CanWatch(string path);

        IFileSystemWatcher Watch(string path);

        string ConvertPathToInternal(string path);

        string ConvertFromInternal(string fileSystemPath);
        #endregion
    }
}