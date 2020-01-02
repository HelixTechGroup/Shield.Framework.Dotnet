#region Usings
using System;
#endregion

namespace Shield.Framework.Platform
{
    public interface INativeHandle
    {
        #region Properties
        string HandleDescriptor { get; }
        IntPtr Pointer { get; }
        #endregion
    }
}