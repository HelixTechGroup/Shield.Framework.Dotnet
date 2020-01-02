using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Shield.Framework.Drawing
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Margins
    {
        public Margins(int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public Margins(int x = 0, int y = 0) : this(x, x, y, y) { }

        public Margins(int all = 0) : this(all, all) { }

        public int Left, Right, Top, Bottom;
    }
}
