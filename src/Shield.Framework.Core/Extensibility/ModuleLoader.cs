using System;
using Shield.Framework.Extensions;

namespace Shield.Framework.Extensibility
{
    public abstract class ModuleLoader : IModuleLoader
    {
        public event Action<IDispose> OnDispose;
        public event EventHandler<ModuleDownloadProgressChangedEventArgs> ModuleDownloadProgressChanged;
        public event EventHandler<LoadModuleCompletedEventArgs> LoadModuleCompleted;

        protected bool m_disposed;
               
        public bool Disposed
        {
            get { return m_disposed; }
        }

        ~ModuleLoader()
        {
            Dispose(false);
        }

        public abstract bool CanLoadModule(ModuleInfo moduleInfo);

        public abstract void LoadModule(ModuleInfo moduleInfo);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            if (OnDispose != null)
                OnDispose(this);
            m_disposed = true;
        }

        protected void RaiseModuleDownloadProgressChanged(ModuleInfo moduleInfo, long bytesReceived, long totalBytesToReceive)
        {
            RaiseModuleDownloadProgressChanged(new ModuleDownloadProgressChangedEventArgs(moduleInfo, bytesReceived, totalBytesToReceive));
        }

        protected void RaiseModuleDownloadProgressChanged(ModuleDownloadProgressChangedEventArgs e)
        {
            ModuleDownloadProgressChanged.Raise(this, e);
        }

        protected void RaiseLoadModuleCompleted(ModuleInfo moduleInfo, Exception error)
        {
            RaiseLoadModuleCompleted(new LoadModuleCompletedEventArgs(moduleInfo, error));
        }

        protected void RaiseLoadModuleCompleted(LoadModuleCompletedEventArgs e)
        {
            LoadModuleCompleted.Raise(this, e);
        }
    }
}