#region Usings
using System;
#endregion

namespace Shield.Framework.Services.IO.FileSystems
{
    public interface IPhysicalFileSystem : IFileSystem, IEquatable<IPhysicalFileSystem> { }
}