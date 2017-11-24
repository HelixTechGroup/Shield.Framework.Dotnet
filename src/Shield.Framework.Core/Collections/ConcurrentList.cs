#region Header

// // ----------------------------------------------------------------------
// // filename: ConcurrentList.cs
// // company: EmpireGaming, LLC
// // date: 05-10-2017
// // namespace: UniverseSol.Framework.System.Collections
// // class: ConcurrentList<T> : IList<T>, IDisposable
// // summary: Class representing a ConcurrentList<T> : IList<T>, IDisposable entity.
// // legal: Copyright (c) 2017 All Right Reserved
// // ------------------------------------------------------------------------
// 
#endregion

#region Usings
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
#endregion

namespace Shield.Framework.Collections
{
    public class ConcurrentList<T> : IList<T>, IDispose
    {
        public event Action<IDispose> OnDispose;

        #region Members
        private readonly ReaderWriterLockSlim m_lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private T[] m_arr;
        private int m_count;
        private bool m_disposed;
        #endregion

        #region Properties
        public virtual int Count
        {
            get
            {
                m_lock.EnterReadLock();
                try { return m_count; }
                finally { m_lock.ExitReadLock(); }
            }
        }

        public virtual int InternalArrayLength
        {
            get
            {
                m_lock.EnterReadLock();
                try { return m_arr.Length; }
                finally { m_lock.ExitReadLock(); }
            }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual T this[int index]
        {
            get
            {
                m_lock.EnterReadLock();
                try
                {
                    if (index >= m_count)
                        throw new ArgumentOutOfRangeException(nameof(index));

                    return m_arr[index];
                }
                finally { m_lock.ExitReadLock(); }
            }
            set
            {
                m_lock.EnterUpgradeableReadLock();
                try
                {
                    if (index >= m_count)
                        throw new ArgumentOutOfRangeException(nameof(index));

                    m_lock.EnterWriteLock();
                    try { m_arr[index] = value; }
                    finally { m_lock.ExitWriteLock(); }
                }
                finally { m_lock.ExitUpgradeableReadLock(); }
            }
        }

        public bool Disposed
        {
            get { return m_disposed; }
        }
        #endregion

        ~ConcurrentList()
        {
            Dispose(false);    
        }

        #region Methods
        public virtual void Add(T item)
        {
            m_lock.EnterWriteLock();
            try
            {
                var newCount = m_count + 1;
                EnsureCapacity(newCount);
                m_arr[m_count] = item;
                m_count = newCount;
            }
            finally { m_lock.ExitWriteLock(); }
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            m_lock.EnterWriteLock();

            try
            {
                var arr = items as T[] ?? items.ToArray();
                var newCount = m_count + arr.Length;
                EnsureCapacity(newCount);
                Array.Copy(arr, 0, m_arr, m_count, arr.Length);
                m_count = newCount;
            }
            finally { m_lock.ExitWriteLock(); }
        }

        public virtual void Clear()
        {
            m_lock.EnterWriteLock();
            try
            {
                Array.Clear(m_arr, 0, m_count);
                m_count = 0;
            }
            finally { m_lock.ExitWriteLock(); }
        }

        public virtual bool Contains(T item)
        {
            m_lock.EnterReadLock();
            try { return IndexOfInternal(item) != -1; }
            finally { m_lock.ExitReadLock(); }
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            m_lock.EnterReadLock();
            try
            {
                if (m_count > array.Length - arrayIndex)
                    throw new ArgumentException("Destination array was not long enough.");

                Array.Copy(m_arr, 0, array, arrayIndex, m_count);
            }
            finally { m_lock.ExitReadLock(); }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            m_lock.Dispose();

            if (OnDispose != null)
                OnDispose(this);
            m_disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void DoSync(Action<ConcurrentList<T>> action)
        {
            GetSync(l =>
                    {
                        action(l);
                        return 0;
                    });
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            m_lock.EnterReadLock();

            try
            {
                for (int i = 0; i < m_count; i++)

                    // deadlocking potential mitigated by lock recursion enforcement
                    yield return m_arr[i];
            }
            finally { m_lock.ExitReadLock(); }
        }

        public virtual TResult GetSync<TResult>(Func<ConcurrentList<T>, TResult> func)
        {
            m_lock.EnterWriteLock();
            try { return func(this); }
            finally { m_lock.ExitWriteLock(); }
        }

        public virtual int IndexOf(T item)
        {
            m_lock.EnterReadLock();
            try { return IndexOfInternal(item); }
            finally { m_lock.ExitReadLock(); }
        }

        public virtual void Insert(int index, T item)
        {
            m_lock.EnterUpgradeableReadLock();

            try
            {
                if (index > m_count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                m_lock.EnterWriteLock();
                try
                {
                    var newCount = m_count + 1;
                    EnsureCapacity(newCount);

                    // shift everything right by one, starting at index
                    Array.Copy(m_arr, index, m_arr, index + 1, m_count - index);

                    // insert
                    m_arr[index] = item;
                    m_count = newCount;
                }
                finally { m_lock.ExitWriteLock(); }
            }
            finally { m_lock.ExitUpgradeableReadLock(); }
        }

        public virtual bool Remove(T item)
        {
            m_lock.EnterUpgradeableReadLock();

            try
            {
                var i = IndexOfInternal(item);

                if (i == -1)
                    return false;

                m_lock.EnterWriteLock();
                try
                {
                    RemoveAtInternal(i);
                    return true;
                }
                finally { m_lock.ExitWriteLock(); }
            }
            finally { m_lock.ExitUpgradeableReadLock(); }
        }

        public virtual void RemoveAt(int index)
        {
            m_lock.EnterUpgradeableReadLock();
            try
            {
                if (index >= m_count)
                    throw new ArgumentOutOfRangeException("index");

                m_lock.EnterWriteLock();
                try { RemoveAtInternal(index); }
                finally { m_lock.ExitWriteLock(); }
            }
            finally { m_lock.ExitUpgradeableReadLock(); }
        }

        protected virtual void EnsureCapacity(int capacity)
        {
            if (m_arr.Length >= capacity)
                return;

            int doubled;
            checked
            {
                try { doubled = m_arr.Length * 2; }
                catch (OverflowException) {
                    doubled = int.MaxValue;
                }
            }

            var newLength = Math.Max(doubled, capacity);
            Array.Resize(ref m_arr, newLength);
        }

        protected virtual int IndexOfInternal(T item)
        {
            return Array.FindIndex(m_arr, 0, m_count, x => x.Equals(item));
        }

        protected virtual void RemoveAtInternal(int index)
        {
            Array.Copy(m_arr, index + 1, m_arr, index, m_count - index - 1);
            m_count--;

            // release last element
            Array.Clear(m_arr, m_count, 1);
        }
        #endregion

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region Constructors
        public ConcurrentList(int initialCapacity)
        {
            m_arr = new T[initialCapacity];
        }

        public ConcurrentList() : this(4) {}

        public ConcurrentList(IEnumerable<T> items)
        {
            m_arr = items.ToArray();
            m_count = m_arr.Length;
        }
        #endregion
    }
}