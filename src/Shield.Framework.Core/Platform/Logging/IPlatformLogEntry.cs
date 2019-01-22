#region Usings
using System;
#endregion

namespace Shield.Framework.Platform.Logging
{
    public interface IPlatformLogEntry : IDispose
    {
        #region Properties
        Guid Id { get; }

        PlatformLogCategory Category { get; }

        PlatformLogPriority Priority { get; }

        string LogDate { get; }

        string LogTime { get; }

        string Message { get; set; }
        #endregion
    }
}