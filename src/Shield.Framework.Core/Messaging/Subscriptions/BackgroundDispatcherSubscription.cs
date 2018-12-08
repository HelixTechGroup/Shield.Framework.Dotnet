using System;
using Shield.Framework.Platform;

namespace Shield.Framework.Messaging.Subscriptions
{
    internal sealed class BackgroundDispatcherSubscription : Subscription
    {
        public BackgroundDispatcherSubscription(IDelegateReference actionReference) :
            base(actionReference) { }

        protected override void InvokeAction(Action action)
        {
            PlatformProvider.Services.Dispatcher.BackgroundDispatcher.Run(action);
        }
    }

    internal sealed class BackgroundDispatcherSubscription<TPayload> : Subscription<TPayload>
    {
        public BackgroundDispatcherSubscription(IDelegateReference actionReference, IDelegateReference filterReference) :
            base(actionReference, filterReference) { }

        protected override void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            PlatformProvider.Services.Dispatcher.BackgroundDispatcher.Run(action, argument);
        }        
    }
}