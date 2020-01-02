using System;

namespace Shield.Framework.Services.Messaging
{
    public interface IActionMessage: IMessage
    {
        SubscriptionToken Subscribe(Action action);

        SubscriptionToken Subscribe(Action action, ThreadOption threadOption);

        SubscriptionToken Subscribe(Action action, bool keepSubscriberReferenceAlive);

        SubscriptionToken Subscribe(Action action, ThreadOption threadOption, bool keepSubscriberReferenceAlive);

        bool Contains(Action subscriber);
    }

    public interface IActionMessage<TPayload> : IMessage
    {
        SubscriptionToken Subscribe(Action<TPayload> action);

        SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption);

        SubscriptionToken Subscribe(Action<TPayload> action, bool keepSubscriberReferenceAlive);

        SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive);

        void Publish(TPayload payload);

        bool Contains(Action<TPayload> subscriber);
    }
}
