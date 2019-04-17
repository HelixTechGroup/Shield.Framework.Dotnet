#region Usings
using System;
#endregion

namespace Shield.Framework.Platform.IO
{
    [Flags]
    public enum WatcherNofifyFilters
    {
        FileName = 1,
        DirectoryName = 2,
        Attributes = 4,
        Size = 8,
        LastWrite = 16,
        LastAccess = 32,
        CreationTime = 64,
        Security = 256,
        Default = FileName | DirectoryName | LastWrite
    }
}