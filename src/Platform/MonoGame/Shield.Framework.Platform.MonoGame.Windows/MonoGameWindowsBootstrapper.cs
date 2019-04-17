#region Usings
using Shield.Framework.Extensibility;
using Shield.Framework.Platform.Environement;
using Shield.Framework.Platform.Environment;
using Shield.Framework.Platform.Extensibility;
using Shield.Framework.Platform.Threading;
#endregion

namespace Shield.Framework.Platform
{
    public class MonoGameWindowsBootstrapper : MonoGameBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            m_container.Register<IAssemblyResolver, FileAssemblyResolver>();
            m_container.Register<IModuleLoader, FileModuleLoader>();
            m_container.Register<IPlatformUiDispatcher, WindowsPlatformUiDispatcher>();
            m_container.Register<IPlatformDispatcherProvider, WindowsDispatcherProvider>();
            m_container.Register<IPlatformOperatingSystemInformation, WindowsOperatingSystemInformation>();
        }

        protected override void ConfigurePlatform()
        {
            base.ConfigurePlatform();
            PlatformProvider.Environment.DetectPlatform();
        }
    }
}