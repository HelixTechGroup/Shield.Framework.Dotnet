using System;
using Shield.Framework.Extensions;
using System.Collections.Generic;
using System.Text;

namespace Shield.Framework
{
    public abstract class Initializable : IInitialize
    {
        protected bool m_isInitialized;

        public bool IsInitialized
        {
            get { return m_isInitialized; }
        }

        public event EventHandler Initializing;
        public event EventHandler Initialized;

        protected Initializable()
        {
            WireUpInitializeEvents();
        }

        public void Initialize()
        {
            if (m_isInitialized)
                return;

            Initializing.Raise(this, EventArgs.Empty);
            InitializeResources();
            Initialized.Raise(this, EventArgs.Empty);
            m_isInitialized = true;
        }

        protected virtual void OnInitializing(object sender, EventArgs e) { }

        protected virtual void OnInitialized(object sender, EventArgs e) { }

        protected virtual void InitializeResources() { }

        private void WireUpInitializeEvents()
        {
            Initializing += OnInitializing;
            Initialized += OnInitialized;
        }
    }
}
