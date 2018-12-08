namespace Shield.Framework.Platform.Threading
{
    public class WindowsDispatcherProvider : PlatformDispatcherProvider
    {
        public WindowsDispatcherProvider()
        {
            m_uiDispatcher = new WindowsPlatformUIDispatcher();
        }
    }
}