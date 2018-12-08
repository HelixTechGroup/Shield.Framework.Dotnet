using System;
using System.Runtime.InteropServices;
using SysRuntime = System.Runtime.InteropServices.RuntimeInformation;
using SysEnv = System.Environment;

namespace Shield.Framework.Platform.Environment
{
	public abstract class PlatformOperatingSystemInformation : IPlatformOperatingSystemInformation
	{
		protected PlatformID m_id;
		protected PlatformOperatingSystemType m_type;
		protected PlatformType m_platform;
		protected bool m_is64Bit;
		protected bool m_isUnixBased;
		protected string m_name;
		protected Version m_version;

		public PlatformType Platform
		{
			get { return m_platform; }
		}

		public PlatformOperatingSystemType Type
		{
			get { return m_type; }
		}

		public string Name
		{
			get { return m_name; }	
		}

		public Version Version
		{
			get { return m_version; }
		}

		public bool IsUnixBased
		{
			get { return m_isUnixBased; }
		}

		public bool Is64Bit
		{
			get { return m_is64Bit; }
		}

		public virtual void DetectOperatingSystem()
		{			
			GetOsType();
			GetOsDetails();			
		}

		protected virtual void GetOsType()
		{
			m_platform = PlatformType.Desktop;
			m_id = SysEnv.OSVersion.Platform;         
			switch ((int)m_id)
			{
				case 6: // PlatformID.MacOSX:
					m_type = PlatformOperatingSystemType.MacOS;
					m_isUnixBased = true;
					break;
				case 4: // PlatformID.Unix:	
					m_type = PlatformOperatingSystemType.Unix;
					m_isUnixBased = true;
					break;
				case 0: // PlatformID.Win32S:
				case 1: // PlatformID.Win32Windows:
				case 2: // PlatformID.Win32NT:
				case 3: // PlatformID.WinCE:
					m_type = PlatformOperatingSystemType.Windows;
					break;
				default:
					m_type = PlatformOperatingSystemType.Unknown;
					m_platform = PlatformType.Unknown;
					break;
			}
		}

		protected virtual void GetOsDetails()
		{
			m_is64Bit = SysRuntime.OSArchitecture.HasFlag(Architecture.X64) || SysRuntime.OSArchitecture.HasFlag(Architecture.Arm64);
			m_version = SysEnv.OSVersion.Version;
			m_name = SysRuntime.OSDescription;
		}
	}
}