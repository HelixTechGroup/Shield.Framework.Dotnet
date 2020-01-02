using System.Runtime.InteropServices;

namespace Shield.Framework.Platform.Interop.User32 {
    [StructLayout(LayoutKind.Sequential)]
    public struct LastInputInfo
    {
        public uint Size;
        public uint Time;
    }
}