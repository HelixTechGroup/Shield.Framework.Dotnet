#region Usings
using System;
using Shield.Framework.Services.Logging;
#endregion

namespace Shield.Framework.Platform.Logging.Loggers
{
    public abstract class PlatformLogger : Disposable, ILogProvider
    {
        #region Methods
        public abstract void Flush(ILogEntry entry);
        #endregion
    }
}