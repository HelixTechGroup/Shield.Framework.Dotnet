using System.Runtime.InteropServices;

namespace Shield.Framework.Platform.Interop.User32 {
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct NcCalcSizeParams
    {
        public NcCalcSizeRegionUnion Region;
        public WindowPosition* Position;
    }
}