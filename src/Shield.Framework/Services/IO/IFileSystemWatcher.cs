#region Usings
using System;
using System.IO;
using Shin.Framework;
#endregion

namespace Shield.Framework.Services.IO
{
    public interface IFileSystemWatcher : IDispose
    {
        #region Events
        event EventHandler<FileChangedEventArgs> OnChanged;
        event EventHandler<FileChangedEventArgs> OnCreated;
        event EventHandler<FileChangedEventArgs> OnDeleted;
        event EventHandler<FileSystemErrorEventArgs> OnError;
        event EventHandler<FileRenamedEventArgs> OnRenamed;
        #endregion

        #region Properties
        bool EnableRaisingEvents { get; set; }
        string Filter { get; set; }
        bool IncludeSubdirectories { get; set; }
        int InternalBufferSize { get; set; }
        NotifyFilters NotifyFilter { get; set; }
        string Path { get; }
        #endregion
    }
}