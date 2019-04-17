#region Usings
using System;
#endregion

namespace Shield.Framework.Platform.IO
{
    [Flags]
    internal enum StorageEnityRefreshType
    {
        Attributes = 1,
        CreationTime = 2,
        Exists = 4,
        LastAccessTime = 8,
        LastWriteTime = 16,
        Name = 32,
        PathType = 64,
        All = Attributes  | CreationTime | Exists | LastAccessTime | LastWriteTime | Name | PathType
    }
}