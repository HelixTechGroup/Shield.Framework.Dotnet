#region Usings
using System;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Shield.Framework.Services.Threading
{
    public abstract class ThreadDispatcher : IThreadDispatcher
    {
        #region Methods
        public abstract void Run(Action action, Action callback = null, CancellationToken cancellationToken = default);

        public abstract Task RunAsync(Action action, Action<Task> callback = null, CancellationToken cancellationToken = default);

        public abstract void Run<T>(Action<T> action,
                                    T parameter,
                                    Action callback = null,
                                    CancellationToken cancellationToken = default);

        public abstract Task RunAsync<T>(Action<T> action,
                                         T parameter,
                                         Action<Task> callback = null,
                                         CancellationToken cancellationToken = default);

        protected abstract void VerifyDispatcher();

        protected abstract bool CheckAccess();

        /// <inheritdoc />
        public void Run(Action action,
                        Action callback = null,
                        ThreadPriority priority = ThreadPriority.Normal,
                        CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task RunAsync(Action action,
                             Action<Task> callback = null,
                             ThreadPriority priority = ThreadPriority.Normal,
                             CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Run<T>(Action<T> action,
                           T parameter,
                           Action callback = null,
                           ThreadPriority priority = ThreadPriority.Normal,
                           CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task RunAsync<T>(Action<T> action,
                                T parameter,
                                Action<Task> callback = null,
                                ThreadPriority priority = ThreadPriority.Normal,
                                CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public T Run<T>(Func<T> func,
                        Action callback = null,
                        ThreadPriority priority = ThreadPriority.Normal,
                        CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        protected async Task WrapCoroutine(Func<Task> coroutine)
        {
            await Task.Yield();
            await coroutine().ConfigureAwait(false);
        }

        protected async Task<T> WrapCoroutine<T>(Func<Task<T>> coroutine)
        {
            await Task.Yield();
            return await coroutine().ConfigureAwait(false);
        }

        protected async Task WrapCoroutine(Action coroutine, CancellationToken cancellationToken)
        {
            await Task.Yield();
            await Task.Factory.StartNew(coroutine, cancellationToken).ConfigureAwait(false);
        }

        protected async Task WrapCoroutine<T>(Action<T> coroutine, T parameter, CancellationToken cancellationToken)
        {
            await Task.Yield();
            await Task.Factory.StartNew(() => coroutine(parameter), cancellationToken).ConfigureAwait(false);
        }

        protected async Task<TResult> WrapCoroutine<T, TResult>(Func<T, TResult> coroutine, T parameter, CancellationToken cancellationToken)
        {
            await Task.Yield();
            return await Task.Factory.StartNew(() => coroutine(parameter), cancellationToken).ConfigureAwait(false);
        }

        protected async Task WrapCoroutine<T>(Func<T, Task> coroutine, T parameter)
        {
            await Task.Yield();
            await coroutine(parameter).ConfigureAwait(false);
        }
        #endregion
    }
}