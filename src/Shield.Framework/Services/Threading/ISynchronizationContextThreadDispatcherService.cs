#region Usings
using System.Threading;
#endregion

namespace Shield.Framework.Services.Threading
{
    public interface ISynchronizationContextThreadDispatcherService : IThreadDispatcher
    {
        #region Properties
        SynchronizationContext CurrentContext { get; }
        SynchronizationContext PreviousContext { get; }
        #endregion

        #region Methods
        SynchronizationContext CreateContext();

        T CreateContext<T>() where T : SynchronizationContext, new();

        ISynchronizationContextThreadDispatcherService SetContext<T>(T context) where T : SynchronizationContext;
        #endregion
    }
}