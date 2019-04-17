#region Usings
using Shield.Framework.Platform.IO;
using Shield.Framework.Platform.Logging;
using Shield.Framework.Platform.Threading;
#endregion

namespace Shield.Framework.Platform
{
    public class MonoGamePlatformServices : PlatformServices<IPlatformLogProvider, IPlatformDispatcherProvider, IMonoGameStorageProvider>,
                                            IMonoGamePlatformServices { }
}