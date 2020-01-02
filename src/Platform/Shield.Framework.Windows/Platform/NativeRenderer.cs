using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Shield.Framework.Drawing;
using Shield.Framework.Extensions;
using Shield.Framework.Platform.Interop.User32;
using static Shield.Framework.Platform.Interop.User32.Methods;
using static Shield.Framework.Platform.Interop.Kernel32.Methods;

namespace Shield.Framework.Platform
{
    public class NativeRenderer : WindowsProcessHook, INativeRender
    {
        private double m_scaling;
        private Size m_clientSize;
        private double m_scaling1;

        /// <inheritdoc />
        public event EventHandler Painting;

        /// <inheritdoc />
        public event EventHandler<Rectangle> Paint;

        /// <inheritdoc />
        public event EventHandler Painted;

        /// <inheritdoc />
        public event EventHandler<Size> Resized;

        /// <inheritdoc />
        public event EventHandler<double> ScalingChanged;

        /// <inheritdoc />
        public event EventHandler<Point> PositionChanged;

        /// <inheritdoc />
        public Size ClientSize
        {
            get { return m_clientSize; }
        }

        /// <inheritdoc />
        public double Scaling
        {
            get { return m_scaling1; }
        }

        /// <inheritdoc />
        public void Invalidate(Rectangle rect)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Point PointToClient(Point point)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Point PointToScreen(Point point)
        {
            throw new NotImplementedException();
        }

        public NativeRenderer(IWindowsProcess window) : base(window, WindowHookType.WH_GETMESSAGE) { }

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

        public bool Validate(ref Rectangle rect)
        {
            return ValidateRect(m_process.Handle.Pointer, ref rect);
        }

        public bool Validate()
        {
            return ValidateRect(m_process.Handle.Pointer, IntPtr.Zero);
        }

        public bool Invalidate(ref Rectangle rect, bool shouldErase = false)
        {
            return InvalidateRect(m_process.Handle.Pointer, ref rect, shouldErase);
        }

        public bool Invalidate(bool shouldErase = false)
        {
            return InvalidateRect(m_process.Handle.Pointer, IntPtr.Zero, shouldErase);
        }

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
    }
}
