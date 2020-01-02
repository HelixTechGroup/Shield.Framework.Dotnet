#region Usings
using System;
#endregion

namespace Shield.Framework.Services.Logging
{
    public interface ILogEntry : IDispose
    {
        #region Properties
        LogCategory Category { get; }
        Guid Id { get; }

        string LogDate { get; }

        string LogTime { get; }

        string Message { get; set; }

        LogPriority Priority { get; }
        #endregion
    }
}