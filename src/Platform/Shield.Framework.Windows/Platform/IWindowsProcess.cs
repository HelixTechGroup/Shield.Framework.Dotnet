using System;
using System.Collections.Generic;
using System.Text;
using Shield.Framework.Platform.Interop.User32;

namespace Shield.Framework.Platform
{
    public interface IWindowsProcess : INativeObject
    {
        WindowProc Process { get; }
    }
}
