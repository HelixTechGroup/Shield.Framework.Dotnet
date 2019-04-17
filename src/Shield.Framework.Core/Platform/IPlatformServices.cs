#region Usings
using Shield.Framework.Platform.IO;
using Shield.Framework.Platform.Logging;
using Shield.Framework.Platform.Threading;
#endregion

namespace Shield.Framework.Platform
{
    public interface IPlatformServices { }

    public interface IPlatformServices<out TLoggProvider, out TDispatcherProvider, out TStorageProvider>
        : IPlatformServices
        where TLoggProvider : IPlatformLogProvider
        where TDispatcherProvider : IPlatformDispatcherProvider
        where TStorageProvider : IPlatformStorageProvider
    {
        #region Properties
        TDispatcherProvider Dispatcher { get; }
        TLoggProvider Logger { get; }
        TStorageProvider Storage { get; }
        #endregion
    }
}