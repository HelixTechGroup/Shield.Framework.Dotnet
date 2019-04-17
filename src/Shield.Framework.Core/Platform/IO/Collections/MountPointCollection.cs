#region Usings
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Shield.Framework.Platform.IO.Exceptions;
#endregion

namespace Shield.Framework.Platform.IO.Collections
{
    public class MountPointCollection : IMountPointCollection
    {
        #region Events
        public event Action<IDispose> OnDispose;
        #endregion

        #region Members
        private readonly ReaderWriterLockSlim m_lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private IMountPoint[] m_arr;
        private int m_count;
        protected bool m_disposed;
        protected string[] m_protectedMountPoints;
        protected string m_rootDirectory;
        #endregion

        #region Properties
        public int Count
        {
            get
            {
                m_lock.EnterReadLock();
                try
                {
                    return m_count;
                }
                finally
                {
                    m_lock.ExitReadLock();
                }
            }
        }

        public bool Disposed
        {
            get { return m_disposed; }
        }

        public IStorageManager this[string index]
        {
            get
            {
                m_lock.EnterReadLock();
                try
                {
                    Throw.IfNull(index).ArgumentNullException(nameof(index));
                    return m_arr.Single(s => s.Location == index).Storage;
                }
                finally
                {
                    m_lock.ExitReadLock();
                }
            }
        }

        public string[] ProtectedMountPoints
        {
            get { return m_protectedMountPoints; }
        }

        public string RootDirectory
        {
            get { return m_rootDirectory; }
        }
        #endregion

        protected MountPointCollection(string rootDirectory)
        {
            m_rootDirectory = rootDirectory;
            m_arr = new IMountPoint[2];
        }

        protected MountPointCollection()
        {
            m_arr = new IMountPoint[2];
        }

        ~MountPointCollection()
        {
            Dispose(false);
        }

        #region Methods
        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            if (disposing)
            {
                foreach (var s in this)
                    s.Dispose();

                m_lock.Dispose();
            }

            OnDispose?.Invoke(this);
            m_disposed = true;
        }

        public virtual void MountStorage(params KeyValuePair<string, IStorageManager>[] mounts)
        {
            Throw.IfNull(mounts).ArgumentNullException(nameof(mounts));
            foreach (var mount in mounts)
                AddAtInternal(mount.Key, mount.Value);
        }

        public virtual IEnumerable<string> MountStorage(params IStorageManager[] storageManagers)
        {
            Throw.IfNull(storageManagers).ArgumentNullException(nameof(storageManagers));

            var paths = new List<string>();
            foreach (var storageManager in storageManagers)
            {
                var path = "test";
                AddAtInternal(path, storageManager);
                paths.Add(path);
            }

            return paths.AsReadOnly().ToArray();
        }

        public virtual bool UnmountStorage()
        {
            m_lock.EnterWriteLock();
            try
            {
                Array.Clear(m_arr, 0, m_count);
                m_count = 0;
                return true;
            }
            finally
            {
                m_lock.ExitWriteLock();
            }
        }

        public virtual IStorageManager GetStorage(string path)
        {
            m_lock.EnterReadLock();
            try
            {
                Throw.IfNull(path).ArgumentNullException(nameof(path));
                return m_arr.Single(s => s.Location == path).Storage;
            }
            finally
            {
                m_lock.ExitReadLock();
            }
        }

        public virtual IEnumerable<IStorageManager> GetStorage(params string[] paths)
        {
            m_lock.EnterReadLock();
            try
            {
                Throw.IfNull(paths).ArgumentNullException(nameof(paths));
                return m_arr.Where(m => paths.Contains(m.Location)).Select(m => m.Storage).ToList().AsReadOnly();
            }
            finally
            {
                m_lock.ExitReadLock();
            }
        }

        public virtual IEnumerable<string> GetMountPoint<T>(T storageManager) where T : IStorageManager
        {
            m_lock.EnterReadLock();
            try
            {
                Throw.IfNull(storageManager).ArgumentNullException(nameof(storageManager));
                return m_arr.Where(m => m.Storage.GetType() == typeof(T)
                                        && m.Storage.Equals(storageManager))
                            .Select(m => m.Location)
                            .ToList()
                            .AsReadOnly();
            }
            finally
            {
                m_lock.ExitReadLock();
            }
        }

        public virtual IEnumerable<IStorageManager> GetStorage()
        {
            m_lock.EnterReadLock();
            try
            {
                return m_arr.Select(m => m.Storage).ToList().AsReadOnly();
            }
            finally
            {
                m_lock.ExitReadLock();
            }
        }

        public virtual IEnumerable<string> GetMountPoints()
        {
            m_lock.EnterReadLock();
            try
            {
                return m_arr.Select(m => m.Location).ToList().AsReadOnly();
            }
            finally
            {
                m_lock.ExitReadLock();
            }
        }

        public virtual bool ContainsMountPoint(string path)
        {
            m_lock.EnterReadLock();
            try
            {
                return m_arr.Any(m => m.Location == path.ToLower());
            }
            finally
            {
                m_lock.ExitReadLock();
            }
        }

        public virtual bool ContainsStorage<T>(T storageManager) where T : IStorageManager
        {
            m_lock.EnterReadLock();
            try
            {
                return m_arr.Any(m => m.Storage.Equals(storageManager));
            }
            finally
            {
                m_lock.ExitReadLock();
            }
        }

        public virtual void MountStorage<T>(string path, T storageManager) where T : IStorageManager
        {
            AddAtInternal(path, storageManager);
        }

        public virtual string MountStorage<T>(T storageManager) where T : IStorageManager
        {
            var path = "test";
            AddAtInternal(path, storageManager);
            return path;
        }

        public virtual bool UnmountStorage(string path)
        {
            m_lock.EnterUpgradeableReadLock();

            try
            {
                var i = IndexOfInternal(path);

                if (i == -1)
                    return false;

                m_lock.EnterWriteLock();
                try
                {
                    RemoveAtInternal(i);
                    return true;
                }
                finally
                {
                    m_lock.ExitWriteLock();
                }
            }
            finally
            {
                m_lock.ExitUpgradeableReadLock();
            }
        }

        public virtual bool UnmountStorage(params string[] paths)
        {
            m_lock.EnterUpgradeableReadLock();
            var result = false;

            try
            {
                foreach (var path in paths)
                {
                    var i = IndexOfInternal(path);

                    if (i == -1)
                    {
                        result = false;
                        continue;
                    }

                    m_lock.EnterWriteLock();
                    try
                    {
                        RemoveAtInternal(i);
                        result = true;
                    }
                    finally
                    {
                        m_lock.ExitWriteLock();
                    }
                }

                return result;
            }
            finally
            {
                m_lock.ExitUpgradeableReadLock();
            }
        }

        public virtual IEnumerator<IStorageManager> GetEnumerator()
        {
            m_lock.EnterReadLock();

            try
            {
                for (int i = 0; i < m_count; i++)

                    // deadlocking potential mitigated by lock recursion enforcement
                    yield return m_arr[i].Storage;
            }
            finally
            {
                m_lock.ExitReadLock();
            }
        }

        public IEnumerable<T> GetStorage<T>() where T : IStorageManager
        {
            m_lock.EnterReadLock();
            try
            {
                return m_arr.Where(m => m.Storage.GetType() == typeof(T)).Select(m => m.Storage).Cast<T>().ToList().AsReadOnly();
            }
            finally
            {
                m_lock.ExitReadLock();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void EnsureCapacity(int capacity)
        {
            if (m_arr.Length >= capacity)
                return;

            int doubled;
            checked
            {
                try
                {
                    doubled = m_arr.Length * 2;
                }
                catch (OverflowException)
                {
                    doubled = int.MaxValue;
                }
            }

            var newLength = Math.Max(doubled, capacity);
            Array.Resize(ref m_arr, newLength);
        }

        private int IndexOfInternal(IMountPoint item)
        {
            return Array.FindIndex(m_arr, 0, m_count, x => x.Equals(item));
        }

        private int IndexOfInternal(string path)
        {
            return Array.FindIndex(m_arr, 0, m_count, x => x.Location == path.ToLower());
        }

        private int IndexOfInternal(IStorageManager storageManager)
        {
            return Array.FindIndex(m_arr, 0, m_count, x => x.Storage.Equals(storageManager));
        }

        private void AddAtInternal(string mountPoint, IStorageManager storageManager)
        {
            m_lock.EnterUpgradeableReadLock();
            try
            {
                Throw.IfNull(mountPoint).ArgumentNullException(nameof(mountPoint));
                Throw.IfNull(storageManager).ArgumentNullException(nameof(storageManager));
                Throw.If<DuplicateMountPointException>(ContainsMountPoint(mountPoint),
                                                       args: new object[] { mountPoint, storageManager.RootDirectory });
                Throw.If(m_protectedMountPoints.Contains(mountPoint)).InvalidOperationException();

                m_lock.EnterWriteLock();
                try
                {
                    var newCount = m_count + 1;
                    EnsureCapacity(newCount);
                    m_arr[m_count] = new MountPoint(mountPoint, storageManager);
                    m_count = newCount;
                }
                finally
                {
                    m_lock.ExitWriteLock();
                }
            }
            finally
            {
                m_lock.ExitUpgradeableReadLock();
            }
        }

        private void RemoveAtInternal(int index)
        {
            var mount = m_arr[index];
            Throw.If(m_protectedMountPoints.Contains(mount.Location)).InvalidOperationException();

            mount.Dispose();
            Array.Copy(m_arr, index + 1, m_arr, index, m_count - index - 1);
            m_count--;

            // release last element
            Array.Clear(m_arr, m_count, 1);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}