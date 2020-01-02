using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Shield.Framework.Drawing;

namespace Shield.Framework.Platform
{
    public interface INativeRender
    {
        event EventHandler Painting;
        event EventHandler<Rectangle> Paint;
        event EventHandler Painted;
        event EventHandler<Size> Resized;
        event EventHandler<double> ScalingChanged;
        event EventHandler<Point> PositionChanged;

        Size ClientSize { get; }

        double Scaling { get; }

        void Invalidate(Rectangle rect);

        Point PointToClient(Point point);

        Point PointToScreen(Point point);
    }
}
