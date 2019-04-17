#region Usings
using System;
#endregion

namespace Shield.Framework.Platform.IO
{
    [Flags]
    public enum WatcherChangeTypes
    {
        Created = 1,
        Deleted = 2,
        Changed = 4,
        Renamed = 8,
        All = Created | Deleted | Changed | Renamed
    }
}