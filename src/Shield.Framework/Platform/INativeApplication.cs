using System.Collections.Generic;

namespace Shield.Framework.Platform
{
    public interface INativeApplication : INativeObject, IInitialize, IDispose
    {
        #region Properties
        IEnumerable<INativeWindow> Windows { get; }
        #endregion

        INativeWindow CreateWindow();
    }
}