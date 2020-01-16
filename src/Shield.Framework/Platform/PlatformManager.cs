#region Usings
using System;
using System.Linq;
using Shield.Framework.Environment;
using Shin.Framework;
using Shin.Framework.Extensions;
#endregion

namespace Shield.Framework.Platform
{
    internal class PlatformManager : Initializable
    {
        #region Members
        private static PlatformManager m_platform;
        #endregion

        #region Properties
        public INativeApplication Application { get; private set; }

        public static PlatformManager CurrentPlatform
        {
            get { return m_platform ??= new PlatformManager(); }
        }

        public INativeThreadDispatcher Dispatcher { get; private set; }
        #endregion

        #region Methods
        protected override void InitializeResources()
        {
            Application.Initialize();
        }

        public void CreatePlatform(IOperatingSystemInformation operatingSystem, IRuntimeInformation runtimeInformation)
        {
            var assemble = AppDomain.CurrentDomain.GetAssemblies();
            var platform = AppDomain.CurrentDomain.GetAssemblies()
                                    .GetAssemblyAttribute<AssemblyPlatformAttribute>()
                                    .Where(attribute => attribute.RequiredOS == operatingSystem.Type)
                                    .OrderBy(attribute => attribute.Priority).FirstOrDefault();

            if (platform == null)
                throw new InvalidOperationException("No platform found. Are you missing assembly references?");

            Application = Activator.CreateInstance(platform.ApplicationType) as INativeApplication;
            Dispatcher = Activator.CreateInstance(platform.DispatcherType) as INativeThreadDispatcher;
        }
        #endregion
    }
}