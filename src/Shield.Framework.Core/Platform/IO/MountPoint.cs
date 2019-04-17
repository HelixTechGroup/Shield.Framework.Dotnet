#region Usings
using System;
#endregion

namespace Shield.Framework.Platform.IO
{
    internal sealed class MountPoint : IMountPoint, IEquatable<MountPoint>
    {
        #region Events
        public event Action<IDispose> OnDispose;
        #endregion

        #region Members
        private readonly string m_mountPoint;
        private readonly IStorageManager m_storageManager;
        private bool m_disposed;
        #endregion

        #region Properties
        public bool Disposed
        {
            get { return m_disposed; }
        }

        public string Location
        {
            get { return m_mountPoint; }
        }

        public IStorageManager Storage
        {
            get { return m_storageManager; }
        }
        #endregion

        public MountPoint(string mountPoint, IStorageManager storageManager)
        {
            Throw.IfNull(mountPoint).ArgumentNullException(nameof(mountPoint));
            Throw.IfNull(storageManager).ArgumentNullException(nameof(storageManager));

            m_mountPoint = mountPoint.ToLower();
            m_storageManager = storageManager;
        }

        ~MountPoint()
        {
            Dispose(false);
        }

        #region Methods
        public static bool operator ==(MountPoint left, MountPoint right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MountPoint left, MountPoint right)
        {
            return !Equals(left, right);
        }

        public static bool Equals(IMountPoint left, IMountPoint right)
        {
            return left.Equals(right);
        }

        public static bool Equals(MountPoint left, MountPoint right)
        {
            return left.Equals(right);
        }

        public static implicit operator string(MountPoint argument) => argument.Location;

        public override int GetHashCode()
        {
            unchecked
            {
                return ((m_mountPoint.GetHashCode() + m_storageManager.GetHashCode()) * 397);
            }
        }

        public bool Equals(MountPoint other)
        {
            if (other == null)
                return false;

            return m_mountPoint == other.Location;
        }

        public bool Equals(IMountPoint other)
        {
            if (other == null)
                return false;

            return m_mountPoint == other.Location;
        }

        public override bool Equals(object obj)
        {
            var p = obj as MountPoint;
            return p != null && Equals(p);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            if (disposing)
                m_storageManager.Dispose();

            OnDispose?.Invoke(this);
            m_disposed = true;
        }
        #endregion
    }
}