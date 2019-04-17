#region Usings
using System;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Shield.Framework.Platform.Threading.Default
{
    public sealed class DefaultPlatformBackgroundDispatcher : PlatformDispatcher, IPlatformBackgroundDispatcher
    {
        #region Methods
        public override void Run(Action action, Action callback = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var task = Task.Run(() => action, cancellationToken);
            if (callback != null)
                task.ContinueWith(t => callback, cancellationToken);

            task.Wait(cancellationToken);
        }

        public override Task RunAsync(Action action, Action<Task> callback = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var task = WrapCoroutine(action, cancellationToken);
            if (callback != null)
                task.ContinueWith(callback, cancellationToken);

            return task;
        }

        public override void Run<T>(Action<T> action,
                                    T parameter,
                                    Action callback = null,
                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            var task = Task.Run(() => action(parameter), cancellationToken);
            if (callback != null)
                task.ContinueWith(t => callback, cancellationToken);

            task.Wait(cancellationToken);
        }

        public override Task RunAsync<T>(Action<T> action,
                                         T parameter,
                                         Action<Task> callback = null,
                                         CancellationToken cancellationToken = default(CancellationToken))
        {
            var task = WrapCoroutine(() => action(parameter), cancellationToken);
            if (callback != null)
                task.ContinueWith(callback, cancellationToken);

            return task;
        }
        #endregion

        protected override void VerifyDispatcher() { }

        protected override bool CheckAccess()
        {
            return true;
        }
    }
}