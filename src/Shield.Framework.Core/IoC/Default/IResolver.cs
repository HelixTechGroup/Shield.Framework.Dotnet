using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shield.Framework.IoC.Default
{
    internal interface IResolver : IDispose
    {
        bool Singleton { get; set; }

        Func<object> CreateInstanceFunc { get; set; }

        object GetObject();
    }
}
