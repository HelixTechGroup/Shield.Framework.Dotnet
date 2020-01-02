#region Usings
using System;
using Shield.Framework.Platform.Services.IO;
#endregion

namespace Shield.Framework.Platform.IO.Managers
{
    public interface ILocalApplicationStorageManager : IStorageManager, IEquatable<ILocalApplicationStorageManager> { }
}