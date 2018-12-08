using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Shield.Framework.Platform.Threading
{
    public sealed class WindowsPlatformUIDispatcher : PlatformDispatcher
    {
        private readonly Dispatcher m_dispatcher;

        public WindowsPlatformUIDispatcher()
        {
            m_dispatcher = Dispatcher.CurrentDispatcher;
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
                Exception exception = null;
                System.Action method = () => {
                    try
                    {
                        action();
                        if (callback != null)
                            callback();
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

        public override Task RunAsync(Action action, Action<Task> callback = null)
        {
            VerifyDispatcher();
            var task = m_dispatcher.InvokeAsync(action).Task;
            if (callback != null)
                task.ContinueWith(callback);

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
                Exception exception = null;
                System.Action method = () => {
                    try
                    {
                        action(parameter);
                        if (callback != null)
                            callback();
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

        public override Task RunAsync<T>(Action<T> action, T parameter, Action<Task> callback = null)
        {
            VerifyDispatcher();
            var taskSource = new TaskCompletionSource<object>();
            System.Action method = () => {
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
                task.ContinueWith(callback);
            return task;
        }

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
