#region Usings
using System;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Shield.Framework.Services.Threading.Native
{
    public sealed class DefaultPlatformBackgroundDispatcher : ThreadDispatcher, IBackgroundThreadDispatcherService
    {
        #region Methods
        public override void Run(Action action, Action callback = null, CancellationToken cancellationToken = default)
        {
            var task = Task.Run(() => action, cancellationToken);
            if (callback != null)
                task.ContinueWith(t => callback, cancellationToken).ConfigureAwait(false);

            task.Wait(cancellationToken);
        }

        public override async Task RunAsync(Action action, Action<Task> callback = null, CancellationToken cancellationToken = default)
        {
            var task = WrapCoroutine(action, cancellationToken);
            if (callback != null)
                await task.ContinueWith(callback, cancellationToken).ConfigureAwait(false);

            await task;
        }

        public override void Run<T>(Action<T> action,
                                    T parameter,
                                    Action callback = null,
                                    CancellationToken cancellationToken = default)
        {
            var task = Task.Run(() => action(parameter), cancellationToken);
            if (callback != null)
                task.ContinueWith(t => callback, cancellationToken);

            task.Wait(cancellationToken);
        }

        public override async Task RunAsync<T>(Action<T> action,
                                               T parameter,
                                               Action<Task> callback = null,
                                               CancellationToken cancellationToken = default)
        {
            var task = WrapCoroutine(() => action(parameter), cancellationToken);
            if (callback != null)
                await task.ContinueWith(callback, cancellationToken).ConfigureAwait(false);

            await task;
        }

        protected override void VerifyDispatcher() { }

        protected override bool CheckAccess()
        {
            return true;
        }
        #endregion
    }
}