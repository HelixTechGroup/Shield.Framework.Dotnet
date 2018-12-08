namespace Shield.Framework.Platform.Threading
{
    public interface IPlatformDispatcherProvider
    {
        IPlatformDispatcher BackgroundDispatcher { get; }

        IPlatformContextDispatcher ContextDispatcher { get; }

        IPlatformDispatcher UIDispatcher { get; }
    }
}