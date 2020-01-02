using System;

namespace Shield.Framework.Services.Messaging
{
    internal interface ISubscription
    {
        SubscriptionToken Token { get; set; }

        Delegate Action { get; }

        Action<object[]> GetExecutionStrategy();
    }
}