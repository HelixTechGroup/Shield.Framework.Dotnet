#region Usings
using System;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Shield.Framework.Platform.Threading.Default
{
    public sealed class DefaultPlatformContextDispatcher : PlatformDispatcher, IPlatformContextDispatcher
    {
        #region Members
        private SynchronizationContext m_context;
        private SynchronizationContext m_currentContext;
        private SynchronizationContext m_previousContext;
        #endregion

        #region Properties
        public SynchronizationContext CurrentContext
        {
            get { return m_currentContext; }
        }

        public SynchronizationContext PreviousContext
        {
            get { return m_previousContext; }
        }
        #endregion

        public DefaultPlatformContextDispatcher()
        {
            m_previousContext = m_currentContext = SynchronizationContext.Current;
        }

        #region Methods
        public SynchronizationContext CreateContext()
        {
            m_context = new SynchronizationContext();
            return m_context;
        }

        public IPlatformContextDispatcher SetContext(SynchronizationContext context)
        {
            m_context = context;
            return this;
        }

        public override void Run(Action action, Action callback = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (CheckAccess())
            {
                cancellationToken.ThrowIfCancellationRequested();
                action();

                if (callback != null)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    callback();
                }
            }
            else
                RunAsync(action,
                         t =>
                         {
                             if (callback != null)
                             {
                                 cancellationToken.ThrowIfCancellationRequested();
                                 callback();
                             }
                         },
                         cancellationToken);
        }

        public override Task RunAsync(Action action, Action<Task> callback = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            VerifyDispatcher();
            Task task;
            m_previousContext = SynchronizationContext.Current;
            m_currentContext = m_context;

            try
            {
                SynchronizationContext.SetSynchronizationContext(m_currentContext);
                task = Task.Factory.StartNew(
                                             action,
                                             cancellationToken,
                                             TaskCreationOptions.None,
                                             TaskScheduler.FromCurrentSynchronizationContext());

                if (callback != null)
                    task.ContinueWith(callback, cancellationToken);
            }
            finally
            {
                m_currentContext = m_previousContext;
                m_previousContext = m_context;
                SynchronizationContext.SetSynchronizationContext(m_currentContext);
            }

            return task;
        }

        public override void Run<T>(Action<T> action,
                                    T parameter,
                                    Action callback = null,
                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            if (CheckAccess())
            {
                cancellationToken.ThrowIfCancellationRequested();
                action(parameter);

                if (callback != null)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    callback();
                }
            }
            else
            {
                RunAsync(action,
                         parameter,
                         t =>
                         {
                             if (callback != null)
                             {
                                 cancellationToken.ThrowIfCancellationRequested();
                                 callback();
                             }
                         },
                         cancellationToken);
            }
        }

        public override Task RunAsync<T>(Action<T> action,
                                         T parameter,
                                         Action<Task> callback = null,
                                         CancellationToken cancellationToken = default(CancellationToken))
        {
            VerifyDispatcher();
            Task task;
            m_previousContext = SynchronizationContext.Current;
            m_currentContext = m_context;

            try
            {
                SynchronizationContext.SetSynchronizationContext(m_currentContext);
                task = Task.Factory.StartNew(
                                             () => action(parameter),
                                             cancellationToken,
                                             TaskCreationOptions.None,
                                             TaskScheduler.FromCurrentSynchronizationContext());

                if (callback != null)
                    task.ContinueWith(callback, cancellationToken);
            }
            finally
            {
                m_currentContext = m_previousContext;
                m_previousContext = m_context;
                SynchronizationContext.SetSynchronizationContext(m_currentContext);
            }

            return task;
        }
        #endregion

        protected override void VerifyDispatcher()
        {
            if (m_context == null)
                throw new InvalidOperationException();
        }

        protected override bool CheckAccess()
        {
            return SynchronizationContext.Current == m_context;
        }
    }
}