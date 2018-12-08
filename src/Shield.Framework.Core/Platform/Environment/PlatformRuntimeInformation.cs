using System;
using System.Linq;
using SysRuntime = System.Runtime.InteropServices.RuntimeInformation;
using SysEnv = System.Environment;

namespace Shield.Framework.Platform.Environment
{
    public class PlatformRuntimeInformation
    {
        protected PlatformRuntimeType m_runtimeType;
        protected Version m_runtimeVersion;

        public PlatformRuntimeType Runtime
        {
            get { return m_runtimeType; }
        }

        public Version RuntimeVersion
        {
            get { return m_runtimeVersion; }
        }

        public virtual void DetectRuntime()
        {
            var runtimeName = SysRuntime.FrameworkDescription;
            var types = new[] { ".NET Core", ".NET Framework" };
            var test = types.FirstOrDefault(s => runtimeName.Contains(s));

            switch (test)
            {
                case ".NET Core":
                    m_runtimeType = PlatformRuntimeType.NetCore;
                    break;
                case ".NET Framework":
                    m_runtimeType = PlatformRuntimeType.NetFramework;
                    break;
                default:
                    m_runtimeType = PlatformRuntimeType.Unknown;
                    break;
            }

            if (System.Type.GetType("Mono.Runtime") != null 
                || Type.GetType("Mono.Interop.IDispatch", false) != null)
                m_runtimeType = PlatformRuntimeType.Mono;

            m_runtimeVersion = SysEnv.Version;
        }
    }
}