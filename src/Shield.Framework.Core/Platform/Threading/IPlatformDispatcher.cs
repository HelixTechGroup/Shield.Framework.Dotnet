using System;
using System.Threading.Tasks;

namespace Shield.Framework.Platform.Threading
{
    public interface IPlatformDispatcher : IDispose
    {
        void Run(Action action, Action callback = null);

        Task RunAsync(Action action, Action<Task> callback = null);

        void Run<T>(Action<T> action, T parameter, Action callback = null);

        Task RunAsync<T>(Action<T> action, T parameter, Action<Task> callback = null);
    }
}
