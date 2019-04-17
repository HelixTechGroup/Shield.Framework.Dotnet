#region Usings
using Shield.Framework.Platform.IO.FileSystems;
#endregion

namespace Shield.Framework.Platform.IO
{
    public sealed class MonoGameStorageProvider : PlatformStorageProvider, IMonoGameStorageProvider
    {
        #region Properties
        public IStorageManager Title
        {
            get { return Local; }
        }
        #endregion

        public MonoGameStorageProvider(ILocalApplicationFileSystem localStorage, IPrivateApplicationFileSystem privateStorage) : base(
            localStorage,
            privateStorage) { }
    }
}