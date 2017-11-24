using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Shield.Framework.Messaging.Messages;

namespace Shield.Framework.Messaging
{
    public sealed class MessageAggregator : IMessageAggregator
    {
        private readonly SynchronizationContext m_context;
        private readonly ConcurrentDictionary<Type, IMessage> m_messages;

        public MessageAggregator()
        {
            m_context = SynchronizationContext.Current;  
            m_messages = new ConcurrentDictionary<Type, IMessage>();
        }

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
    }
}