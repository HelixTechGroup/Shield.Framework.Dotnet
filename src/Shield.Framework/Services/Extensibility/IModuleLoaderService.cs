#region Usings
using System;
using System.Collections.Generic;
using Shield.Framework.Services.Extensibility;
#endregion

namespace Shield.Framework.Services
{
    public interface IModuleLoaderService : IApplicationService
    {
        #region Events
        event EventHandler<ModuleDownloadProgressChangedEventArgs> ModuleDownloadProgressChanged;
        event EventHandler<LoadModuleCompletedEventArgs> LoadModuleCompleted;
        #endregion

        #region Methods
        void LoadModule(string moduleName);

        void LoadModule(ModuleInfo module);

        void LoadModules(IEnumerable<ModuleInfo> modules);

        void AddLoader(IModuleLoader loader);
        #endregion
    }
}