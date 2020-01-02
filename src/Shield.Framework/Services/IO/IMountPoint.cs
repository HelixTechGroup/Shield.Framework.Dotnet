#region Usings
using System;
#endregion

namespace Shield.Framework.Services.IO
{
    internal interface IMountPoint : IEquatable<IMountPoint>, IDispose
    {
        #region Properties
        string Location { get; }
        IStorageManager Storage { get; }
        #endregion
    }
}