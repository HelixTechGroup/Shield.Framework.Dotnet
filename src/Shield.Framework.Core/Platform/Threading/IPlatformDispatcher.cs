#region Usings
using System;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Shield.Framework.Platform.Threading
{
    public interface IPlatformDispatcher : IDispose
    {
        #region Methods
        void Run(Action action, Action callback = null, CancellationToken cancellationToken = default(CancellationToken));

        Task RunAsync(Action action, Action<Task> callback = null, CancellationToken cancellationToken = default(CancellationToken));

        void Run<T>(Action<T> action, T parameter, Action callback = null, CancellationToken cancellationToken = default(CancellationToken));

        Task RunAsync<T>(Action<T> action,
                         T parameter,
                         Action<Task> callback = null,
                         CancellationToken cancellationToken = default(CancellationToken));
        #endregion
    }
}