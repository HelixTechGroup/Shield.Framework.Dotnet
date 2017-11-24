using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shield.Framework.Logging.Providers
{
    public class ConsoleLogProvider : TextLogProvider
    {
        public ConsoleLogProvider()
        {
            m_writer = Console.Out;
        }
    }
}
