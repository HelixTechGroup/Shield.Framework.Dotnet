#region Usings
using System;
using Shield.Framework.Extensions;
#endregion

namespace Shield.Framework.Messaging.Subscriptions
{
    internal sealed class BackgroundDispatcherSubscription : Subscription
    {
        public BackgroundDispatcherSubscription(IDelegateReference actionReference) :
            base(actionReference) { }

        protected override void InvokeAction(Action action)
        {
            action.OnNewThread();
        }
    }

    internal sealed class BackgroundDispatcherSubscription<TPayload> : Subscription<TPayload>
    {
        public BackgroundDispatcherSubscription(IDelegateReference actionReference,
                                                IDelegateReference filterReference) :
            base(actionReference, filterReference) { }

        protected override void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            action.OnNewThread(argument);
        }
    }
}