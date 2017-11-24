using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shield.Framework.IoC.Default
{
    internal sealed class ResolverDictionary : ConcurrentDictionary<string, IResolver>, IDispose
    {
        public event Action<IDispose> OnDispose;

        private bool m_disposed;

        public bool Disposed
        {
            get { return m_disposed; }
        }

        ~ResolverDictionary()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            if (disposing)
                foreach (var value in Values)
                    value.Dispose();

            if (OnDispose != null)
                OnDispose(this);
            m_disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
