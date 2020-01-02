using System;
using System.Collections.Generic;
using System.Text;
using Shield.Framework.Platform;
using Shield.Framework.Platform.Interop.User32;

namespace Shield.Framework.Platform
{
    public class NativeInputHandler : WindowsProcessHook, INativeInput
    {
        /// <inheritdoc />
        public NativeInputHandler(IWindowsProcess process) : base(process, WindowHookType.WH_GETMESSAGE) { }

        /// <inheritdoc />
        protected override IntPtr OnGetMsg(WindowMessage message)
        {
            switch (message.Id)
            {
                case WindowsMessage.INPUT:
                    break;
            }

            return base.OnGetMsg(message);
        }
    }
}
