using System;
using System.Threading.Tasks;

namespace Shield.Framework.Platform.Threading
{
    public sealed class DefaultPlatformBackgroundDispatcher : PlatformDispatcher
    {
        public override void Run(Action action, Action callback = null)
        {
            var task = Task.Run(() => action);
            if (callback != null)
                task.ContinueWith((t) => callback);

            task.Wait();
        }

        public override Task RunAsync(Action action, Action<Task> callback = null)
        {
            var task = WrapCoroutine(action);
            if (callback != null)
                task.ContinueWith(callback);

            return task;
        }

        public override void Run<T>(Action<T> action, T parameter, Action callback = null)
        {
            var task = Task.Run(() => action(parameter));
            if (callback != null)
                task.ContinueWith((t) => callback);

            task.Wait();
        }

        public override Task RunAsync<T>(Action<T> action, T parameter, Action<Task> callback = null)
        {
            var task = WrapCoroutine(() => action(parameter));
            if (callback != null)
                task.ContinueWith(callback);

            return task;
        }        
        
        protected override void VerifyDispatcher() { }

        protected override bool CheckAccess()
        {
            return true;
        }
    }
}
