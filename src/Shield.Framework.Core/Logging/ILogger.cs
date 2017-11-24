using System;

namespace Shield.Framework.Logging
{
    public interface ILogger : IDispose
    {
        void AddProvider(ILogProvider logProvider);

        void LogInfo(string message);

        void LogWarn(string message);

        void LogError(string message);

        void LogDebug(string message);

        void LogException(Exception exception);

        void Log(string message, Category category, Priority priority);

        void Log(ILogEntry entry);
    }    
}
