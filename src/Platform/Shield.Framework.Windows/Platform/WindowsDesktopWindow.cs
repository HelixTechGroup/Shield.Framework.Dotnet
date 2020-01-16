#region Usings
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using Shield.Framework.Platform.Interop;
using Shield.Framework.Platform.Interop.User32;
using Shin.Framework.Collections.Concurrent;
using static Shield.Framework.Platform.Interop.User32.Methods;
using static Shield.Framework.Platform.Interop.Kernel32.Methods;
#endregion

namespace Shield.Framework.Platform
{
    public class WindowsDesktopWindow : NativeDesktopWindow, IWindowsProcess
    {
        private WindowProc m_wndProc;
        private string m_windowClass;

        public WindowsDesktopWindow(NativeDesktopWindowDefinition definition) : this(definition, null) { }

        public WindowsDesktopWindow(NativeDesktopWindowDefinition definition, INativeObject parent) : base(definition, parent)
        {
            m_windowClass = "ShieldWindow-" + Guid.NewGuid();
            m_childWindows = new ConcurrentList<INativeDesktopWindow>();
        }

        #region Methods
        protected virtual IntPtr OnMessage(WindowMessage message)
        {
            switch (message.Id)
            {
                case WindowsMessage.DESTROY:
                    PostQuitMessage(0);
                    return IntPtr.Zero;
                case WindowsMessage.QUIT:
                    return IntPtr.Zero;
                case WindowsMessage.PAINT:
                case WindowsMessage.NULL:
                case WindowsMessage.MOVE:
                case WindowsMessage.SIZE:
                case WindowsMessage.ENABLE:
                case WindowsMessage.SETREDRAW:
                case WindowsMessage.SETTEXT:
                case WindowsMessage.GETTEXT:
                case WindowsMessage.GETTEXTLENGTH:
                case WindowsMessage.QUERYENDSESSION:
                case WindowsMessage.QUERYOPEN:
                case WindowsMessage.ENDSESSION:
                case WindowsMessage.ERASEBKGND:
                case WindowsMessage.SYSCOLORCHANGE:
                case WindowsMessage.WININICHANGE:
                case WindowsMessage.DEVMODECHANGE:
                case WindowsMessage.FONTCHANGE:
                case WindowsMessage.CANCELMODE:
                case WindowsMessage.SETCURSOR:
                case WindowsMessage.CHILDACTIVATE:
                case WindowsMessage.QUEUESYNC:
                case WindowsMessage.GETMINMAXINFO:
                case WindowsMessage.PAINTICON:
                case WindowsMessage.ICONERASEBKGND:
                case WindowsMessage.NEXTDLGCTL:
                case WindowsMessage.SPOOLERSTATUS:
                case WindowsMessage.DRAWITEM:
                case WindowsMessage.MEASUREITEM:
                case WindowsMessage.DELETEITEM:
                case WindowsMessage.VKEYTOITEM:
                case WindowsMessage.CHARTOITEM:
                case WindowsMessage.SETFONT:
                case WindowsMessage.GETFONT:
                case WindowsMessage.SETHOTKEY:
                case WindowsMessage.GETHOTKEY:
                case WindowsMessage.QUERYDRAGICON:
                case WindowsMessage.COMPAREITEM:
                case WindowsMessage.GETOBJECT:
                case WindowsMessage.COMPACTING:
                case WindowsMessage.COMMNOTIFY:
                case WindowsMessage.WINDOWPOSCHANGING:
                case WindowsMessage.WINDOWPOSCHANGED:
                case WindowsMessage.POWER:
                case WindowsMessage.COPYDATA:
                case WindowsMessage.CANCELJOURNAL:
                case WindowsMessage.NOTIFY:
                case WindowsMessage.INPUTLANGCHANGEREQUEST:
                case WindowsMessage.INPUTLANGCHANGE:
                case WindowsMessage.TCARD:
                case WindowsMessage.HELP:
                case WindowsMessage.USERCHANGED:
                case WindowsMessage.NOTIFYFORMAT:
                case WindowsMessage.CONTEXTMENU:
                case WindowsMessage.STYLECHANGING:
                case WindowsMessage.STYLECHANGED:
                case WindowsMessage.DISPLAYCHANGE:
                case WindowsMessage.GETICON:
                case WindowsMessage.SETICON:
                case WindowsMessage.NCCREATE:
                case WindowsMessage.NCCALCSIZE:
                case WindowsMessage.NCHITTEST:
                case WindowsMessage.NCPAINT:
                case WindowsMessage.GETDLGCODE:
                case WindowsMessage.SYNCPAINT:
                case WindowsMessage.NCLBUTTONDOWN:
                case WindowsMessage.NCLBUTTONUP:
                case WindowsMessage.NCLBUTTONDBLCLK:
                case WindowsMessage.NCRBUTTONDOWN:
                case WindowsMessage.NCRBUTTONUP:
                case WindowsMessage.NCRBUTTONDBLCLK:
                case WindowsMessage.NCMBUTTONDOWN:
                case WindowsMessage.NCMBUTTONUP:
                case WindowsMessage.NCMBUTTONDBLCLK:
                case WindowsMessage.NCXBUTTONDOWN:
                case WindowsMessage.NCXBUTTONUP:
                case WindowsMessage.NCXBUTTONDBLCLK:
                case WindowsMessage.INPUT_DEVICE_CHANGE:
                case WindowsMessage.INPUT:
                case WindowsMessage.UNICHAR:
                case WindowsMessage.IME_STARTCOMPOSITION:
                case WindowsMessage.IME_ENDCOMPOSITION:
                case WindowsMessage.IME_COMPOSITION:
                case WindowsMessage.INITDIALOG:
                case WindowsMessage.TIMER:
                case WindowsMessage.HSCROLL:
                case WindowsMessage.VSCROLL:
                case WindowsMessage.INITMENU:
                case WindowsMessage.INITMENUPOPUP:
                case WindowsMessage.GESTURE:
                case WindowsMessage.GESTURENOTIFY:
                case WindowsMessage.MENUSELECT:
                case WindowsMessage.MENUCHAR:
                case WindowsMessage.ENTERIDLE:
                case WindowsMessage.MENURBUTTONUP:
                case WindowsMessage.MENUDRAG:
                case WindowsMessage.MENUGETOBJECT:
                case WindowsMessage.UNINITMENUPOPUP:
                case WindowsMessage.CHANGEUISTATE:
                case WindowsMessage.UPDATEUISTATE:
                case WindowsMessage.QUERYUISTATE:
                case WindowsMessage.CTLCOLORMSGBOX:
                case WindowsMessage.CTLCOLOREDIT:
                case WindowsMessage.CTLCOLORLISTBOX:
                case WindowsMessage.CTLCOLORBTN:
                case WindowsMessage.CTLCOLORDLG:
                case WindowsMessage.CTLCOLORSCROLLBAR:
                case WindowsMessage.CTLCOLORSTATIC:
                case WindowsMessage.PARENTNOTIFY:
                case WindowsMessage.ENTERMENULOOP:
                case WindowsMessage.EXITMENULOOP:
                case WindowsMessage.NEXTMENU:
                case WindowsMessage.SIZING:
                case WindowsMessage.MOVING:
                case WindowsMessage.POWERBROADCAST:
                case WindowsMessage.DEVICECHANGE:
                case WindowsMessage.MDICREATE:
                case WindowsMessage.MDIDESTROY:
                case WindowsMessage.MDIACTIVATE:
                case WindowsMessage.MDIRESTORE:
                case WindowsMessage.MDINEXT:
                case WindowsMessage.MDIMAXIMIZE:
                case WindowsMessage.MDITILE:
                case WindowsMessage.MDICASCADE:
                case WindowsMessage.MDIICONARRANGE:
                case WindowsMessage.MDIGETACTIVE:
                case WindowsMessage.MDISETMENU:
                case WindowsMessage.ENTERSIZEMOVE:
                case WindowsMessage.EXITSIZEMOVE:
                case WindowsMessage.DROPFILES:
                case WindowsMessage.MDIREFRESHMENU:
                case WindowsMessage.POINTERDEVICECHANGE:
                case WindowsMessage.POINTERDEVICEINRANGE:
                case WindowsMessage.POINTERDEVICEOUTOFRANGE:
                case WindowsMessage.TOUCH:
                case WindowsMessage.NCPOINTERUPDATE:
                case WindowsMessage.NCPOINTERDOWN:
                case WindowsMessage.NCPOINTERUP:
                case WindowsMessage.POINTERUPDATE:
                case WindowsMessage.POINTERDOWN:
                case WindowsMessage.POINTERUP:
                case WindowsMessage.POINTERENTER:
                case WindowsMessage.POINTERLEAVE:
                case WindowsMessage.POINTERACTIVATE:
                case WindowsMessage.POINTERCAPTURECHANGED:
                case WindowsMessage.TOUCHHITTESTING:
                case WindowsMessage.POINTERWHEEL:
                case WindowsMessage.POINTERHWHEEL:
                case WindowsMessage.IME_SETCONTEXT:
                case WindowsMessage.IME_NOTIFY:
                case WindowsMessage.IME_CONTROL:
                case WindowsMessage.IME_COMPOSITIONFULL:
                case WindowsMessage.IME_SELECT:
                case WindowsMessage.IME_CHAR:
                case WindowsMessage.IME_REQUEST:
                case WindowsMessage.IME_KEYDOWN:
                case WindowsMessage.IME_KEYUP:
                case WindowsMessage.NCMOUSEHOVER:
                case WindowsMessage.NCMOUSELEAVE:
                case WindowsMessage.WTSSESSION_CHANGE:
                case WindowsMessage.TABLET_FIRST:
                case WindowsMessage.TABLET_LAST:
                case WindowsMessage.DPICHANGED:
                case WindowsMessage.CUT:
                case WindowsMessage.COPY:
                case WindowsMessage.PASTE:
                case WindowsMessage.CLEAR:
                case WindowsMessage.UNDO:
                case WindowsMessage.RENDERFORMAT:
                case WindowsMessage.RENDERALLFORMATS:
                case WindowsMessage.DESTROYCLIPBOARD:
                case WindowsMessage.DRAWCLIPBOARD:
                case WindowsMessage.PAINTCLIPBOARD:
                case WindowsMessage.VSCROLLCLIPBOARD:
                case WindowsMessage.SIZECLIPBOARD:
                case WindowsMessage.ASKCBFORMATNAME:
                case WindowsMessage.CHANGECBCHAIN:
                case WindowsMessage.HSCROLLCLIPBOARD:
                case WindowsMessage.QUERYNEWPALETTE:
                case WindowsMessage.PALETTEISCHANGING:
                case WindowsMessage.PALETTECHANGED:
                case WindowsMessage.PRINT:
                case WindowsMessage.PRINTCLIENT:
                case WindowsMessage.THEMECHANGED:
                case WindowsMessage.CLIPBOARDUPDATE:
                case WindowsMessage.DWMCOMPOSITIONCHANGED:
                case WindowsMessage.DWMNCRENDERINGCHANGED:
                case WindowsMessage.DWMCOLORIZATIONCOLORCHANGED:
                case WindowsMessage.DWMWINDOWMAXIMIZEDCHANGE:
                case WindowsMessage.DWMSENDICONICTHUMBNAIL:
                case WindowsMessage.DWMSENDICONICLIVEPREVIEWBITMAP:
                case WindowsMessage.GETTITLEBARINFOEX:
                case WindowsMessage.HANDHELDFIRST:
                case WindowsMessage.HANDHELDLAST:
                case WindowsMessage.AFXFIRST:
                case WindowsMessage.AFXLAST:
                case WindowsMessage.PENWINFIRST:
                case WindowsMessage.PENWINLAST:
                case WindowsMessage.APP:
                case WindowsMessage.USER:

                case WindowsMessage.NCDESTROY:
                case WindowsMessage.CLOSE:
                case WindowsMessage.TIMECHANGE:
                case WindowsMessage.MOUSELEAVE:
                case WindowsMessage.NCACTIVATE:
                case WindowsMessage.SHOWWINDOW:
                case WindowsMessage.CREATE:
                case WindowsMessage.ACTIVATE:
                case WindowsMessage.ACTIVATEAPP:
                case WindowsMessage.MOUSEMOVE:
                case WindowsMessage.LBUTTONUP:
                case WindowsMessage.LBUTTONDOWN:
                case WindowsMessage.LBUTTONDBLCLK:
                case WindowsMessage.RBUTTONUP:
                case WindowsMessage.RBUTTONDOWN:
                case WindowsMessage.RBUTTONDBLCLK:
                case WindowsMessage.MBUTTONUP:
                case WindowsMessage.MBUTTONDOWN:
                case WindowsMessage.MBUTTONDBLCLK:
                case WindowsMessage.XBUTTONUP:
                case WindowsMessage.XBUTTONDOWN:
                case WindowsMessage.XBUTTONDBLCLK:
                case WindowsMessage.MOUSEACTIVATE:
                case WindowsMessage.MOUSEHOVER:
                case WindowsMessage.MOUSEWHEEL:
                case WindowsMessage.MOUSEHWHEEL:
                case WindowsMessage.CHAR:
                case WindowsMessage.SYSCHAR:
                case WindowsMessage.DEADCHAR:
                case WindowsMessage.SYSDEADCHAR:
                case WindowsMessage.KEYUP:
                case WindowsMessage.KEYDOWN:
                case WindowsMessage.SYSKEYUP:
                case WindowsMessage.SYSKEYDOWN:
                case WindowsMessage.COMMAND:
                case WindowsMessage.SYSCOMMAND:
                case WindowsMessage.MENUCOMMAND:
                case WindowsMessage.APPCOMMAND:
                case WindowsMessage.KILLFOCUS:
                case WindowsMessage.SETFOCUS:
                case WindowsMessage.CAPTURECHANGED:
                case WindowsMessage.HOTKEY:
                case WindowsMessage.NCMOUSEMOVE:
                default:
                    return DefWindowProc(message.Hwnd, message.Id, message.WParam, message.LParam);
            }
        }

        /// <inheritdoc />
        protected override Rectangle PlatformGetClientArea()
        {
            GetClientRect(m_handle.Pointer, out var rect);
            return rect;
        }

        /// <inheritdoc />
        protected override Size PlatformGetClientSize()
        {
            GetClientRect(m_handle.Pointer, out var rect);
            return Size.Round(new SizeF(rect.Right, rect.Bottom) / (float)((INativeDesktopRenderer)m_renderer).DpiScaling);
        }

        /// <inheritdoc />
        protected override Size PlatformGetMaxClientSize()
        {
            var r = m_renderer as INativeDesktopRenderer;
            return Size.Round((new Size(
                                        GetSystemMetrics(SystemMetrics.SM_CXMAXTRACK),
                                        GetSystemMetrics(SystemMetrics.SM_CYMAXTRACK))
                             - r.BorderThickness) / (float)r.DpiScaling);
        }

        /// <inheritdoc />
        protected override void PlatformSetTitle(string value)
        {
            SetWindowText(m_handle.Pointer, value);
        }

        /// <inheritdoc />
        protected override string PlatformGetTitle()
        {

            var size = GetWindowTextLength(m_handle.Pointer);
            if (size <= 0) 
                return string.Empty;
            var len = size + 1;
            var sb = new StringBuilder(len);
            return GetWindowText(m_handle.Pointer, sb, len) > 0 ? sb.ToString() : string.Empty;

        }

        /// <inheritdoc />
        protected override void PlatformShow()
        {
            var shouldActivate = false;
            if (m_definition.AcceptsInput)
                shouldActivate = m_definition.ActivationPolicy == NativeWindowActivationPolicy.Always || m_isFirstTimeVisible && m_definition.ActivationPolicy == NativeWindowActivationPolicy.FirstShown;

            var showWindowCommand = shouldActivate ? ShowWindowCommands.SW_SHOW : ShowWindowCommands.SW_SHOWNOACTIVATE;
            if (m_isFirstTimeVisible)
            {
                m_isFirstTimeVisible = false;
                switch (m_initialState) {
                    case NativeWindowState.Minimized:
                        showWindowCommand = shouldActivate ? ShowWindowCommands.SW_MINIMIZE : ShowWindowCommands.SW_SHOWMINNOACTIVE;
                        break;
                    case NativeWindowState.Maximized:
                        showWindowCommand = shouldActivate ? ShowWindowCommands.SW_SHOWMAXIMIZED : ShowWindowCommands.SW_MAXIMIZE;
                        break;
                }
            }


            ShowWindow(m_handle.Pointer, showWindowCommand);
        }

        /// <inheritdoc />
        protected override void PlatformHide()
        {
            ShowWindow(m_handle.Pointer, ShowWindowCommands.SW_HIDE);
        }

        /// <inheritdoc />
        protected override void PlatformCreate()
        {
            var x = m_definition.DesiredPosition.X;
            var y = m_definition.DesiredPosition.Y;
            WindowStyles style = 0;
            WindowExStyles styleEx = 0;

            if (m_definition.DesiredSize.Width > 0 && m_definition.DesiredSize.Height > 0)
            {
                var screenWidth = GetSystemMetrics(SystemMetrics.SM_CXSCREEN);
                var screenHeight = GetSystemMetrics(SystemMetrics.SM_CYSCREEN);

                // Place the window in the middle of the screen.WS_EX_APPWINDOW
                x = (screenWidth - m_definition.DesiredSize.Width) / 2;
                y = (screenHeight - m_definition.DesiredSize.Height) / 2;
            }

            if (m_definition.SupportedDecorations.HasFlag(NativeWindowDecorations.CloseButton))
                style = WindowStyles.WS_OVERLAPPEDWINDOW;
            else
                style = WindowStyles.WS_POPUP | WindowStyles.WS_BORDER | WindowStyles.WS_CAPTION | WindowStyles.WS_SYSMENU;

            styleEx = WindowExStyles.WS_EX_APPWINDOW | WindowExStyles.WS_EX_WINDOWEDGE;
            style |= WindowStyles.WS_CLIPCHILDREN | WindowStyles.WS_CLIPSIBLINGS;

            int windowWidth;
            int windowHeight;

            if (m_definition.DesiredSize.Width > 0 && m_definition.DesiredSize.Height > 0)
            {
                var rect = new Rectangle(0, 0, m_definition.DesiredSize.Width, m_definition.DesiredSize.Height);

                // Adjust according to window styles
                AdjustWindowRectEx(
                                   ref rect,
                                   style,
                                   false,
                                   styleEx);

                windowWidth = rect.Right - rect.Left;
                windowHeight = rect.Bottom - rect.Top;
            }
            else
                x = y = windowWidth = windowHeight = Constants.CW_USEDEFAULT;

            // Ensure that the delegate doesn't get garbage collected by storing it as a field.
            m_wndProc = WindowProc;
            var hInstance = GetModuleHandle(null);

            var wndClassEx = new WindowClassEx
                             {
                                 Size = (uint)Marshal.SizeOf<WindowClassEx>(),
                                 Styles = WindowClassStyles.CS_OWNDC | WindowClassStyles.CS_HREDRAW |
                                          WindowClassStyles.CS_VREDRAW, // Unique DC helps with performance when using Gpu based rendering
                                 WindowProc = m_wndProc,
                                 InstanceHandle = hInstance,
                                 ClassName = m_windowClass,
                                 BackgroundBrushHandle = (IntPtr)SystemColor.COLOR_WINDOW
                             };

            var atom = RegisterClassEx(ref wndClassEx);
            if (atom == 0)
                throw new Win32Exception();

            var hwnd = CreateWindowEx(
                                      styleEx,
                                      m_windowClass,
                                      m_definition.Title,
                                      style,
                                      x,
                                      y,
                                      windowWidth,
                                      windowHeight,
                                      m_parent?.Handle.Pointer ?? IntPtr.Zero,
                                      IntPtr.Zero,
                                      hInstance,
                                      IntPtr.Zero);

            if (hwnd == IntPtr.Zero)
            {
                var error = GetLastError();
                UnregisterClass(m_windowClass, hInstance);
                throw new Win32Exception((int)error);
            }

            m_handle = new NativeHandle(hwnd, "");
            /// = windowWidth;
            //m_height = windowHeight;
            m_renderer = new NativeRenderer(this);
            m_input = new NativeInput(this);
        }

        /// <inheritdoc />
        protected override void PlatformDestroy()
        {
            DestroyWindow(m_handle.Pointer);
        }

        /// <inheritdoc />
        protected override void PlatformDeactivate()
        {
            return;
        }

        /// <inheritdoc />
        protected override bool PlatformCheckActive()
        {
            return (GetActiveWindow() == m_handle.Pointer);
        }

        /// <inheritdoc />
        public override bool IsPointInWindow(Point point)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override Point PointToClient(Point point)
        {
            var p = point;
            ScreenToClient(m_handle.Pointer, ref p);
            return new Point(p.X, p.Y);
        }

        /// <inheritdoc />
        public override Point PointToScreen(Point point)
        {
            var p = point;
            ClientToScreen(m_handle.Pointer, ref p);
            return new Point(p.X, p.Y);
        }

        /// <inheritdoc />
        protected override void PlatformActivate()
        {
            SetActiveWindow(m_handle.Pointer);
        }

        /// <inheritdoc />
        protected override bool PlatformCheckVisible()
        {
            return IsWindowVisible(m_handle.Pointer);
        }

        private IntPtr WindowProc(IntPtr hwnd, WindowsMessage msg, IntPtr wParam, IntPtr lParam)
        {
            var wMsg = new WindowMessage
                       {
                           Id = msg,
                           WParam = wParam,
                           LParam = lParam,
                           Result = IntPtr.Zero,
                           Hwnd = hwnd
                       };

            return OnMessage(wMsg);
        }
        #endregion

        /// <inheritdoc />
        public WindowProc Process
        {
            get { return m_wndProc; }
        }

        /// <inheritdoc />
        protected override Point PlatformGetPosition()
        {
            GetWindowRect(m_handle.Pointer, out var rect);
            return new Point(rect.Left, rect.Top);
        }

        /// <inheritdoc />
        protected override Size PlatformGetWindowSize()
        {
            GetWindowRect(m_handle.Pointer, out var rect);
            return Size.Round(new SizeF(rect.Right, rect.Bottom) / (float)((INativeDesktopRenderer)m_renderer).DpiScaling);
        }

        /// <inheritdoc />
        protected override void PlatformSetPosition(Point position)
        {
            var x = position.X;
            var y = position.Y;
            if (((INativeDesktopRenderer)m_renderer).SupportedDecorations.HasFlag(NativeWindowDecorations.Border))
            {
                var windowStyle = (WindowStyles)GetWindowLongPtr(m_handle.Pointer, WindowLongFlags.GWL_STYLE);
                var windowExStyle = (WindowExStyles)GetWindowLongPtr(m_handle.Pointer, WindowLongFlags.GWL_EXSTYLE);

                // This adjusts a zero rect to give us the size of the border
                var borderRect = Rectangle.Empty;
                AdjustWindowRectEx(ref borderRect, windowStyle, false, windowExStyle);

                // Border rect size is negative
                x += borderRect.Left;
                y += borderRect.Top;
            }

            SetWindowPos(
                         m_handle.Pointer,
                         HwndZOrder.HWND_TOP,
                         x,
                         y,
                         0,
                         0,
                         WindowPositionFlags.SWP_NOSIZE | WindowPositionFlags.SWP_NOACTIVATE | WindowPositionFlags.SWP_NOZORDER);
        }

        /// <inheritdoc />
        protected override void PlatformSetWindowSize(Size size)
        {
            
        }

        /// <inheritdoc />
        protected override void PlatformFocus()
        {
            SetFocus(m_handle.Pointer);
        }

        /// <inheritdoc />
        protected override bool PlatformCheckFocus()
        {
            return (GetFocus() == m_handle.Pointer);
        }

        /// <inheritdoc />
        protected override void PlatformRestore()
        {
            ShowWindow(m_handle.Pointer, ShowWindowCommands.SW_RESTORE);
        }

        /// <inheritdoc />
        protected override Rectangle PlatformGetRestoreArea()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void PlatformMinimize()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void PlatformMaximize()
        {
            throw new NotImplementedException();
        }

        private void MaximizeWithoutCoveringTaskbar()
        {
            var monitor = MonitorFromWindow(m_handle.Pointer, MonitorFlag.MONITOR_DEFAULTTONEAREST);

            if (monitor == IntPtr.Zero) 
                return;
            var monitorInfo = new MonitorInfo() { Size = (uint)Marshal.SizeOf<MonitorInfo>() };
            GetMonitorInfo(monitor, ref monitorInfo);

            if (!GetMonitorInfo(monitor, ref monitorInfo)) 
                return;

            var x = monitorInfo.WorkRect.Left;
            var y = monitorInfo.WorkRect.Top;
            var cx = Math.Abs(monitorInfo.WorkRect.Right - x);
            var cy = Math.Abs(monitorInfo.WorkRect.Bottom - y);

            SetWindowPos(m_handle.Pointer, HwndZOrder.HWND_NOTOPMOST, x, y, cx, cy, WindowPositionFlags.SWP_SHOWWINDOW);
        }

        /// <inheritdoc />
        protected override NativeWindowState PlatformGetState()
        {
            GetWindowPlacement(m_handle.Pointer, out var placement);
            switch (placement.ShowCmd)
            {
                case ShowWindowCommands.SW_MAXIMIZE:
                    return NativeWindowState.Maximized;
                case ShowWindowCommands.SW_MINIMIZE:
                    return NativeWindowState.Minimized;
                default:
                    return NativeWindowState.Normal;
            }
        }

        /// <inheritdoc />
        protected override void PlatformEnable()
        {
            EnableWindow(m_handle.Pointer, true);
        }

        /// <inheritdoc />
        protected override void PlatformDisable()
        {
            EnableWindow(m_handle.Pointer, false);
        }

        protected override bool PlatformCheckEnable()
        {
            return IsWindowEnabled(m_handle.Pointer);
        }

        /// <inheritdoc />
        public override void BringToFront(bool force)
        {
            if (m_definition.IsRegularWindow)
            {
                if (IsIconic(m_handle.Pointer))
                    Restore();
                else
                    Activate();
            }
            else
            {
                var hWndInsertAfter = HwndZOrder.HWND_TOP;
                // By default we activate the window or it isn't actually brought to the front 
                var flags = WindowPositionFlags.SWP_NOMOVE | WindowPositionFlags.SWP_NOSIZE | WindowPositionFlags.SWP_NOOWNERZORDER;

                if (!force)
                    flags |= WindowPositionFlags.SWP_NOACTIVATE;

                if (m_definition.IsTopmostWindow)
                    hWndInsertAfter = HwndZOrder.HWND_TOPMOST;


                SetWindowPos(m_handle.Pointer, hWndInsertAfter, 0, 0, 0, 0, flags);
            }
        }

        /// <inheritdoc />
        public override void ForceToFront()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void CenterToScreen()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void PlatformDrawAttention()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override INativeDesktopWindow PlatformCreateChildWindow(NativeDesktopWindowDefinition definition)
        {
            return new WindowsDesktopWindow(definition);
        }

        /// <inheritdoc />
        protected override INativePopupWindow PlatformCreatePopupWindow(NativeDesktopWindowDefinition definition)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void DisposeUnmanagedResources()
        {
            UnregisterClass(m_windowClass, m_parent.Handle.Pointer);
            base.DisposeUnmanagedResources();
        }
    }
}