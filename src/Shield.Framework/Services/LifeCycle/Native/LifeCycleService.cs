#region Usings
using System;
using System.Threading;
using Shield.Framework.Platform;
using Shield.Framework.Threading;
using SysEnv = System.Environment;
#endregion

namespace Shield.Framework.Services.LifeCycle.Native
{
    public class LifeCycleService : ApplicationService, ILifeCycleService
    {
        #region Events
        /// <inheritdoc />
        public event EventHandler<ApplicationStartupEventArgs> Starting;

        /// <inheritdoc />
        public event EventHandler<ApplicationStartupEventArgs> Started;

        /// <inheritdoc />
        public event EventHandler<ApplicationShutdownEventArgs> Stopping;

        /// <inheritdoc />
        public event EventHandler<ApplicationShutdownEventArgs> Stopped;

        /// <inheritdoc />
        public event EventHandler Activating;

        /// <inheritdoc />
        public event EventHandler Activated;

        /// <inheritdoc />
        public event EventHandler Deactivating;

        /// <inheritdoc />
        public event EventHandler Deactivated;

        /// <inheritdoc />
        public event EventHandler<ApplicationStateChangeEventArgs> StateChanging;

        /// <inheritdoc />
        public event EventHandler<ApplicationStateChangedEventArgs> StateChanged;
        #endregion

        #region Members
        private CancellationTokenSource _cts;
        private int _exitCode;
        private bool _isShuttingDown;
        private ApplicationState m_currentState;
        private ApplicationState m_previousState;
        private ApplicationShutdownMode m_shutdownMode;
        #endregion

        #region Properties
        /// <inheritdoc />
        public ApplicationState CurrentState
        {
            get { return m_currentState; }
        }

        /// <inheritdoc />
        public ApplicationState PreviousState
        {
            get { return m_previousState; }
        }

        /// <inheritdoc />
        public ApplicationShutdownMode ShutdownMode
        {
            get { return m_shutdownMode; }
            set { m_shutdownMode = value; }
        }
        #endregion

        #region Methods
        /// <inheritdoc />
        public int Start(string[] args)
        {
            Starting?.Invoke(this, new ApplicationStartupEventArgs());
            _cts = new CancellationTokenSource();
            //MainWindow?.Show();
            PlatformManager.CurrentPlatform.Application.CreateWindow().Show();
            Dispatcher.CurrentDispatcher.MainLoop(_cts.Token);
            SysEnv.ExitCode = _exitCode;
            return _exitCode;
        }

        /// <inheritdoc />
        public void Shutdown(int exitCode = 0)
        {
            if (_isShuttingDown)
                throw new InvalidOperationException("Application is already shutting down.");

            _exitCode = exitCode;
            _isShuttingDown = true;

            try
            {
                //foreach (var w in Windows)
                //    w.Close();
                var e = new ApplicationShutdownEventArgs();
                Stopping?.Invoke(this, e);
                //_exitCode = e.ApplicationExitCode;
            }
            finally
            {
                _cts?.Cancel();
                _cts = null;
                _isShuttingDown = false;
            }
        }

        /// <inheritdoc />
        public void NotifyStarting()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void NotifyStarted()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void NotifyStopping()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void NotifyStopped()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void NotifyActivating()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void NotifyActivated()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void NotifyDeactivating()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void NotifyDeactivated()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}