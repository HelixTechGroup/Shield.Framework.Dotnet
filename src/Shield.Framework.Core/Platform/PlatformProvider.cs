using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shield.Framework.Platform.Threading;

namespace Shield.Framework.Platform
{
    public static class PlatformProvider
    {
        private static IPlatformDispatcher m_uiDispatcher = new DefaultPlatformUIDispatcher();
        private static IPlatformDispatcher m_backgroundDispatcher = new DefaultPlatformBackgroundDispatcher();
        private static IPlatformDispatcher m_contextDispatcher = new DefaultPlatformContextDispatcher();

        public static IPlatformDispatcher UIDispatcher
        {
            get { return m_uiDispatcher; }
            set { m_uiDispatcher = value; }
        }

        public static IPlatformDispatcher BackgroundDispatcher
        {
            get { return m_backgroundDispatcher; }
            set { m_backgroundDispatcher = value; }
        }

        public static IPlatformDispatcher ContextDispatcher
        {
            get { return m_contextDispatcher; }
            set { m_contextDispatcher = value; }
        }
    }
}
