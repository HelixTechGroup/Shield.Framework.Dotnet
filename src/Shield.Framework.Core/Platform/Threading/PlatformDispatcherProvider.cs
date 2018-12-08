using Shield.Framework.Platform.Threading.Default;

namespace Shield.Framework.Platform.Threading
{
	public abstract class PlatformDispatcherProvider : IPlatformDispatcherProvider
    {
		protected IPlatformDispatcher m_uiDispatcher = new DefaultPlatformUIDispatcher();
		protected IPlatformDispatcher m_backgroundDispatcher = new DefaultPlatformBackgroundDispatcher();
		protected IPlatformContextDispatcher m_contextDispatcher = new DefaultPlatformContextDispatcher();

		public IPlatformDispatcher UIDispatcher
		{
			get { return m_uiDispatcher; }
		}

		public IPlatformDispatcher BackgroundDispatcher
		{
			get { return m_backgroundDispatcher; }
		}

		public IPlatformContextDispatcher ContextDispatcher
		{
			get { return m_contextDispatcher; }
		}
	}
}