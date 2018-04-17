using System;

namespace Shield.Framework.Logging
{
    public interface ILogProvider: IDispose
    {
        void AddLogger(ILogger logProvider);

        void LogInfo(string message);

        void LogWarn(string message);

        void LogError(string message);

        void LogDebug(string message);

        void LogException(Exception exception);

        void Log(string message, Category category, Priority priority);

        void Log(ILogEntry entry);
    }    
}
