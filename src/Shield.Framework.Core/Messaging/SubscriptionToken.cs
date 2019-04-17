using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shield.Framework.Messaging
{
    public sealed class SubscriptionToken : IEquatable<SubscriptionToken>, IDispose
    {
        private readonly Guid m_token;
        private Action<SubscriptionToken> m_unsubscribeAction;
        private bool m_disposed;

        public event Action<IDispose> OnDispose;

        public bool Disposed
        {
            get { return m_disposed; }
        }

        public SubscriptionToken(Action<SubscriptionToken> unsubscribeAction)
        {
            m_unsubscribeAction = unsubscribeAction;
            m_token = Guid.NewGuid();
        }

        ~SubscriptionToken()
        {
            Dispose(false);
        }

        public static bool operator ==(SubscriptionToken left, SubscriptionToken right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SubscriptionToken left, SubscriptionToken right)
        {
            return !Equals(left, right);
        }

        public bool Equals(SubscriptionToken other)
        {
            return other != null && Equals(m_token, other.m_token);
        }
        
        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || Equals(obj as SubscriptionToken);
        }

        public override int GetHashCode()
        {
            return m_token.GetHashCode();
        }

        private void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            if (disposing) { }

            if (m_unsubscribeAction != null)
                {
                    m_unsubscribeAction(this);
                    m_unsubscribeAction = null;
                }

            OnDispose?.Invoke(this);
            m_disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }        
    }
}
