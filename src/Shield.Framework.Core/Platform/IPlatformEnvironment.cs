using System;
using System.Threading;
using Shield.Framework.Platform.Environment;

namespace Shield.Framework.Platform
{
	public interface IPlatformEnvironment
	{                
		PlatformType Platform { get; }

		PlatformApplicationType ApplicationType { get; }

		PlatformFronendType Frontend { get; }

		bool IsUnixBased { get; }

		bool Is64Bit { get; }

		string RootDirectory { get; }

		string WorkingDirectory { get; }

		string NewLine { get; }

		string DirectorySeperator { get; }

		string PathSeperator { get; }

		IPlatformOperatingSystemInformation OperatingSystem { get; }

		PlatformRuntimeInformation Runtime { get; }

		void DetectPlatform();
	}
}