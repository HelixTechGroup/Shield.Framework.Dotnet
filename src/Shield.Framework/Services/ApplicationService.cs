using System;
using System.Collections.Generic;
using System.Text;

namespace Shield.Framework.Services
{
    public abstract class ApplicationService : IApplicationService
    {
        private bool m_isDisposed;
        private bool m_isInitialized;

        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public event EventHandler Disposing;

        /// <inheritdoc />
        public event EventHandler Disposed;

        /// <inheritdoc />
        public bool IsDisposed
        {
            get { return m_isDisposed; }
        }

        /// <inheritdoc />
        public event EventHandler Initializing;

        /// <inheritdoc />
        public event EventHandler Initialized;

        /// <inheritdoc />
        public bool IsInitialized
        {
            get { return m_isInitialized; }
        }

        /// <inheritdoc />
        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
