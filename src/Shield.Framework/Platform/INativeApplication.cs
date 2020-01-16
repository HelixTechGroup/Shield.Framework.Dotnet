using System;
using System.Collections.Generic;
using Shin.Framework;

namespace Shield.Framework.Platform
{
    public interface INativeApplication : INativeObject, IInitialize, IDispose
    {
        event EventHandler<INativeWindow> WindowCreated;
        event EventHandler<INativeWindow> WindowDestroyed;

        #region Properties
        IEnumerable<INativeWindow> Windows { get; }
        #endregion

        INativeWindow CreateWindow();
    }
}