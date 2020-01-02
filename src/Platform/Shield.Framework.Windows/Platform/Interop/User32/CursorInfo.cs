using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Shield.Framework.Drawing;

namespace Shield.Framework.Platform.Interop.User32 {
    [StructLayout(LayoutKind.Sequential)]
    public struct CursorInfo
    {
        public uint Size;
        public CursorInfoFlags Flags;
        public IntPtr CursorHandle;
        public Point ScreenPosition;
    }
}