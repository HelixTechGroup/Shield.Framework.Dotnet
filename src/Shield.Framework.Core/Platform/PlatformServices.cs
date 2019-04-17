#region Usings
using Shield.Framework.Platform.IO;
using Shield.Framework.Platform.Logging;
using Shield.Framework.Platform.Threading;
#endregion

namespace Shield.Framework.Platform
{
    public abstract class PlatformServices<TLoggProvider, TDispatcherProvider, TStorageProvider>
        : IPlatformServices<TLoggProvider, TDispatcherProvider, TStorageProvider>
        where TLoggProvider : IPlatformLogProvider
        where TDispatcherProvider : IPlatformDispatcherProvider
        where TStorageProvider : IPlatformStorageProvider
    {
        #region Properties
        public TDispatcherProvider Dispatcher
        {
            get { return IoCProvider.Container.Resolve<TDispatcherProvider>(); }
        }

        public TLoggProvider Logger
        {
            get { return IoCProvider.Container.Resolve<TLoggProvider>(); }
        }

        public TStorageProvider Storage
        {
            get { return IoCProvider.Container.Resolve<TStorageProvider>(); }
        }
        #endregion
    }
}