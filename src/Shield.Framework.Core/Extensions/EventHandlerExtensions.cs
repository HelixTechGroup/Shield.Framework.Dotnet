#region Usings
using System;
using System.ComponentModel;
using System.Threading;
#endregion

namespace Shield.Framework.Extensions
{
    public static class EventHandlerExtensions
    {
        #region Methods
        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs args)
            where TEventArgs : EventArgs
        {
            var tmp = Interlocked.CompareExchange(ref handler, null, null);
            if (tmp != null)
                tmp(sender, args);
        }

        public static void Raise(this EventHandler handler, object sender, EventArgs args)
        {
            var tmp = Interlocked.CompareExchange(ref handler, null, null);
            if (tmp != null)
                tmp(sender, args);
        }

        public static void Raise(this PropertyChangedEventHandler handler, object sender, PropertyChangedEventArgs args)
        {
            var tmp = Interlocked.CompareExchange(ref handler, null, null);
            if (tmp != null)
                tmp(sender, args);
        }

        public static void Dispose<TEventArgs>(this EventHandler<TEventArgs> handler)
        {
            if (handler != null)
            {
                foreach (var d in handler.GetInvocationList())
                    handler -= d as EventHandler<TEventArgs>;
            }
        }

        public static void Dispose(this EventHandler handler)
        {
            if (handler != null)
            {
                foreach (var d in handler.GetInvocationList())
                    handler -= d as EventHandler;
            }
        }
        #endregion
    }
}