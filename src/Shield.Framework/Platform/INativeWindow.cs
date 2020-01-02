using System;
using System.Drawing;

namespace Shield.Framework.Platform
{
    public interface INativeWindow : INativeObject, IDisposable
    {
        event EventHandler Closing;
        event EventHandler Closed;
        event EventHandler Created;
        event EventHandler Destroyed;
        event EventHandler Activating;
        event EventHandler Activated;
        event EventHandler Deactivating;
        event EventHandler Deactivated;

        #region Properties
        string Title { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        bool IsApplicationWindow { get; }
        bool IsChildWindow { get; }
        bool IsForegroundWindow { get; }
        Size ClientSize { get; }
        Size MaxClientSize { get; }
        INativeHandle ParentHandle { get; }
        INativeRender Renderer { get; }
        INativeInput Input { get; }
        #endregion

        void Show();
        void Hide();
        void Create();
        void Create(INativeObject parent);
        void Destroy();
        bool IsPointInWindow(Point point);
        bool BringToForeground();
        bool MoveToBackground();
    }
}