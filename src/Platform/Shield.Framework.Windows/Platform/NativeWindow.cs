using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using Shield.Framework.Drawing;
using Shield.Framework.Extensions;
using Shield.Framework.Platform.Interop;
using Shield.Framework.Platform.Interop.User32;
using Shield.Framework.Platform.Interop.Kernel32;
using static Shield.Framework.Platform.Interop.User32.Methods;
using static Shield.Framework.Platform.Interop.Kernel32.Methods;

namespace Shield.Framework.Platform
{
    public class NativeWindow : INativeWindow, IWindowsProcess
    {
        private INativeHandle m_handle;
        private string m_title;
        private int m_width;
        private int m_height;
        private bool m_resizable;
        private WindowProc _wndProc;
        private readonly string m_windowClass;
        private IWindowsProcess m_parent;
        private INativeRender m_rendererHandler;
        private INativeInput m_inputHandler;

        /// <inheritdoc />
        public event EventHandler Closing;

        /// <inheritdoc />
        public event EventHandler Closed;

        /// <inheritdoc />
        public event EventHandler Created;

        /// <inheritdoc />
        public event EventHandler Destroyed;

        /// <inheritdoc />
        public event EventHandler Activating;

        /// <inheritdoc />
        public event EventHandler Activated;

        /// <inheritdoc />
        public event EventHandler Deactivating;

        /// <inheritdoc />
        public event EventHandler Deactivated;

        /// <inheritdoc />
        public string Title
        {
            get { return m_title; }
        }

        /// <inheritdoc />
        public int Width
        {
            get { return m_width; }
        }

        /// <inheritdoc />
        public int Height
        {
            get { return m_height; }
        }

        /// <inheritdoc />
        public INativeHandle Handle
        {
            get { return m_handle; }
        }

        /// <inheritdoc />
        public WindowProc Process
        {
            get { return _wndProc; }
        }

        /// <inheritdoc />
        public INativeHandle ParentHandle
        {
            get { return m_parent?.Handle; }
        }

        /// <inheritdoc />
        public INativeRender Renderer
        {
            get { return m_rendererHandler; }
        }

        /// <inheritdoc />
        public void Show()
        {
            ShowWindow(m_handle.Pointer, ShowWindowCommands.SW_SHOWNORMAL);
            UpdateWindow(m_handle.Pointer);
        }

        /// <inheritdoc />
        public void Hide()
        {
            ShowWindow(m_handle.Pointer, ShowWindowCommands.SW_HIDE);
            UpdateWindow(m_handle.Pointer);
        }

        /// <inheritdoc />
        public void Create()
        {
            Create(null);
        }

        /// <inheritdoc />
        public void Create(INativeObject parent)
        {
            if (parent != null)
                m_parent = parent as IWindowsProcess;

            var x = 0;
            var y = 0;
            WindowStyles style = 0;
            WindowExStyles styleEx = 0;

            if (Width > 0 && Height > 0)
            {
                var screenWidth = GetSystemMetrics(SystemMetrics.SM_CXSCREEN);
                var screenHeight = GetSystemMetrics(SystemMetrics.SM_CYSCREEN);

                // Place the window in the middle of the screen.WS_EX_APPWINDOW
                x = (screenWidth - Width) / 2;
                y = (screenHeight - Height) / 2;
            }

            if (m_resizable)
                style = WindowStyles.WS_OVERLAPPEDWINDOW;
            else
                style = WindowStyles.WS_POPUP | WindowStyles.WS_BORDER | WindowStyles.WS_CAPTION | WindowStyles.WS_SYSMENU;

            styleEx = WindowExStyles.WS_EX_APPWINDOW | WindowExStyles.WS_EX_WINDOWEDGE;
            style |= WindowStyles.WS_CLIPCHILDREN | WindowStyles.WS_CLIPSIBLINGS;

            int windowWidth;
            int windowHeight;

            if (Width > 0 && Height > 0)
            {
                var rect = new Rectangle(0, 0, Width, Height);

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
            _wndProc = WindowProc;
            var hInstance = GetModuleHandle(null);

            var wndClassEx = new WindowClassEx()
                             {
                                 Size = (uint)Marshal.SizeOf<WindowClassEx>(),
                                 Styles = (WindowClassStyles.CS_OWNDC | WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW), // Unique DC helps with performance when using Gpu based rendering
                                 WindowProc = _wndProc,
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
                                      m_title,
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
            m_width = windowWidth;
            m_height = windowHeight;
            m_rendererHandler = new NativeRenderer(this);
            m_inputHandler = new NativeInputHandler(this);
        }

        public NativeWindow(string title, int width, int height) : this(title, width, height, true){ }

        public NativeWindow(string title, int width, int height, bool resizable)
        {
            m_title = title;
            m_width = width;
            m_height = height;
            m_resizable = resizable;
            m_windowClass = "ShieldWindow-" + Guid.NewGuid();
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
    }
}
