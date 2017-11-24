using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Shield.Framework.Collections;

namespace Shield.Framework.Messaging.Messages
{
    public abstract class Message : IMessage
    {
        protected SynchronizationContext m_context;
        internal readonly ConcurrentList<ISubscription> m_subscriptions;

        public SynchronizationContext Context
        {
            get { return m_context; }
            set { m_context = value; }
        }

        protected Message()
        {
            m_subscriptions = new ConcurrentList<ISubscription>();
        }

        public virtual void Unsubscribe(SubscriptionToken token)
        {
            var subscription = m_subscriptions.FirstOrDefault(evt => evt.Token == token);
            if (subscription != null)
                m_subscriptions.Remove(subscription);
        }

        public virtual void Publish(params object[] arguments)
        {
            var executionStrategies = PruneAndReturnStrategies();
            foreach (var executionStrategy in executionStrategies)
                executionStrategy(arguments);
        }

        public virtual void Publish()
        {
            Publish(new object[] { });
        }

        public bool Contains(SubscriptionToken token)
        {
            var subscription = m_subscriptions.FirstOrDefault(evt => evt.Token == token);
            return subscription != null;
        }

        protected IEnumerable<Action<object[]>> PruneAndReturnStrategies()
        {
            var returnList = new ConcurrentList<Action<object[]>>();
            for (var i = m_subscriptions.Count - 1; i >= 0; i--)
            {
                var listItem = m_subscriptions[i].GetExecutionStrategy();
                if (listItem == null)
                    m_subscriptions.RemoveAt(i);
                else
                    returnList.Add(listItem);
            }

            return returnList;
        }

        internal SubscriptionToken Subscribe(ISubscription subscription)
        {
            if (subscription == null)
                throw new ArgumentNullException(nameof(subscription));

            subscription.Token = new SubscriptionToken(Unsubscribe);
            m_subscriptions.Add(subscription);

            return subscription.Token;
        }
    }    
}