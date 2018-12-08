using System.Collections.Generic;

namespace Shield.Framework.Platform.IO
{
    public interface IPlatformStorageCollection : IEnumerable<IPlatformStorage>, IDispose
    {
        IPlatformStorage this[string index] { get; }

        string MountStorage(string path, IPlatformStorage storageManager);

        string MountStorage(IPlatformStorage storageManager);

        void UnmountStorage(string path);

        IPlatformStorage GetStorage(string path);
    }
}