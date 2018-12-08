using System;
using System.IO;
using System.Linq;
using Shield.Framework.Platform.Environment;
using SysEnv = System.Environment;

namespace Shield.Framework.Platform
{
	public class PlatformEnvironment : IPlatformEnvironment
	{                       
		protected PlatformApplicationType m_applicationType;
		protected PlatformFronendType m_frontendType;        	    	    
		protected IPlatformOperatingSystemInformation m_operatingSystem;
		protected PlatformRuntimeInformation m_runtime;

		public PlatformType Platform
		{
			get { return m_operatingSystem.Platform; }
		}        

		public PlatformApplicationType ApplicationType
		{
			get { return m_applicationType; }
			set { m_applicationType = value; }
		}

		public PlatformFronendType Frontend
		{
			get { return m_frontendType; }
			set { m_frontendType = value; }
		}

		public bool IsUnixBased
		{
			get { return m_operatingSystem.IsUnixBased; }
		}

		public bool Is64Bit
		{
			get { return SysEnv.Is64BitProcess; }
		}

		public string RootDirectory
		{
			get { return AppDomain.CurrentDomain.BaseDirectory; }
		}

		public string WorkingDirectory
		{
			get { return SysEnv.CurrentDirectory; }
		}

		public string NewLine
		{
			get { return SysEnv.NewLine; }
		}

		public string DirectorySeperator
		{
			get { return new string(Path.DirectorySeparatorChar, 1); }
		}

		public string PathSeperator
		{
			get { return new string(Path.PathSeparator, 1); }
		}

		public IPlatformOperatingSystemInformation OperatingSystem
		{
			get { return m_operatingSystem; }    
		}

		public PlatformRuntimeInformation Runtime
		{
			get { return m_runtime; }
		}

		protected PlatformEnvironment(IPlatformOperatingSystemInformation operatingSystem)
		{		    
			m_operatingSystem = operatingSystem;
			m_runtime = new PlatformRuntimeInformation();
		}

		public virtual void DetectPlatform()
		{
			m_runtime.DetectRuntime();		
			m_operatingSystem.DetectOperatingSystem();
		}	    
	}
}