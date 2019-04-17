#region Usings
using System;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Shield.Framework.Platform.Threading
{
    public abstract class PlatformDispatcher : IPlatformDispatcher, IDispose
    {
        #region Events
        public event Action<IDispose> OnDispose;
        #endregion

        #region Members
        protected bool m_disposed;
        #endregion

        #region Properties
        public bool Disposed
        {
            get { return m_disposed; }
        }
        #endregion

        ~PlatformDispatcher()
        {
            Dispose(false);
        }

        #region Methods
        public abstract void Run(Action action, Action callback = null, CancellationToken cancellationToken = default(CancellationToken));

        public abstract Task RunAsync(Action action, Action<Task> callback = null, CancellationToken cancellationToken = default(CancellationToken));

        public abstract void Run<T>(Action<T> action,
                                    T parameter,
                                    Action callback = null,
                                    CancellationToken cancellationToken = default(CancellationToken));

        public abstract Task RunAsync<T>(Action<T> action,
                                         T parameter,
                                         Action<Task> callback = null,
                                         CancellationToken cancellationToken = default(CancellationToken));

        protected abstract void VerifyDispatcher();

        protected abstract bool CheckAccess();

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            OnDispose?.Invoke(this);
            m_disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        protected async Task WrapCoroutine(Func<Task> coroutine)
        {
            await Task.Yield();
            await coroutine();
        }

        protected async Task<T> WrapCoroutine<T>(Func<Task<T>> coroutine)
        {
            await Task.Yield();
            return await coroutine();
        }

        protected async Task WrapCoroutine(Action coroutine, CancellationToken cancellationToken)
        {
            await Task.Yield();
            await Task.Factory.StartNew(coroutine, cancellationToken);
        }

        protected async Task WrapCoroutine<T>(Action<T> coroutine, T parameter, CancellationToken cancellationToken)
        {
            await Task.Yield();
            await Task.Factory.StartNew(() => coroutine(parameter), cancellationToken);
        }

        protected async Task<TResult> WrapCoroutine<T, TResult>(Func<T, TResult> coroutine, T parameter, CancellationToken cancellationToken)
        {
            await Task.Yield();
            return await Task.Factory.StartNew(() => coroutine(parameter), cancellationToken);
        }

        protected async Task WrapCoroutine<T>(Func<T, Task> coroutine, T parameter)
        {
            await Task.Yield();
            await coroutine(parameter);
        }
    }
}