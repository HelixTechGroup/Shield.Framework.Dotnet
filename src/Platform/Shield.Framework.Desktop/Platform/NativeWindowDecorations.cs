using System;
using System.Collections.Generic;
using System.Text;

namespace Shield.Framework.Platform
{
    [Flags]
    public enum NativeWindowDecorations
    {
        None = 0,
        CloseButton,
        MinimizeButton,
        MaximizeButton,
        Titlebar,
        Border,
        All = CloseButton | MinimizeButton | MaximizeButton | Titlebar | Border
    }
}
