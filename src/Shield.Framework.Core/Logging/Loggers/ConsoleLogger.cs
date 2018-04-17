using System;

namespace Shield.Framework.Logging.Loggers
{
    public class ConsoleLogger : TextLogger
    {
        public ConsoleLogger()
        {
            m_writer = Console.Out;
        }
    }
}
