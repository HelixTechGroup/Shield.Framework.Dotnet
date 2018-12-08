using Shield.Framework.Platform;

namespace Shield.Framework
{
    public static class PlatformProvider
    {
	    private static IPlatformServices m_services;
        private static IPlatformEnvironment m_environment;

	    public static IPlatformServices Services
	    {
		    get { return m_services; }
			internal set { m_services = value; }
	    }

        public static IPlatformEnvironment Environment
        {
            get { return m_environment; }
            set { m_environment = value; }
        }
    }
}
