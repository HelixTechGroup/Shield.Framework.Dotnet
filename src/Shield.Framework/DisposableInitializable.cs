using System;
using Shield.Framework.Extensions;

namespace Shield.Framework
{
    public abstract class DisposableInitializable : Disposable, IInitialize
    {
        protected bool m_isInitialized;

        public bool IsInitialized
        {
            get { return m_isInitialized; }
        }

        public event EventHandler Initializing;
        public event EventHandler Initialized;

        protected DisposableInitializable() : base()
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

        protected override void DisposeManagedResources()
        {
            Initializing.Dispose();
            Initialized.Dispose();
        }

        protected virtual void InitializeResources() { }

        private void WireUpInitializeEvents()
        {
            Initializing += OnInitializing;
            Initialized += OnInitialized;
        }
    }
}
