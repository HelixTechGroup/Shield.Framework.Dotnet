using System;
using System.Collections.Generic;

namespace Shield.Framework.Services.Extensibility
{
    public interface IModuleLibrary : IDispose
    {
        IEnumerable<IModuleLibraryItem> Items { get; }

        IEnumerable<ModuleInfo> Modules { get; }

        IEnumerable<ModuleInfo> UngroupedModules { get; }

        IEnumerable<ModuleInfoGroup> Groups { get; }

        bool Validated { get; }

        bool Loaded { get; }

        void Initialize();

        void Load();

        void Validate();

        void AddModule(ModuleInfo moduleInfo);

        void AddModule(Type moduleType, params string[] dependsOn);

        void AddModule(Type moduleType, InitializationMode initializationMode, params string[] dependsOn);

        void AddModule(string moduleName, string moduleType, params string[] dependsOn);

        void AddModule(string moduleName, string moduleType, InitializationMode initializationMode, params string[] dependsOn);

        void AddModule(string moduleName,
                       string moduleType,
                       string refValue,
                       InitializationMode initializationMode,
                       params string[] dependsOn);

        void AddGroup(ModuleInfoGroup group);

        void AddGroup(InitializationMode initializationMode, string refValue, params ModuleInfo[] moduleInfos);

        IEnumerable<ModuleInfo> GetDependentModules(ModuleInfo moduleInfo);

        IEnumerable<ModuleInfo> GetModuleDependencies(params ModuleInfo[] modules);
    }
}
