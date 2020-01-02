using System;

namespace Shield.Framework.Services.Messaging
{
    /// <summary>   Interface for delegate reference. </summary>
    internal interface IDelegateReference
    {
        Delegate Target { get; }
    }
}