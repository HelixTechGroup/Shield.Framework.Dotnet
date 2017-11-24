#region Usings
using System;
#endregion

namespace Shield.Framework.Logging
{
    public interface ILogEntry : IDispose
    {
        #region Properties
        Guid Id { get; }

        Category Category { get; }

        Priority Priority { get; }

        string LogDate { get; }

        string LogTime { get; }

        string Message { get; set; }
        #endregion
    }
}