namespace Shield.Framework.Services.Threading
{
    public interface IMainThreadDispatcherService : IThreadDispatcher
    {
        #region Properties
        bool IsMainThread { get; }
        int MainThreadId { get; }
        #endregion
    }
}