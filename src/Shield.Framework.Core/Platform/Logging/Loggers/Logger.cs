using System;

namespace Shield.Framework.Platform.Logging.Loggers
{
    public abstract class Logger : ILogger
    {
        public event Action<IDispose> OnDispose;

        protected bool m_disposed;

        public bool Disposed
        {
            get { return m_disposed; }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            if (OnDispose != null)
                OnDispose(this);
            m_disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract void Flush(ILogEntry entry);
    }
}
