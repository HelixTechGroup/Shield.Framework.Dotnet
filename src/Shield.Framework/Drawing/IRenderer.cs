using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Shield.Framework.Drawing
{
    public interface IRenderer
    {
        void Paint(Rectangle rect);
    }
}
