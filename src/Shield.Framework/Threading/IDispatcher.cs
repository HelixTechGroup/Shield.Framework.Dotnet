#region Usings
using System;
using System.Threading.Tasks;
using Patchwork.Framework.Platform;
#endregion

namespace Shield.Framework.Threading
{
    /// <summary>
    /// Dispatches jobs to a thread.
    /// </summary>
    public interface IDispatcher
    {
        #region Methods
        /// <summary>
        /// Determines whether the calling thread is the thread associated with this <see cref="IDispatcher"/>.
        /// </summary>
        /// <returns>True if he calling thread is the thread associated with the dispatched, otherwise false.</returns>
        bool CheckAccess();

        /// <summary>
        /// Throws an exception if the calling thread is not the thread associated with this <see cref="IDispatcher"/>.
        /// </summary>
        void VerifyAccess();

        /// <summary>
        /// Invokes a method on the dispatcher thread.
        /// </summary>
        /// <param name="action">The method.</param>
        /// <param name="priority">The priority with which to invoke the method.</param>
        /// <returns>A task that can be used to track the method's execution.</returns>
        void Post(Action action, NativeThreadDispatcherPriority priority = NativeThreadDispatcherPriority.Normal);

        /// <summary>
        /// Posts an action that will be invoked on the dispatcher thread.
        /// </summary>
        /// <param name="action">The method.</param>
        /// <param name="priority">The priority with which to invoke the method.</param>
        Task InvokeAsync(Action action, NativeThreadDispatcherPriority priority = NativeThreadDispatcherPriority.Normal);

        /// <summary>
        /// Posts a function that will be invoked on the dispatcher thread.
        /// </summary>
        /// <param name="function">The method.</param>
        /// <param name="priority">The priority with which to invoke the method.</param>
        Task<TResult> InvokeAsync<TResult>(Func<TResult> function, NativeThreadDispatcherPriority priority = NativeThreadDispatcherPriority.Normal);

        /// <summary>
        /// Queues the specified work to run on the dispatcher thread and returns a proxy for the
        /// task returned by <paramref name="function"/>.
        /// </summary>
        /// <param name="function">The work to execute asynchronously.</param>
        /// <param name="priority">The priority with which to invoke the method.</param>
        /// <returns>A task that represents a proxy for the task returned by <paramref name="function"/>.</returns>
        Task InvokeAsync(Func<Task> function, NativeThreadDispatcherPriority priority = NativeThreadDispatcherPriority.Normal);

        /// <summary>
        /// Queues the specified work to run on the dispatcher thread and returns a proxy for the
        /// task returned by <paramref name="function"/>.
        /// </summary>
        /// <param name="function">The work to execute asynchronously.</param>
        /// <param name="priority">The priority with which to invoke the method.</param>
        /// <returns>A task that represents a proxy for the task returned by <paramref name="function"/>.</returns>
        Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> function, NativeThreadDispatcherPriority priority = NativeThreadDispatcherPriority.Normal);
        #endregion
    }
}