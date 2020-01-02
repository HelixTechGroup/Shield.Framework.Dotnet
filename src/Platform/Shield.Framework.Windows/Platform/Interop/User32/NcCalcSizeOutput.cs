using System.Drawing;
using System.Runtime.InteropServices;

namespace Shield.Framework.Platform.Interop.User32 {
    [StructLayout(LayoutKind.Sequential)]
    public struct NcCalcSizeOutput
    {
        public Rectangle TargetClientRect;
        public Rectangle DestRect;
        public Rectangle SrcRect;
    }
}