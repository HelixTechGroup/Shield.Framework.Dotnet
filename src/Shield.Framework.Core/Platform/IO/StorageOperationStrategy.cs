using System;

namespace Shield.Framework.Platform.IO
{
    [Flags]
    public enum StorageOperationStrategy
    {
        OverwriteExisting = 1,
        BackupSource = 2,
        BackupDestination = 4,
        BackupAll = BackupSource | BackupDestination,
        RollbackOnFailure = 8,
        IgnoreErrors = 16,
        Default = RollbackOnFailure,
        DefaultOverwrite = Default | OverwriteExisting,
        Full = BackupAll | RollbackOnFailure,
        FullOverwrite = Full | OverwriteExisting
    }
}