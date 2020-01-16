using System;
using System.Collections.Generic;
using System.Text;
using Shield.Framework.Drawing;
using Shin.Framework.ComponentModel;

namespace Shield.Framework.Platform
{
    public interface INativeDesktopRenderer : INativeRenderer
    {
        event EventHandler<PropertyChangingEventArgs<NativeWindowTransparency>> TransparencySupportChanging;
        event EventHandler<PropertyChangedEventArgs<NativeWindowTransparency>> TransparencySupportChanged;
        event EventHandler<PropertyChangingEventArgs<NativeWindowDecorations>> SupportedDecorationsChanging;
        event EventHandler<PropertyChangedEventArgs<NativeWindowDecorations>> SupportedDecorationsChanged;
        event EventHandler<PropertyChangingEventArgs<NativeWindowMode>> ModeChanging;
        event EventHandler<PropertyChangedEventArgs<NativeWindowMode>> ModeChanged;
        event EventHandler<PropertyChangedEventArgs<double>> DpiScalingChanged;
        event EventHandler<PropertyChangedEventArgs<double>> OpacityChanged;

        double DpiScaling { get; }
        NativeWindowTransparency TransparencySupport { get; set; }
        NativeWindowDecorations SupportedDecorations { get; set; }
        NativeWindowMode Mode { get; }

        Thickness BorderThickness { get; }
        int TitlebarSize { get; }
        double Opacity { get; set; }

        void EnableWindowDecorations();
        void DisableWindowDecorations();
    }
}
