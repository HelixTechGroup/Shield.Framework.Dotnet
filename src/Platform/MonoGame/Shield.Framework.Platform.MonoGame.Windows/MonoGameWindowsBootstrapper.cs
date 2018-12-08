using Shield.Framework.Extensibility;
using Shield.Framework.Platform.Environement;
using Shield.Framework.Platform.Environment;
using Shield.Framework.Platform.Extensibility;
using Shield.Framework.Platform.IO;
using Shield.Framework.Platform.Threading;

namespace Shield.Framework.Platform
{
    public class MonoGameWindowsBootstrapper : MonoGameBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            m_container.Register<IAssemblyResolver, FileAssemblyResolver>();
            m_container.Register<IModuleLoader, FileModuleLoader>();
            m_container.Register<IPlatformDispatcherProvider, WindowsDispatcherProvider>();
            m_container.Register<IPlatformStorageProvider, WindowsStorageProvider>();
            m_container.Register<IPlatformOperatingSystemInformation, WindowsOperatingSystemInformation>();
        }
    }
}