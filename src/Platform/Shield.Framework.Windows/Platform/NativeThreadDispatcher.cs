#region Usings
using System;
using System.Threading;
using Shield.Framework.Collections;
using Shield.Framework.Platform.Interop;
using Shield.Framework.Platform.Interop.User32;
using Shield.Framework.Platform.Interop.Kernel32;
using static Shield.Framework.Platform.Interop.User32.Methods;
using static Shield.Framework.Platform.Interop.Kernel32.Methods;
using Shield.Framework.Threading;
#endregion

namespace Shield.Framework.Platform
{
    public class NativeThreadDispatcher : INativeThreadDispatcher
    {
        #region Events
        public event Action<DispatcherPriority?> Signaled;
        #endregion

        #region Members
        private readonly ConcurrentList<Delegate> _delegates = new ConcurrentList<Delegate>();
        private readonly Thread m_thread;
        private bool m_quitSend;
        #endregion

        #region Properties
        public bool CurrentThreadIsLoopThread
        {
            get { return m_thread == Thread.CurrentThread; }
        }

        public Thread Thread
        {
            get { return m_thread; }
        }
        #endregion

        public NativeThreadDispatcher()
        {
            m_thread = Thread.CurrentThread;
        }

        #region Methods
        public bool HasMessages(out Message msg)
        {
            return PeekMessage(out msg, IntPtr.Zero, 0, 0, PeekMessageFlags.PM_REMOVE);
        }

        public void ProcessMessage(ref Message msg)
        {
            TranslateMessage(ref msg);
            DispatchMessage(ref msg);
        }

        public void RunLoop(CancellationToken cancellationToken)
        {
            do
            {
                if (!HasMessages(out var msg))
                    continue;

                if ((WindowsMessage)msg.Value == WindowsMessage.QUIT)
                    continue;
                
                ProcessMessage(ref msg);
            } while (!cancellationToken.IsCancellationRequested);
        }

        public IDisposable StartTimer(DispatcherPriority priority, TimeSpan interval, Action callback)
        {
            //UnmanagedMethods.TimerProc timerDelegate =
            //    (hWnd, uMsg, nIDEvent, dwTime) => callback();

            //IntPtr handle = UnmanagedMethods.SetTimer(
            //    IntPtr.Zero,
            //    IntPtr.Zero,
            //    (uint)interval.TotalMilliseconds,
            //    timerDelegate);

            //// Prevent timerDelegate being garbage collected.
            //_delegates.Add(timerDelegate);

            //return Disposable.Create(() =>
            //{
            //    _delegates.Remove(timerDelegate);
            //    UnmanagedMethods.KillTimer(IntPtr.Zero, handle);
            //});
            throw new NotImplementedException();
        }

        public void Signal(DispatcherPriority prio)
        {
            PostMessage(PlatformManager.CurrentPlatform.Application.Handle.Pointer,
                        (uint)WindowsMessage.DISPATCH_WORK_ITEM,
                        new IntPtr(Constants.SignalW),
                        new IntPtr(Constants.SignalL));
        }

        public void Signal()
        {
            Signaled?.Invoke(null);
        }
        #endregion
    }
}