namespace Shield.Framework.Platform.IO
{
    public interface IFileSystemMigrator : IDispose
    {
        #region Methods
        void CopyDirectory(string sourcePath, string destinationPath, bool overwrite, bool isRecursive);

        void MoveDirectory(string sourcePath, string destinationPath, bool overwrite);

        void CopyFile(string sourcePath, string destinationPath, bool overwrite);

        void MoveFile(string sourcePath, string destinationPath, bool overwrite);
        #endregion
    }
}