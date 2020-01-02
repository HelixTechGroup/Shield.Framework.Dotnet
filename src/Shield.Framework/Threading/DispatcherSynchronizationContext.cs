// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

#region Usings
using System.Threading;
#endregion

namespace Shield.Framework.Threading
{
    /// <summary>
    /// SynchronizationContext to be used on main thread
    /// </summary>
    public class DispatcherSynchronizationContext : SynchronizationContext
    {
        #region Properties
        /// <summary>
        /// Controls if SynchronizationContext should be installed in InstallIfNeeded. Used by Designer.
        /// </summary>
        public static bool AutoInstall { get; set; } = true;
        #endregion

        #region Methods
        /// <summary>
        /// Installs synchronization context in current thread
        /// </summary>
        public static void InstallIfNeeded()
        {
            if (!AutoInstall || Current is DispatcherSynchronizationContext) return;

            SetSynchronizationContext(new DispatcherSynchronizationContext());
        }

        /// <inheritdoc/>
        public override void Post(SendOrPostCallback d, object state)
        {
            Dispatcher.CurrentDispatcher.Post(() => d(state), DispatcherPriority.Send);
        }

        /// <inheritdoc/>
        public override void Send(SendOrPostCallback d, object state)
        {
            if (Dispatcher.CurrentDispatcher.CheckAccess())
                d(state);
            else
                Dispatcher.CurrentDispatcher.InvokeAsync(() => d(state), DispatcherPriority.Send).Wait();
        }
        #endregion
    }
}