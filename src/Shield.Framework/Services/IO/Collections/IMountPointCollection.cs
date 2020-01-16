#region Usings
using System.Collections.Generic;
using Shin.Framework;
#endregion

namespace Shield.Framework.Services.IO.Collections
{
    public interface IMountPointCollection : IEnumerable<IStorageManager>, IDispose
    {
        #region Properties
        int Count { get; }
        IStorageManager this[string index] { get; }
        string[] ProtectedMountPoints { get; }
        string RootDirectory { get; }
        #endregion

        #region Methods
        void MountStorage(params KeyValuePair<string, IStorageManager>[] mounts);

        IEnumerable<string> MountStorage(params IStorageManager[] storageManagers);

        void MountStorage<T>(string path, T storageManager) where T : IStorageManager;

        string MountStorage<T>(T storageManager) where T : IStorageManager;

        bool UnmountStorage(string path);

        bool UnmountStorage(params string[] paths);

        bool UnmountStorage();

        IStorageManager GetStorage(string path);

        IEnumerable<IStorageManager> GetStorage(params string[] paths);

        IEnumerable<string> GetMountPoint<T>(T storageManager) where T : IStorageManager;

        IEnumerable<IStorageManager> GetStorage();

        IEnumerable<T> GetStorage<T>() where T : IStorageManager;

        IEnumerable<string> GetMountPoints();

        bool ContainsMountPoint(string path);

        bool ContainsStorage<T>(T storageManager) where T : IStorageManager;
        #endregion
    }
}