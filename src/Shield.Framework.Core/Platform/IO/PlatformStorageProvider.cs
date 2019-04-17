#region Usings
using Shield.Framework.Platform.IO.Collections;
using Shield.Framework.Platform.IO.FileSystems;
#endregion

namespace Shield.Framework.Platform.IO
{
    public class PlatformStorageProvider : MountPointCollection, IPlatformStorageProvider
    {
        #region Members
        protected readonly IStorageManager<ILocalApplicationFileSystem> m_localStorage;
        protected readonly IStorageManager<IPrivateApplicationFileSystem> m_privateStorage;
        protected readonly IStorageManager m_roamingStorage;
        #endregion

        #region Properties
        public IStorageManager<ILocalApplicationFileSystem> Local
        {
            get { return m_localStorage; }
        }

        public IStorageManager<IPrivateApplicationFileSystem> Private
        {
            get { return m_privateStorage; }
        }

        public IStorageManager Roaming
        {
            get { return m_roamingStorage; }
        }
        #endregion

        protected PlatformStorageProvider(ILocalApplicationFileSystem localStorage,
                                          IPrivateApplicationFileSystem privateStorage) :
            base(StoragePath.RootPath)
        {
            m_localStorage = new StorageManager<ILocalApplicationFileSystem>(localStorage);
            m_privateStorage = new StorageManager<IPrivateApplicationFileSystem>(privateStorage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_localStorage.Dispose();
                m_privateStorage.Dispose();
                m_roamingStorage.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}