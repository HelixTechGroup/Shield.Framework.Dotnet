using System;
using System.Collections.Generic;
using System.Text;

namespace Shield.Framework.Platform
{
    public interface INativeObject
    {
        INativeHandle Handle { get; }
    }
}
