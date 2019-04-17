#region Usings
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
#endregion

namespace Shield.Framework.Platform.Threading
{
    public sealed class WindowsPlatformUiDispatcher : PlatformDispatcher, IPlatformUiDispatcher
    {
        #region Members
        private readonly Dispatcher m_dispatcher;
        #endregion

        public WindowsPlatformUiDispatcher()
        {
            m_dispatcher = Dispatcher.CurrentDispatcher;
        }

        #region Methods
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
            {
                Exception exception = null;
                Action method = () =>
                                {
                                    try
                                    {
                                        cancellationToken.ThrowIfCancellationRequested();
                                        action();

                                        if (callback != null)
                                        {
                                            cancellationToken.ThrowIfCancellationRequested();
                                            callback();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        exception = ex;
                                    }
                                };
                m_dispatcher.Invoke(method, DispatcherPriority.Normal, cancellationToken);
                if (exception != null)
                    throw new TargetInvocationException("An error occurred while dispatching a call to the UI Thread", exception);
            }
        }

        public override Task RunAsync(Action action, Action<Task> callback = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            VerifyDispatcher();
            var task = m_dispatcher.InvokeAsync(action, DispatcherPriority.Normal, cancellationToken).Task;
            if (callback != null)
                task.ContinueWith(callback, cancellationToken);

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
                Exception exception = null;
                Action method = () =>
                                {
                                    try
                                    {
                                        action(parameter);

                                        if (callback != null)
                                        {
                                            cancellationToken.ThrowIfCancellationRequested();
                                            callback();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        exception = ex;
                                    }
                                };
                m_dispatcher.Invoke(method);
                if (exception != null)
                    throw new TargetInvocationException("An error occurred while dispatching a call to the UI Thread", exception);
            }
        }

        public override Task RunAsync<T>(Action<T> action,
                                         T parameter,
                                         Action<Task> callback = null,
                                         CancellationToken cancellationToken = default(CancellationToken))
        {
            VerifyDispatcher();
            var taskSource = new TaskCompletionSource<object>();
            Action method = () =>
                            {
                                try
                                {
                                    action(parameter);
                                    taskSource.SetResult(null);
                                }
                                catch (Exception ex)
                                {
                                    taskSource.SetException(ex);
                                }
                            };
            m_dispatcher.BeginInvoke(method);
            var task = taskSource.Task;
            if (callback != null)
                task.ContinueWith(callback, cancellationToken);
            return task;
        }
        #endregion

        protected override void VerifyDispatcher()
        {
            if (m_dispatcher == null)
                throw new InvalidOperationException("Not initialized with dispatcher.");
        }

        protected override bool CheckAccess()
        {
            return m_dispatcher == null || m_dispatcher.CheckAccess();
        }
    }
}