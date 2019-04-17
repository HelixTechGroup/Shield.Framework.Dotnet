using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Shield.Framework.Extensibility.Exceptions;
using Shield.Framework.Extensions;
using Shield.Framework.IoC;
using Shield.Framework.Platform.Logging;

namespace Shield.Framework.Extensibility
{
    public sealed class ModuleManager : IModuleManager
    {
        public event EventHandler<ModuleDownloadProgressChangedEventArgs> ModuleDownloadProgressChanged;
        public event EventHandler<LoadModuleCompletedEventArgs> LoadModuleCompleted;
        public event Action<IDispose> OnDispose;

        private readonly IModuleLibrary m_library;
        private readonly IModuleInitializer m_moduleInitializer;
        private readonly IPlatformLogProvider m_logger;
        private IList<IModuleLoader> m_loaders;
        private HashSet<IModuleLoader> m_subscribedLoaders;
        private bool m_disposed;

        public bool Disposed
        {
            get { return m_disposed; }
        }

        public ModuleManager(/*IModuleLoader[] loaders,*/ IModuleInitializer moduleInitializer, IModuleLibrary moduleCatalog, IPlatformLogProvider logger)
        {
            if (moduleInitializer == null)
                throw new ArgumentNullException(nameof(moduleInitializer));

            if (moduleCatalog == null)
                throw new ArgumentNullException(nameof(moduleCatalog));

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            m_moduleInitializer = moduleInitializer;
            m_library = moduleCatalog;
            m_logger = logger;
            m_subscribedLoaders = new HashSet<IModuleLoader>();
            m_loaders = new List<IModuleLoader>(/*loaders*/);
        }

        ~ModuleManager()
        {
            Dispose(false);
        }

        public void Run()
        {
            m_library.Initialize();
            LoadModulesWhenAvailable();
        }

        public void LoadModule(string moduleName)
        {
            var modules = m_library.Modules.Where(m => m.Name == moduleName);
            var moduleInfos = modules as ModuleInfo[] ?? modules.ToArray();
            if (modules == null || !moduleInfos.Any())
                throw new ModuleNotFoundException(moduleName, string.Format("Module {0} was not found in the catalog.", moduleName));

            if (moduleInfos.Count() != 1)
                throw new DuplicateModuleException(moduleName, string.Format("A duplicated module with name {0} has been found in the catalog.", moduleName));

            var modulesToLoad = m_library.GetModuleDependencies(moduleInfos);
            LoadModules(modulesToLoad);
        }

        public void LoadModule(ModuleInfo module)
        {
            if  (module == null)
                throw new ArgumentNullException(nameof(module));

            if (string.IsNullOrWhiteSpace(module.Name))
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture,
                                  "The provided String argument {0} must not be null or empty.",
                                  nameof(module.Name)), nameof(module));

            var modules = m_library.Modules.Where(m => m.Name == module.Name);
            var moduleInfos = modules as ModuleInfo[] ?? modules.ToArray();
            if (modules == null || !moduleInfos.Any())
                throw new ModuleNotFoundException(module.Name, string.Format("Module {0} was not found in the catalog.", module.Name));

            if (moduleInfos.Count() != 1)
                throw new DuplicateModuleException(module.Name, string.Format("A duplicated module with name {0} has been found in the catalog.", module.Name));

            var modulesToLoad = m_library.GetModuleDependencies(moduleInfos);
            LoadModules(modulesToLoad);
        }

        public void LoadModules(IEnumerable<ModuleInfo> modules)
        {
            if (modules == null)
                return;

            foreach (var moduleInfo in modules)
            {
                if (ModuleNeedsRetrieval(moduleInfo))
                    BeginRetrievingModule(moduleInfo);
                else
                    moduleInfo.State = ModuleState.ReadyForInitialization;
            }

            LoadModulesThatAreReadyForLoad();
        }

        public void AddLoader(IModuleLoader loader)
        {
            if (loader == null)
                throw new ArgumentNullException(nameof(loader));

            m_loaders.Add(loader);
        }
    
        private void LoadModulesWhenAvailable()
        {
            var whenAvailableModules = m_library.Modules.Where(m => m.InitializationMode == InitializationMode.WhenAvailable).ToArray();
            var modulesToLoad = m_library.GetModuleDependencies(whenAvailableModules);
            LoadModules(modulesToLoad);
        }                

        private bool ModuleNeedsRetrieval(ModuleInfo moduleInfo)
        {
            if (moduleInfo == null)
                throw new ArgumentNullException(nameof(moduleInfo));

            if (moduleInfo.State != ModuleState.NotStarted)
                return false;

            // If we can instantiate the type, that means the module's assembly is already loaded into
            // the AppDomain and we don't need to retrieve it.
            return Type.GetType(moduleInfo.Type) != null;
        }

        private void BeginRetrievingModule(ModuleInfo moduleInfo)
        {
            var moduleInfoToLoadType = moduleInfo;
            var moduleLoader = GetLoaderForModule(moduleInfoToLoadType);
            moduleInfoToLoadType.State = ModuleState.LoadingTypes;

            // Delegate += works differently between SL and WPF.
            // We only want to subscribe to each instance once.
            if (!m_subscribedLoaders.Contains(moduleLoader))
            {
                moduleLoader.ModuleDownloadProgressChanged += moduleLoader_ModuleDownloadProgressChanged;
                moduleLoader.LoadModuleCompleted += moduleLoader_LoadModuleCompleted;
                m_subscribedLoaders.Add(moduleLoader);
            }

            moduleLoader.LoadModule(moduleInfo);
        }

        private void LoadModulesThatAreReadyForLoad()
        {
            var keepLoading = true;
            while (keepLoading)
            {
                keepLoading = false;
                var availableModules = m_library.Modules.Where(m => m.State == ModuleState.ReadyForInitialization);

                foreach (var moduleInfo in availableModules)
                {
                    if ((moduleInfo.State == ModuleState.Initialized) 
                        || (!AreDependenciesLoaded(moduleInfo)))
                        continue;

                    moduleInfo.State = ModuleState.Initializing;
                    InitializeModule(moduleInfo);
                    keepLoading = true;
                    break;
                }
            }
        }

        private void moduleLoader_ModuleDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        {
            RaiseModuleDownloadProgressChanged(e);
        }

        private void moduleLoader_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if ((e.ModuleInfo.State != ModuleState.Initializing) 
                    && (e.ModuleInfo.State != ModuleState.Initialized))
                    e.ModuleInfo.State = ModuleState.ReadyForInitialization;

                // This callback may call back on the UI thread, but we are not guaranteeing it.
                // If you were to add a custom retriever that retrieved in the background, you
                // would need to consider dispatching to the UI thread.
                LoadModulesThatAreReadyForLoad();
            }
            else
            {
                RaiseLoadModuleCompleted(e);

                // If the error is not handled then I log it and raise an exception.
                if (!e.IsErrorHandled)
                    HandleModuleLoadingError(e.ModuleInfo, e.Error);
            }
        }

        private void HandleModuleLoadingError(ModuleInfo moduleInfo, Exception exception)
        {
            if (moduleInfo == null)
                throw new ArgumentNullException(nameof(moduleInfo));

            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            var moduleLoadingException = exception as ModuleLoadingException 
                ?? new ModuleLoadingException(moduleInfo.Name, exception.Message, exception);

            m_logger.Log(moduleLoadingException.Message, PlatformLogCategory.Exception, PlatformLogPriority.High);

            throw moduleLoadingException;
        }

        private bool AreDependenciesLoaded(ModuleInfo moduleInfo)
        {
            var requiredModules = m_library.GetDependentModules(moduleInfo);
            if (requiredModules == null)
                return true;

            var notReadyRequiredModuleCount =
                requiredModules.Count(
                    requiredModule => 
                    requiredModule.State != ModuleState.Initialized);

            return notReadyRequiredModuleCount == 0;
        }

        private IModuleLoader GetLoaderForModule(ModuleInfo moduleInfo)
        {
            foreach (var loader in m_loaders)
                if (loader.CanLoadModule(moduleInfo))
                    return loader;

            throw new ModuleLoaderNotFoundException(moduleInfo.Name, string.Format(CultureInfo.CurrentCulture, 
                "There is currently no moduleTypeLoader in the ModuleManager that can retrieve the specified module.", moduleInfo.Name), null);
        }

        private void InitializeModule(ModuleInfo moduleInfo)
        {
            if (moduleInfo.State != ModuleState.Initializing)
                return;

            m_moduleInitializer.Initialize(moduleInfo);
            moduleInfo.State = ModuleState.Initialized;
            RaiseLoadModuleCompleted(moduleInfo, null);
        }

        private void RaiseModuleDownloadProgressChanged(ModuleDownloadProgressChangedEventArgs e)
        {
            ModuleDownloadProgressChanged.Raise(this, e);
        }

        private void RaiseLoadModuleCompleted(ModuleInfo moduleInfo, Exception error)
        {
            RaiseLoadModuleCompleted(new LoadModuleCompletedEventArgs(moduleInfo, error));
        }

        private void RaiseLoadModuleCompleted(LoadModuleCompletedEventArgs e)
        {
            LoadModuleCompleted.Raise(this, e);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (m_disposed)
                return;
            
            foreach (var loader in m_loaders)
                    loader.Dispose();

            OnDispose?.Invoke(this);
            m_disposed = true;
        }
    }
}