using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shield.Framework.Logging.Providers
{
    public abstract class LogProvider : ILogProvider
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
