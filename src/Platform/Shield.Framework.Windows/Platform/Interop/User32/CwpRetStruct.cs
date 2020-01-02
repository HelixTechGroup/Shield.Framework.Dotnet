using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Shield.Framework.Platform.Interop.User32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CwpRetStruct
    {
        public IntPtr LResult;
        public IntPtr LParam;
        public IntPtr WParam;
        public uint Message;
        public IntPtr Hwnd;
    }
}
