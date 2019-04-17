#region Usings
#endregion

namespace Shield.Framework.Platform.Threading
{
    public abstract class PlatformDispatcherProvider : IPlatformDispatcherProvider
    {
        #region Properties
        public IPlatformBackgroundDispatcher BackgroundDispatcher
        {
            get { return IoCProvider.Container.Resolve<IPlatformBackgroundDispatcher>(); }
        }

        public IPlatformContextDispatcher ContextDispatcher
        {
            get { return IoCProvider.Container.Resolve<IPlatformContextDispatcher>(); }
        }

        public IPlatformUiDispatcher UiDispatcher
        {
            get { return IoCProvider.Container.Resolve<IPlatformUiDispatcher>(); }
        }
        #endregion
    }
}