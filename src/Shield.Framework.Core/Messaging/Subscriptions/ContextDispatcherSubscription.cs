using System;
using System.Threading;
using Shield.Framework.Platform;

namespace Shield.Framework.Messaging.Subscriptions
{
    internal sealed class ContextDispatcherSubscription : Subscription
    {
        private readonly SynchronizationContext m_context;

        public ContextDispatcherSubscription(IDelegateReference actionReference, SynchronizationContext context)
            : base(actionReference)
        {
            m_context = context;
        }

        protected override void InvokeAction(Action action)
        {
            ((IPlatformContextDispatcher)PlatformProvider.ContextDispatcher).SetContext(m_context);
            PlatformProvider.ContextDispatcher.Run(action);
            //m_context.Post((o) => action(), null);
        }
    }

    internal sealed class ContextDispatcherSubscription<TPayload> : Subscription<TPayload>
    {
        private readonly SynchronizationContext m_context;

        public ContextDispatcherSubscription(IDelegateReference actionReference, IDelegateReference filterReference, SynchronizationContext context) :
            base(actionReference, filterReference)
        {
            m_context = context;
        }

        protected override void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            ((IPlatformContextDispatcher)PlatformProvider.ContextDispatcher).SetContext(m_context);
            PlatformProvider.ContextDispatcher.Run(action, argument);
           //m_context.Post((o) => action((TPayload)o), argument);
        }
    }
}