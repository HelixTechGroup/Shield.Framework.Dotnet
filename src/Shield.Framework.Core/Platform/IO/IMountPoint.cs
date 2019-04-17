#region Usings
using System;
#endregion

namespace Shield.Framework.Platform.IO
{
    internal interface IMountPoint : IEquatable<IMountPoint>, IDispose
    {
        #region Properties
        string Location { get; }
        IStorageManager Storage { get; }
        #endregion
    }
}