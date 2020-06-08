// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

#region Usings
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Patchwork.Framework;
using Patchwork.Framework.Platform;
#endregion

namespace Shield.Framework.Threading
{
    /// <summary>
    /// Provides services for managing work items on a thread.
    /// </summary>
    /// <remarks>
    /// In Avalonia, there is usually only a single <see cref="Dispatcher"/> in the application -
    /// the one for the UI thread, retrieved via the <see cref="MainThread"/> property.
    /// </remarks>
    public class Dispatcher : IDispatcher
    {
        #region Members
        private static readonly object olock = new object();
        private static readonly Dictionary<Thread, Dispatcher> dispatchers = new Dictionary<Thread, Dispatcher>();
        private readonly JobRunner _jobRunner;
        private INativeThreadDispatcher _platform;
        #endregion

        #region Properties
        public static Dispatcher CurrentDispatcher
        {
            get
            {
                lock(olock)
                {
                    var platform = PlatformManager.Dispatcher;
                    var t = platform.Thread;
                    var dis = FromThread(t);

                    if (dis != null)
                        return dis;

                    dis = new Dispatcher(platform);
                    dispatchers[t] = dis;
                    return dis;
                }
            }
        }

        public Thread Thread
        {
            get { return _platform.Thread; }
        }
        #endregion

        public Dispatcher(INativeThreadDispatcher platform)
        {
            _platform = platform;
            _jobRunner = new JobRunner(platform);

            if (_platform != null) _platform.Signaled += _jobRunner.RunJobs;
        }

        #region Methods
        public static Dispatcher FromThread(Thread thread)
        {
            lock(olock)
            {
                return dispatchers.TryGetValue(thread, out var dis) ? dis : null;
            }
        }

        /// <summary>
        /// Checks that the current thread is the UI thread.
        /// </summary>
        public bool CheckAccess()
        {
            return _platform?.CurrentThreadIsLoopThread ?? true;
        }

        /// <summary>
        /// Checks that the current thread is the UI thread and throws if not.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The current thread is not the UI thread.
        /// </exception>
        public void VerifyAccess()
        {
            if (!CheckAccess())
                throw new InvalidOperationException("Call from invalid thread");
        }

        /// <summary>
        /// Runs the dispatcher's main loop.
        /// </summary>
        /// <param name="cancellationToken">
        /// A cancellation token used to exit the main loop.
        /// </param>
        public void MainLoop(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => _platform.Signal(NativeThreadDispatcherPriority.Normal));
            //_platform.(cancellationToken);
        }

        /// <summary>
        /// Runs continuations pushed on the loop.
        /// </summary>
        public void RunJobs()
        {
            _jobRunner.RunJobs(null);
        }

        /// <summary>
        /// Use this method to ensure that more prioritized tasks are executed
        /// </summary>
        /// <param name="minimumPriority"></param>
        public void RunJobs(NativeThreadDispatcherPriority minimumPriority)
        {
            _jobRunner.RunJobs(minimumPriority);
        }

        /// <inheritdoc/>
        public Task InvokeAsync(Action action, NativeThreadDispatcherPriority priority = NativeThreadDispatcherPriority.Normal)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            return _jobRunner.InvokeAsync(action, priority);
        }

        /// <inheritdoc/>
        public Task<TResult> InvokeAsync<TResult>(Func<TResult> function, NativeThreadDispatcherPriority priority = NativeThreadDispatcherPriority.Normal)
        {
            Contract.Requires<ArgumentNullException>(function != null);
            return _jobRunner.InvokeAsync(function, priority);
        }

        /// <inheritdoc/>
        public Task InvokeAsync(Func<Task> function, NativeThreadDispatcherPriority priority = NativeThreadDispatcherPriority.Normal)
        {
            Contract.Requires<ArgumentNullException>(function != null);
            return _jobRunner.InvokeAsync(function, priority).Unwrap();
        }

        /// <inheritdoc/>
        public Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> function, NativeThreadDispatcherPriority priority = NativeThreadDispatcherPriority.Normal)
        {
            Contract.Requires<ArgumentNullException>(function != null);
            return _jobRunner.InvokeAsync(function, priority).Unwrap();
        }

        /// <inheritdoc/>
        public void Post(Action action, NativeThreadDispatcherPriority priority = NativeThreadDispatcherPriority.Normal)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            _jobRunner.Post(action, priority);
        }

        /// <summary>
        /// This is needed for platform backends that don't have internal priority system (e. g. win32)
        /// To ensure that there are no jobs with higher priority
        /// </summary>
        /// <param name="currentPriority"></param>
        internal void EnsurePriority(NativeThreadDispatcherPriority currentPriority)
        {
            if (currentPriority == NativeThreadDispatcherPriority.MaxValue)
                return;
            currentPriority += 1;
            _jobRunner.RunJobs(currentPriority);
        }

        /// <summary>
        /// Allows unit tests to change the platform threading interface.
        /// </summary>
        internal void UpdateServices()
        {
            if (_platform != null) _platform.Signaled -= _jobRunner.RunJobs;

            _platform = Shield.CurrentApplication.Container.Resolve<INativeThreadDispatcher>();
            _jobRunner.UpdateServices();

            if (_platform != null) _platform.Signaled += _jobRunner.RunJobs;
        }
        #endregion
    }
}