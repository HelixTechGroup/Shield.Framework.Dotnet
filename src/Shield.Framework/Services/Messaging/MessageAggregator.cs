#region Usings
using System;
using System.Collections.Concurrent;
using System.Threading;
#endregion

namespace Shield.Framework.Services.Messaging
{
    public sealed class MessageAggregator : IMessageAggregatorService
    {
        #region Events
        /// <inheritdoc />
        public event EventHandler Disposing;

        /// <inheritdoc />
        public event EventHandler Disposed;

        /// <inheritdoc />
        public event EventHandler Initializing;

        /// <inheritdoc />
        public event EventHandler Initialized;
        #endregion

        #region Members
        private readonly SynchronizationContext m_context;
        private readonly ConcurrentDictionary<Type, IMessage> m_messages;
        private bool m_isDisposed;
        private bool m_isInitialized;
        #endregion

        #region Properties
        /// <inheritdoc />
        public bool IsDisposed
        {
            get { return m_isDisposed; }
        }

        /// <inheritdoc />
        public bool IsInitialized
        {
            get { return m_isInitialized; }
        }
        #endregion

        public MessageAggregator()
        {
            m_context = SynchronizationContext.Current;
            m_messages = new ConcurrentDictionary<Type, IMessage>();
        }

        #region Methods
        public bool MessageExists<T>() where T : IMessage, new()
        {
            return m_messages.ContainsKey(typeof(T));
        }

        public T GetMessage<T>() where T : IMessage, new()
        {
            IMessage message;
            if (m_messages.TryGetValue(typeof(T), out message))
                return (T)message;

            var newMessage = new T();
            newMessage.Context = m_context;
            m_messages[typeof(T)] = newMessage;

            return newMessage;
        }

        public void RemoveMessage<T>() where T : IMessage, new()
        {
            if (!MessageExists<T>())
                return;

            IMessage message;
            m_messages.TryRemove(typeof(T), out message);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Initialize()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}