#region Usings
using System;
using System.Threading;
using Shield.Framework.Threading;
#endregion

namespace Shield.Framework.Platform
{
    public interface INativeThreadDispatcher
    {
        #region Events
        event Action<DispatcherPriority?> Signaled;
        #endregion

        #region Properties
        bool CurrentThreadIsLoopThread { get; }
        Thread Thread { get; }
        #endregion

        #region Methods
        void RunLoop(CancellationToken cancellationToken);

        /// <summary>
        /// Starts a timer.
        /// </summary>
        /// <param name="priority"></param>
        /// <param name="interval">The interval.</param>
        /// <param name="tick">The action to call on each tick.</param>
        /// <returns>An <see cref="IDisposable"/> used to stop the timer.</returns>
        IDisposable StartTimer(DispatcherPriority priority, TimeSpan interval, Action tick);

        void Signal(DispatcherPriority priority);
        #endregion
    }
}