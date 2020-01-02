#region Usings
using Shield.Framework.Services.IO;
#endregion

namespace Shield.Framework.Platform
{
    public abstract class PlatformServices<TDispatcherProvider, TLogProvider, TStorageProvider>
        : IApplicationSystems
        where TDispatcherProvider : IThreadDispatchSystem
        where TLogProvider : ILoggingSystem
        where TStorageProvider : IPlatformStorageProvider
    {
        #region Properties
        public TDispatcherProvider Dispatcher
        {
            get { return IoCProvider.Container.Resolve<TDispatcherProvider>(); }
        }

        public TLogProvider Logger
        {
            get { return IoCProvider.Container.Resolve<TLogProvider>(); }
        }

        public TStorageProvider Storage
        {
            get { return IoCProvider.Container.Resolve<TStorageProvider>(); }
        }
        #endregion
    }
}