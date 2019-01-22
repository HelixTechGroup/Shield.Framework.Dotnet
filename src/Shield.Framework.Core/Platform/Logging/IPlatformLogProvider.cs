using System;

namespace Shield.Framework.Platform.Logging
{
    public interface IPlatformLogProvider: IDispose
    {
        void AddLogger(IPlatformLogger logProvider);

        void LogInfo(string message);

        void LogWarn(string message);

        void LogError(string message);

        void LogDebug(string message);

        void LogException(Exception exception);

        void Log(string message, PlatformLogCategory category, PlatformLogPriority priority);

        void Log(IPlatformLogEntry entry);
    }    
}
