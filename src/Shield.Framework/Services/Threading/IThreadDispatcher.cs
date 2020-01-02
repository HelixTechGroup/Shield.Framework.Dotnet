#region Usings
using System;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Shield.Framework.Services.Threading
{
    public interface IThreadDispatcher
    {
        #region Methods
        void Run(Action action,
                 Action callback = null,
                 ThreadPriority priority = ThreadPriority.Normal,
                 CancellationToken cancellationToken = default);

        Task RunAsync(Action action,
                      Action<Task> callback = null,
                      ThreadPriority priority = ThreadPriority.Normal,
                      CancellationToken cancellationToken = default);

        void Run<T>(Action<T> action,
                    T parameter,
                    Action callback = null,
                    ThreadPriority priority = ThreadPriority.Normal,
                    CancellationToken cancellationToken = default);

        Task RunAsync<T>(Action<T> action,
                         T parameter,
                         Action<Task> callback = null,
                         ThreadPriority priority = ThreadPriority.Normal,
                         CancellationToken cancellationToken = default);

        T Run<T>(Func<T> func,
                 Action callback = null,
                 ThreadPriority priority = ThreadPriority.Normal,
                 CancellationToken cancellationToken = default);
        #endregion
    }
}