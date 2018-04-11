using System;
using System.Collections.Generic;

namespace Shield.Framework.Extensibility
{
    public interface IModuleManager : IDispose
    {
        event EventHandler<ModuleDownloadProgressChangedEventArgs> ModuleDownloadProgressChanged;
        event EventHandler<LoadModuleCompletedEventArgs> LoadModuleCompleted;

        void Run();

        void LoadModule(string moduleName);

        void LoadModule(ModuleInfo module);

        void LoadModules(IEnumerable<ModuleInfo> modules);

        void AddLoader(IModuleLoader loader);
    }
}