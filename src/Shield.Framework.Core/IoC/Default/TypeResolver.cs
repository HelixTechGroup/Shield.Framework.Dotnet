using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shield.Framework.IoC.Default
{
    internal sealed class TypeResolver : IResolver
    {
        public event Action<IDispose> OnDispose;

        private object m_instance;
        private bool m_disposed;

        public Func<object> CreateInstanceFunc { get; set; }

        public bool Singleton { get; set; }
        
        public bool Disposed
        {
            get { return m_disposed; }
        }

        ~TypeResolver()
        {
            Dispose(false);
        }

        public object GetObject()
        {
            if (!Singleton)
                return CreateInstanceFunc();

            if (m_instance != null)
                return m_instance;

            m_instance = CreateInstanceFunc();

            if (m_instance != null)
                CreateInstanceFunc = null;

            return m_instance;
        }

        private void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            if (disposing)
            {
                m_instance = null;
                CreateInstanceFunc = null;
            }

            OnDispose?.Invoke(this);
            m_disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
