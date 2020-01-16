using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Shield.Framework.Drawing;

namespace Shield.Framework.Platform
{
    public interface INativeRenderer
    {
        event EventHandler Painting;
        event EventHandler<Rectangle> Paint;
        event EventHandler Painted;

        INativeScreen Screen { get; }
        Size VirutalSize { get; }
        float AspectRatio { get; }

        bool Invalidate();
        bool Validate();

    }
}
