using System;
using System.Collections;
using System.Collections.Generic;
using Shield.Framework.Platform.IO.Default;

namespace Shield.Framework.Platform.IO
{
    public abstract class PlatformStorageProvider : IPlatformStorageProvider
    {
        protected IPlatformStorage m_privateStorage = new DefaultPlatformPrivateStorage();
        protected IPlatformStorage m_localStorage;
        protected IPlatformStorage m_roamingStorage;

        public IPlatformStorage this[string index] => throw new NotImplementedException();

        public IPlatformStorage PrivateApplicationStorage
        {
            get { return m_privateStorage; }
        }

        public IPlatformStorage LocalApplicationStorage
        {
            get { return m_localStorage; }
        }

        public IPlatformStorage RoamingApplicationStorage
        {
            get { return m_roamingStorage; }
        }

        public bool Disposed => throw new NotImplementedException();

        public event Action<IDispose> OnDispose;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IPlatformStorage> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IPlatformStorage GetStorage(string path)
        {
            throw new NotImplementedException();
        }

        public string MountStorage(string path, IPlatformStorage storageManager)
        {
            throw new NotImplementedException();
        }

        public string MountStorage(IPlatformStorage storageManager)
        {
            throw new NotImplementedException();
        }

        public void UnmountStorage(string path)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}