#region Usings
using System;
using System.Drawing;
using Shield.Framework.Platform.Interop.User32;
using static Shield.Framework.Platform.Interop.User32.Methods;
#endregion

namespace Shield.Framework.Platform
{
    public class NativeRenderer : WindowsProcessHook, INativeRenderer
    {
        #region Events
        /// <inheritdoc />
        public event EventHandler Painting;

        /// <inheritdoc />
        public event EventHandler<Rectangle> Paint;

        /// <inheritdoc />
        public event EventHandler Painted;
        #endregion

        #region Members
        private INativeScreen m_screen;
        private Size m_virutalSize;
        private float m_aspectRatio;
        #endregion

        #region Properties
        /// <inheritdoc />
        public INativeScreen Screen
        {
            get { return m_screen; }
        }

        /// <inheritdoc />
        public Size VirutalSize
        {
            get { return m_virutalSize; }
        }

        /// <inheritdoc />
        public float AspectRatio
        {
            get { return m_aspectRatio; }
        }
        #endregion

        public NativeRenderer(IWindowsProcess window) : base(window, WindowHookType.WH_GETMESSAGE) { }

        #region Methods
        /// <inheritdoc />
        protected override IntPtr OnGetMsg(WindowMessage message)
        {
            switch (message.Id)
            {
                case WindowsMessage.PAINT:
                    return OnPaint(message);
                case WindowsMessage.NCCALCSIZE:
                case WindowsMessage.SIZE:
                case WindowsMessage.MOVE:
                case WindowsMessage.WINDOWPOSCHANGED:
                case WindowsMessage.WINDOWPOSCHANGING:
                case WindowsMessage.ERASEBKGND:
                case WindowsMessage.DISPLAYCHANGE:
                case WindowsMessage.CAPTURECHANGED:
                case WindowsMessage.NCHITTEST:
                case WindowsMessage.GETMINMAXINFO:
                case WindowsMessage.NCPAINT:
                    break;
            }

            return base.OnGetMsg(message);
        }

        public bool Validate(ref Rectangle rect)
        {
            return ValidateRect(m_process.Handle.Pointer, ref rect);
        }

        public bool Validate()
        {
            return ValidateRect(m_process.Handle.Pointer, IntPtr.Zero);
        }

        public bool Invalidate(ref Rectangle rect, bool shouldErase)
        {
            return InvalidateRect(m_process.Handle.Pointer, ref rect, shouldErase);
        }

        public bool Invalidate(bool shouldErase)
        {
            return InvalidateRect(m_process.Handle.Pointer, IntPtr.Zero, shouldErase);
        }

        public bool Invalidate()
        {
            return Invalidate(false);
        }

        private IntPtr OnPaint(WindowMessage msg)
        {
            if (GetUpdateRect(m_process.Handle.Pointer, out var rec, false))
                Validate();

            //if (BeginPaint(m_process.Handle.Pointer, out var ps) == IntPtr.Zero) 
            //    return IntPtr.Zero;

            //var f = m_scaling;
            //var r = ps.PaintRect;
            //Painting.Raise(this, null);
            //Paint.Raise(this, new Rectangle((int)Math.Floor(r.Left / f),
            //                                (int)Math.Floor(r.Top / f),
            //                                (int)Math.Floor((r.Right - r.Left) / f),
            //                                (int)Math.Floor((r.Bottom - r.Top) / f)));
            //Painted.Raise(this, null);
            //EndPaint(m_process.Handle.Pointer, ref ps);
            //Validate();

            return IntPtr.Zero;
        }
        #endregion
    }
}