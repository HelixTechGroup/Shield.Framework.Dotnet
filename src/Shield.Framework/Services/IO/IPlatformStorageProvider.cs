#region Header
// solution:Shield.Framework
// project:	Shield.Framework.Core
// file:	Platform\IO\IStorageProvider.cs
// summary:	Declares the IStorageProvider interface
//			Copyright (c) 2017 HelixDesign, llc. All rights reserved.
#endregion

#region Usings
using Shield.Framework.Services.IO.Collections;
using Shield.Framework.Services.IO.FileSystems;
#endregion

namespace Shield.Framework.Services.IO
{
    /// <summary>Interface for storage provider.</summary>
    public interface IPlatformStorageProvider : IMountPointCollection
    {
        #region Properties
        /// <summary>Gets the application data storage which is local to the current application.</summary>
        /// <value>The local application storage.</value>
        IStorageManager<ILocalApplicationFileSystem> Local { get; }

        /// <summary>Gets the Isolated user data storage which is local to the current device.</summary>
        /// <value>The private user storage.</value>
        IStorageManager<IPrivateApplicationFileSystem> Private { get; }

        /// <summary>Gets the user data storage which may be synced with other devices for the same user.</summary>
        /// <value>The roaming user storage.</value>
        IStorageManager Roaming { get; }
        #endregion
    }
}