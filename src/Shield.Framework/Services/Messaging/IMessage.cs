using System.Threading;

namespace Shield.Framework.Services.Messaging
{
    public interface IMessage
    {
        SynchronizationContext Context { get; set; }
        
        void Unsubscribe(SubscriptionToken token);

        void Publish(params object[] arguments);

        bool Contains(SubscriptionToken token);
    }
}
