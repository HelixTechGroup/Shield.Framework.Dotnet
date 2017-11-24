using System;
using System.Reflection;

namespace Shield.Framework.Messaging
{
    internal sealed class DelegateReference : IDelegateReference
    {
        private readonly Delegate m_target;
        private readonly WeakReference m_weakReference;
        private readonly MethodInfo m_method;
        private readonly Type m_delegateType;

        public Delegate Target
        {
            get { return m_target ?? TryGetDelegate(); }
        }

        public DelegateReference(Delegate @delegate, bool keepReferenceAlive)
        {
            if (@delegate == null)
                throw new ArgumentNullException(nameof(@delegate));

            if (keepReferenceAlive)
                m_target = @delegate;
            else
            {
                m_weakReference = new WeakReference(@delegate.Target);
                m_method = @delegate.GetMethodInfo();
                m_delegateType = @delegate.GetType();
            }
        }

        private Delegate TryGetDelegate()
        {
            if (m_method.IsStatic)
                return m_method.CreateDelegate(m_delegateType, null);

            var target = m_weakReference.Target;
            return target != null ? m_method.CreateDelegate(m_delegateType, target) : null;
        }
    }
}