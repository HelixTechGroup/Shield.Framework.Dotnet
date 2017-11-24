using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shield.Framework.Messaging
{
    public interface IMessage
    {
        SynchronizationContext Context { get; set; }
        
        void Unsubscribe(SubscriptionToken token);

        void Publish(params object[] arguments);

        bool Contains(SubscriptionToken token);
    }
}
