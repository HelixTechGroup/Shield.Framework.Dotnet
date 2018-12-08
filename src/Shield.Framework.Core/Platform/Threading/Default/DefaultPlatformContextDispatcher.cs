using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shield.Framework.Platform.Threading.Default
{
    public sealed class DefaultPlatformContextDispatcher : PlatformDispatcher, IPlatformContextDispatcher
    {
        private SynchronizationContext m_previousContext;
        private SynchronizationContext m_currentContext;
        private SynchronizationContext m_context;

        public SynchronizationContext PreviousContext
        {
            get { return m_previousContext; }
        }

        public SynchronizationContext CurrentContext
        {
            get { return m_currentContext; }
        }

        public DefaultPlatformContextDispatcher()
        {
            m_previousContext = m_currentContext = SynchronizationContext.Current;
        }

        public SynchronizationContext CreateContext()
        {
            m_context = new SynchronizationContext();
            return m_context;
        }

        public void SetContext(SynchronizationContext context)
        {
            m_context = context;
        }

        public override void Run(Action action, Action callback = null)
        {
            if (CheckAccess())
            {
                action();
                if (callback != null)
                    callback();
            }
            else
            {                
                if (callback != null)
                    RunAsync(action, (t) => callback());
                else
                    RunAsync(action);
            }
        }

        public override Task RunAsync(Action action, Action<Task> callback = null)
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
                    CancellationToken.None,
                    TaskCreationOptions.None,
                    TaskScheduler.FromCurrentSynchronizationContext());

                if (callback != null)
                    task.ContinueWith(callback);
            }
            finally
            {
                m_currentContext = m_previousContext;
                m_previousContext = m_context;
                SynchronizationContext.SetSynchronizationContext(m_currentContext);
            }

            return task;
        }

        public override void Run<T>(Action<T> action, T parameter, Action callback = null)
        {
            if (CheckAccess())
            {
                action(parameter);
                if (callback != null)
                    callback();
            }
            else
            {
                if (callback != null)
                    RunAsync(action, parameter, (t) => callback());
                else
                    RunAsync(action, parameter);
            }
        }

        public override Task RunAsync<T>(Action<T> action, T parameter, Action<Task> callback = null)
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
                    CancellationToken.None,
                    TaskCreationOptions.None,
                    TaskScheduler.FromCurrentSynchronizationContext());

                if (callback != null)
                    task.ContinueWith(callback);
            }
            finally
            {
                m_currentContext = m_previousContext;
                m_previousContext = m_context;
                SynchronizationContext.SetSynchronizationContext(m_currentContext);
            }

            return task;
        }

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
