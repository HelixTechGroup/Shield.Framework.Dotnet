using System;

namespace Shield.Framework.Platform.Logging.Loggers
{
    public class ConsoleLogger : TextLogger
    {
        public ConsoleLogger()
        {
            m_writer = Console.Out;
        }
    }
}
