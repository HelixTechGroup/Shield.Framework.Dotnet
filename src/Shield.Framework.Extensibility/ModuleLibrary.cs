using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Shield.Framework.Extensibility.Collections;
using Shield.Framework.Extensions;
using Shield.Framework.Services.Extensibility;
using Shield.Framework.Services.Extensibility.Exceptions;

namespace Shield.Framework.Extensibility
{
    public class ModuleLibrary : IModuleLibrary
    {
        public event Action<IDispose> OnDispose;        

        private readonly ModuleLibraryItemCollection m_items;
        private bool m_validated;
        private bool m_loaded;
        private bool m_disposed;

        public IEnumerable<IModuleLibraryItem> Items
        {
            get { return m_items; }
        }

        public IEnumerable<ModuleInfo> Modules
        {
            get { return UngroupedModules.Union(Groups.SelectMany(g => g)); }
        }

        public IEnumerable<ModuleInfo> UngroupedModules
        {
            get { return m_items.OfType<ModuleInfo>(); }
        }

        public IEnumerable<ModuleInfoGroup> Groups
        {
            get { return m_items.OfType<ModuleInfoGroup>(); }
        }

        public bool Validated
        {
            get { return m_validated; }
        }

        public bool Loaded
        {
            get { return m_loaded; }
        }

        public bool Disposed
        {
            get { return m_disposed; }
        }

        public ModuleLibrary()
        {
            m_items = new ModuleLibraryItemCollection();
            m_items.CollectionChanged += ItemsCollectionChanged;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleCatalog"/> class while providing an
        /// initial list of <see cref="ModuleInfo"/>s.
        /// </summary>
        /// <param name="modules">The initial list of modules.</param>
        public ModuleLibrary(IEnumerable<ModuleInfo> modules)
            : this()
        {
            if (modules == null)
                throw new ArgumentNullException(nameof(modules));

            foreach (var moduleInfo in modules)
                m_items.Add(moduleInfo);
        }

        ~ModuleLibrary()
        {
            Dispose(false);
        }

        public virtual void Initialize()
        {
            if (!m_loaded)
                Load();

            Validate();
        }

        public virtual void Load()
        {
            m_loaded = true;
        }

        public virtual void Validate()
        {
            ValidateUniqueModules();
            ValidateDependencyGraph();
            ValidateCrossGroupDependencies();
            ValidateDependenciesInitializationMode();

            m_validated = true;
        }

        public void AddModule(ModuleInfo moduleInfo)
        {
            m_items.Add(moduleInfo);
        }

        public void AddModule(Type moduleType, params string[] dependsOn)
        {
            AddModule(moduleType, InitializationMode.WhenAvailable, dependsOn);
        }

        public void AddModule(Type moduleType, InitializationMode initializationMode, params string[] dependsOn)
        {
            if (moduleType == null)
                throw new ArgumentNullException(nameof(moduleType));

            AddModule(moduleType.Name, moduleType.AssemblyQualifiedName, initializationMode, dependsOn);
        }

        public void AddModule(string moduleName, string moduleType, params string[] dependsOn)
        {
            AddModule(moduleName, moduleType, InitializationMode.WhenAvailable, dependsOn);
        }

        public void AddModule(string moduleName, string moduleType, InitializationMode initializationMode, params string[] dependsOn)
        {
            AddModule(moduleName, moduleType, null, initializationMode, dependsOn);
        }

        public void AddModule(string moduleName,
                              string moduleType,
                              string refValue,
                              InitializationMode initializationMode,
                              params string[] dependsOn)
        {
            if (moduleName == null)
                throw new ArgumentNullException(nameof(moduleName));

            if (moduleType == null)
                throw new ArgumentNullException(nameof(moduleType));

            var moduleInfo = new ModuleInfo(moduleName, moduleType);
            moduleInfo.Dependencies.AddRange(dependsOn);
            moduleInfo.InitializationMode = initializationMode;
            moduleInfo.Ref = refValue;
            m_items.Add(moduleInfo);
        }

        public void AddGroup(ModuleInfoGroup group)
        {
            m_items.Add(group);
        }

        public void AddGroup(InitializationMode initializationMode, string refValue, params ModuleInfo[] moduleInfos)
        {
            if (moduleInfos == null)
                throw new ArgumentNullException(nameof(moduleInfos));

            var newGroup = new ModuleInfoGroup
            {
                InitializationMode = initializationMode,
                Ref = refValue
            };

            foreach (var info in moduleInfos)
                newGroup.Add(info);

            this.m_items.Add(newGroup);
        }

        public IEnumerable<ModuleInfo> GetDependentModules(ModuleInfo moduleInfo)
        {
            EnsureCatalogValidated();
            return this.Modules.Where(
                dependentModule => 
                    moduleInfo.Dependencies
                        .Contains(dependentModule.Name));
        }

        public IEnumerable<ModuleInfo> GetModuleDependencies(params ModuleInfo[] modules)
        {
            if (modules == null)
                throw new ArgumentNullException(nameof(modules));

            EnsureCatalogValidated();

            var completeList = new ConcurrentList<ModuleInfo>();
            var pendingList = modules.ToList();
            while (pendingList.Count > 0)
            {
                var moduleInfo = pendingList[0];

                foreach (var dependency in GetDependentModules(moduleInfo))
                    if (!completeList.Contains(dependency) && !pendingList.Contains(dependency))
                        pendingList.Add(dependency);

                pendingList.RemoveAt(0);
                completeList.Add(moduleInfo);
            }

            return Sort(completeList);
        }

        private void ValidateUniqueModules()
        {
            var moduleNames = Modules.Select(m => m.Name).ToList();
            var duplicateModule = moduleNames.FirstOrDefault(
                m => moduleNames.Count(m2 => m2 == m) > 1);

            if (duplicateModule != null)
                throw new DuplicateModuleException(
                    duplicateModule, 
                    string.Format(CultureInfo.CurrentCulture, "A duplicated module with name {0} has been found by the loader.", duplicateModule));
        }

        private void ValidateDependencyGraph()
        {
            SolveDependencies(Modules);
        }

        private void ValidateCrossGroupDependencies()
        {
            ValidateDependencies(UngroupedModules);
            foreach (var group in Groups)
                ValidateDependencies(UngroupedModules.Union(group));
        }

        private void ValidateDependenciesInitializationMode()
        {
            var moduleInfo = this.Modules.FirstOrDefault(
                m =>
                    m.InitializationMode == InitializationMode.WhenAvailable &&
                    GetDependentModules(m)
                        .Any(dependency => dependency.InitializationMode == InitializationMode.OnDemand));

            if (moduleInfo != null)
                throw new ModuleDependencyException(
                    moduleInfo.Name,
                    string.Format(CultureInfo.CurrentCulture, "Module {0} is marked for automatic initialization when the application starts, but it depends on modules that are marked as OnDemand initialization. To fix this error, mark the dependency modules for InitializationMode=WhenAvailable, or remove this validation by extending the ModuleCatalog class.", moduleInfo.Name));
        }

        private void ValidateDependencies(IEnumerable<ModuleInfo> modules)
        {
            if (modules == null)
                throw new ArgumentNullException(nameof(modules));

            var moduleInfos = modules as ModuleInfo[] ?? modules.ToArray();
            var moduleNames = moduleInfos.Select(m => m.Name).ToList();
            foreach (var moduleInfo in moduleInfos)
                if (moduleInfo.Dependencies != null && moduleInfo.Dependencies.Except(moduleNames).Any())
                    throw new ModuleException(
                        moduleInfo.Name,
                        string.Format(CultureInfo.CurrentCulture, "Module {0} depends on other modules that don't belong to the same group.", moduleInfo.Name));
        }

        protected virtual string[] SolveDependencies(IEnumerable<ModuleInfo> modules)
        {
            if (modules == null)
                throw new ArgumentNullException(nameof(modules));

            var solver = new DependencySolver();

            foreach (var data in modules)
            {
                solver.AddModule(data.Name);

                if (data.Dependencies == null)
                    continue;

                foreach (var dependency in data.Dependencies)
                    solver.AddDependency(data.Name, dependency);
            }

            return solver.ModuleCount > 0 ? solver.Solve() : new string[] { };
        }

        private void EnsureCatalogValidated()
        {
            if (!m_validated)
                Validate();
        }

        private IEnumerable<ModuleInfo> Sort(IEnumerable<ModuleInfo> modules)
        {
            var moduleInfos = modules as ModuleInfo[] ?? modules.ToArray();
            foreach (var moduleName in SolveDependencies(moduleInfos))
                yield return moduleInfos.First(m => m.Name == moduleName);
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            EnsureCatalogValidated();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed)
                return;
            
            m_items.Clear();

            OnDispose?.Invoke(this);
            m_disposed = true;
        }        
    }
}