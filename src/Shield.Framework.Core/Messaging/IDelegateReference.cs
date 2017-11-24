using System;

namespace Shield.Framework.Messaging
{
    /// <summary>   Interface for delegate reference. </summary>
    internal interface IDelegateReference
    {
        Delegate Target { get; }
    }
}