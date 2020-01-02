#region Usings
using System.Threading;
using Shield.Framework.Services.Threading;
#endregion

namespace Shield.Framework.Services
{
    public interface IThreadService : IApplicationService
    {
        #region Properties
        IBackgroundThreadDispatcherService Background { get; }
        SynchronizationContext CurrentSynchronizationContext { get; }
        int CurrentThreadId { get; }
        ISynchronizationContextThreadDispatcherService CustomContext { get; }
        IMainThreadDispatcherService Main { get; }
        #endregion
    }
}