using System;
using System.Collections.Generic;
using System.Text;

namespace Shield.Framework.Extensibility
{
    public interface IAssemblyResolver : IDispose
    {
        void LoadAssemblyFrom(string assemblyPath);
    }
}
