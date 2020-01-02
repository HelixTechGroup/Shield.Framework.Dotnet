using System.Drawing;
using System.Runtime.InteropServices;
using Shield.Framework.Drawing;

namespace Shield.Framework.Platform.Interop.User32 {
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowPlacement
    {
        public uint Size;
        public WindowPlacementFlags Flags;
        public ShowWindowCommands ShowCmd;
        public Point MinPosition;
        public Point MaxPosition;
        public Rectangle NormalPosition;
    }
}