#region Usings
#endregion

#region Usings
using Shield.Framework.Services.Threading;
#endregion

namespace Shield.Framework.Platform.Threading
{
    public class PlatformDispatcherProvider : IThreadDispatchSystem
    {
        #region Properties
        public IBackgroundDispatcherService BackgroundDispatcher
        {
            get { return IoCProvider.Container.Resolve<IBackgroundDispatcherService>(); }
        }

        public ISynchronizationContextThreadDispatcherService ContextDispatcher
        {
            get { return IoCProvider.Container.Resolve<ISynchronizationContextThreadDispatcherService>(); }
        }

        public IPlatformUiDispatcher UiDispatcher
        {
            get { return IoCProvider.Container.Resolve<IPlatformUiDispatcher>(); }
        }
        #endregion
    }
}