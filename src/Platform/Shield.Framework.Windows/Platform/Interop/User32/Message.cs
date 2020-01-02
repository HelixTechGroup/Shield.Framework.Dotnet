using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Shield.Framework.Drawing;

namespace Shield.Framework.Platform.Interop.User32 {
    [StructLayout(LayoutKind.Sequential)]
    public struct Message
    {
        public IntPtr Hwnd;
        public uint Value;
        public IntPtr WParam;
        public IntPtr LParam;
        public uint Time;
        public Point Point;
    }
}