using System;

namespace Shield.Framework.Platform.Environment
{
    public interface IPlatformOperatingSystemInformation
    {
        bool Is64Bit { get; }
        bool IsUnixBased { get; }
        string Name { get; }
        PlatformOperatingSystemType Type { get; }
        PlatformType Platform { get; }
        Version Version { get; }

        void DetectOperatingSystem();
    }
}