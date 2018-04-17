using Shield.Framework.IoC;

namespace Shield.Framework
{
	public static class IoCProvider
	{
		private static IIoCContainer m_container;

		public static IIoCContainer Container
		{
			get { return m_container; }
			internal set { m_container = value; }
		}
	}
}