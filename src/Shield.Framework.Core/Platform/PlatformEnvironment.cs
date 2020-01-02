#region Usings
using System;
using System.IO;
using Shield.Framework.Environment;
using SysEnv = System.Environment;
#endregion

namespace Shield.Framework.Platform
{
    public class PlatformEnvironment : IPlatformEnvironment
    {
        #region Members
        protected ApplicationType m_applicationType;
        protected PlatformFronendType m_frontendType;
        protected IOperatingSystemInformation m_operatingSystem;
        protected RuntimeInformation m_runtime;
        #endregion

        #region Properties
        public ApplicationType ApplicationType
        {
            get { return m_applicationType; }
            set { m_applicationType = value; }
        }

        public string DirectorySeparator
        {
            get { return new string(Path.DirectorySeparatorChar, 1); }
        }

        public PlatformFronendType Frontend
        {
            get { return m_frontendType; }
            set { m_frontendType = value; }
        }

        public bool Is64Bit
        {
            get { return SysEnv.Is64BitProcess; }
        }

        public bool IsUnixBased
        {
            get { return m_operatingSystem.IsUnixBased; }
        }

        public string NewLine
        {
            get { return SysEnv.NewLine; }
        }

        public IOperatingSystemInformation OperatingSystem
        {
            get { return m_operatingSystem; }
        }

        public string PathSeperator
        {
            get { return new string(Path.PathSeparator, 1); }
        }

        public PlatformType Platform
        {
            get { return m_operatingSystem.Platform; }
        }

        public string RootDirectory
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        public RuntimeInformation Runtime
        {
            get { return m_runtime; }
        }

        public string WorkingDirectory
        {
            get { return SysEnv.CurrentDirectory; }
        }
        #endregion

        protected PlatformEnvironment(IOperatingSystemInformation operatingSystem)
        {
            m_operatingSystem = operatingSystem;
            m_runtime = new RuntimeInformation();
        }

        #region Methods
        public virtual void DetectPlatform()
        {
            m_runtime.DetectRuntime();
            m_operatingSystem.DetectOperatingSystem();
        }
        #endregion
    }
}