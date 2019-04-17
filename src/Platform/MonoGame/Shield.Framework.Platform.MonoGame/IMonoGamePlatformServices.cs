#region Usings
using Shield.Framework.Platform.IO;
using Shield.Framework.Platform.Logging;
using Shield.Framework.Platform.Threading;
#endregion

namespace Shield.Framework.Platform
{
    public interface IMonoGamePlatformServices : IPlatformServices<IPlatformLogProvider, IPlatformDispatcherProvider, IMonoGameStorageProvider> { }
}