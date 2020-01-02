using System;

namespace Shield.Framework.Services.Extensibility
{
    public interface IModuleLoader : IDispose
    {
        event EventHandler<ModuleDownloadProgressChangedEventArgs> ModuleDownloadProgressChanged;
        event EventHandler<LoadModuleCompletedEventArgs> LoadModuleCompleted;

        bool CanLoadModule(ModuleInfo moduleInfo);

        void LoadModule(ModuleInfo moduleInfo);        
    }
}