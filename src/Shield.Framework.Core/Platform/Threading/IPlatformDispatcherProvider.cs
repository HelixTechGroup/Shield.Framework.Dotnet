namespace Shield.Framework.Platform.Threading
{
    public interface IPlatformDispatcherProvider
    {
        #region Properties
        IPlatformBackgroundDispatcher BackgroundDispatcher { get; }

        IPlatformContextDispatcher ContextDispatcher { get; }

        IPlatformUiDispatcher UiDispatcher { get; }
        #endregion
    }
}