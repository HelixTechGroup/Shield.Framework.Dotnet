using System;
using System.Threading.Tasks;

namespace Shield.Framework.Platform.Threading
{
    public abstract class PlatformDispatcher : IPlatformDispatcher, IDispose
    {
        public event Action<IDispose> OnDispose;

        protected bool m_disposed;

        public bool Disposed
        {
            get { return m_disposed; }
        }

        ~PlatformDispatcher()
        {
            Dispose(false);
        }

        public abstract void Run(Action action, Action callback = null);

        public abstract Task RunAsync(Action action, Action<Task> callback = null);

        public abstract void Run<T>(Action<T> action, T parameter, Action callback = null);

        public abstract Task RunAsync<T>(Action<T> action, T parameter, Action<Task> callback = null);

        protected abstract void VerifyDispatcher();

        protected abstract bool CheckAccess();

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

        protected async Task WrapCoroutine(Action coroutine)
        {
            await Task.Yield();
            await Task.Factory.StartNew(coroutine);
        }

        protected async Task WrapCoroutine<T>(Action<T> coroutine, T parameter)
        {
            await Task.Yield();
            await Task.Factory.StartNew(() => coroutine(parameter));
        }

        protected async Task<TResult> WrapCoroutine<T, TResult>(Func<T, TResult> coroutine, T parameter)
        {
            await Task.Yield();
            return await Task.Factory.StartNew(() => coroutine(parameter));
        }

        protected async Task WrapCoroutine<T>(Func<T, Task> coroutine, T parameter)
        {
            await Task.Yield();
            await coroutine(parameter);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            if (OnDispose != null)
                OnDispose(this);
            m_disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }        
    }
}
