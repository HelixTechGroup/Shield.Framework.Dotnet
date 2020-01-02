using System;
using System.Collections.Generic;
using System.Text;
using Shield.Framework.Platform.Interop.User32;

namespace Shield.Framework.Platform
{
    public struct WindowMessage
    {
        public IntPtr Hwnd;
        public WindowsMessage Id;
        public IntPtr WParam;
        public IntPtr LParam;
        public IntPtr Result;

        public WindowMessage(IntPtr hwnd, uint id, IntPtr wParam, IntPtr lParam)
        {
            this.Hwnd = hwnd;
            this.Id = (WindowsMessage)id;
            this.WParam = wParam;
            this.LParam = lParam;
            this.Result = IntPtr.Zero;
        }

        public void SetResult(IntPtr result)
        {
            this.Result = result;
        }

        public void SetResult(int result)
        {
            this.SetResult(new IntPtr(result));
        }
    }
}
