#region Header
// solution:Shield.Framework
// project:	Shield.Framework.Core
// file:	Platform\IO\IStorageProvider.cs
// summary:	Declares the IStorageProvider interface
//			Copyright (c) 2017 HelixDesign, llc. All rights reserved.
#endregion

using System.Collections.Generic;

namespace Shield.Framework.Platform.IO
{
    /// <summary>Interface for storage provider.</summary>
    public interface IPlatformStorageProvider : IPlatformStorageCollection
    {
        /// <summary>Gets the Isolated user data storage which is local to the current device.</summary>
        /// <value>The private user storage.</value>
        IPlatformStorage PrivateApplicationStorage { get; }

        /// <summary>Gets the user data storage which may be synced with other devices for the same user.</summary>
        /// <value>The roaming user storage.</value>
        IPlatformStorage RoamingApplicationStorage { get; }

        /// <summary>Gets the application data storage which is local to the current application.</summary>
        /// <value>The local application storage.</value>
        IPlatformStorage LocalApplicationStorage { get; }       
    }
}