#region Usings
using System;
using Shield.Framework.Services.LifeCycle;
#endregion

namespace Shield.Framework.Services
{
    public interface ILifeCycleService : IApplicationService
    {
        #region Events
        event EventHandler<ApplicationStartupEventArgs> Starting;
        event EventHandler<ApplicationStartupEventArgs> Started;
        event EventHandler<ApplicationShutdownEventArgs> Stopping;
        event EventHandler<ApplicationShutdownEventArgs> Stopped;
        event EventHandler Activating;
        event EventHandler Activated;
        event EventHandler Deactivating;
        event EventHandler Deactivated;
        event EventHandler<ApplicationStateChangeEventArgs> StateChanging;
        event EventHandler<ApplicationStateChangedEventArgs> StateChanged;
        #endregion

        #region Properties
        ApplicationState CurrentState { get; }

        ApplicationState PreviousState { get; }

        ApplicationShutdownMode ShutdownMode { get; set; }
        #endregion

        #region Methods
        int Start(string[] args);

        void Shutdown(int exitCode = 0);

        void NotifyStarting();

        void NotifyStarted();

        void NotifyStopping();

        void NotifyStopped();

        void NotifyActivating();

        void NotifyActivated();

        void NotifyDeactivating();

        void NotifyDeactivated();
        #endregion
    }
}