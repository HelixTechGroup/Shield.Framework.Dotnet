using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shield.Framework.Logging
{
    public interface ILogProvider : IDispose
    {
        void Flush(ILogEntry entry);
    }
}
