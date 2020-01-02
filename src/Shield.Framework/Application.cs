#region Usings
using System;
using System.IO;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security;
using Shield.Framework.Environment;
using Shield.Framework.IoC.DependencyInjection;
using Shield.Framework.IoC.Native.DependencyInjection;
using Shield.Framework.Platform;
using Shield.Framework.Platform.Logging.Default;
using Shield.Framework.Platform.Logging.Loggers;
using Shield.Framework.Services;
using Shield.Framework.Services.Extensibility;
using Shield.Framework.Services.LifeCycle.Native;

//using CommonServiceLocator;
#endregion

namespace Shield.Framework
{
    public abstract class Application<TApplicationContext> : Disposable where TApplicationContext : class, IApplicationContext
    {
        #region Members
        protected IApplicationContext m_applicationContext;
        protected IContainer m_container;
        protected IApplicationEnvironment m_environment;
        protected IModuleLibrary m_library;
        protected ILogService m_logger;
        #endregion

        #region Methods
        protected abstract void ConfigureApplication();

        protected virtual void ConfigurePlatform() { }

        protected virtual void CreatePlatform()
        {
            m_environment = new ApplicationEnvironment();
            m_environment.DetectPlatform();
        }

        protected virtual void CreateApplication()
        {
            m_container.Register<IApplicationContext, TApplicationContext>();
        }

        public virtual void Run()
        {
            Run(null);
        }

        public virtual void Run(string[] args)
        {
            RegisterFrameworkExceptionTypes();

            CreateLogger();
            CreateContainer();
            CreatePlatform();
            CreateModuleLibrary();
            CreateApplication();

            ConfigureContainer();
            ConfigurePlatform();
            ConfigureModuleLibrary();
            ConfigureApplication();

            InitializePlatform();
            InitializeModules();
            InitializeApplication();

            OnFrameworkInitializationCompleted();
            StartApplication(args);
        }

        protected virtual void InitializeApplication()
        {
            Shield.Initialize(m_container.Resolve<IApplicationContext>());
            RegisterApplicationEvents();
        }

        protected virtual void CreateLogger()
        {
            m_logger = new LogService();
#if DEBUG
            m_logger.AddLogProvider(new ConsoleLogger());
#endif
        }

        protected virtual void CreateModuleLibrary()
        {
            //m_library = new ModuleLibrary();
        }

        protected virtual void ConfigureModuleLibrary()
        {
            //var manager = m_container.Resolve<IModuleManager>();
            //if (manager == null)
            //    throw new InvalidOperationException("Could not resolve IModuleManager");

            //var loaders = m_container.ResolveAll<IModuleLoader>();
            //var moduleLoaders = loaders as IModuleLoader[] ?? loaders.ToArray();
            //if (loaders != null && !moduleLoaders.Any())
            //    throw new InvalidOperationException("Could not resolve IModuleLoader");

            //foreach (var loader in moduleLoaders)
            //    manager.AddLoader(loader);
        }

        protected virtual void CreateContainer()
        {
            LoadAssembliesInDirectory();
            m_container = new IoCContainer();
        }

        protected virtual void ConfigureContainer()
        {
            ////ServiceLocator.SetLocatorProvider(() => m_container.Resolve<IServiceLocator>());

            m_container.Register(m_logger);
            //m_container.Register(m_library);
            //m_container.Register<IModuleInitializer, ModuleInitializer>();
            //m_container.Register<IModuleManager, ModuleManager>();
            //m_container.Register<IMessageAggregator, MessageAggregator>();
            //m_container.Register<IPlatformEnvironment, PlatformEnvironment>();
            //m_container.Register<IPrivateApplicationFileSystem, PrivateApplicationFileSystem>();
            m_container.Register(m_environment);
            m_container.Register<ILifeCycleService, LifeCycleService>();

            ////Defaults
            //m_container.Register<IBackgroundDispatcherService, DefaultPlatformBackgroundDispatcher>();
            //m_container.Register<ISynchronizationContextThreadDispatcherService, DefaultPlatformContextDispatcher>();
            //m_container.Register<IPlatformMainDispatcher, DefaultPlatformUiDispatcher>();
        }

        protected virtual void RegisterFrameworkExceptionTypes() { }

        protected virtual void InitializePlatform()
        {
            PlatformManager.CurrentPlatform.Initialize();
        }

        protected virtual void RegisterApplicationEvents()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        protected virtual void InitializeModules()
        {
            //var manager = m_container.Resolve<IModuleManager>();
            //if (manager == null)
            //    throw new InvalidOperationException("Could not resolve IModuleManager");

            //manager.Initialize();
        }

        protected virtual void StartApplication(string[] args)
        {
            m_container.Resolve<ILifeCycleService>().Start(args);
        }

        protected virtual void StopApplication()
        {
            m_container.Resolve<ILifeCycleService>().Shutdown();
        }

        public virtual void OnFrameworkInitializationCompleted() { }

        protected virtual void OnApplicationStartup(object sender, EventArgs e) { }

        protected virtual void OnApplicationExit(object sender, EventArgs e) { }

        [SecurityCritical]
        [HandleProcessCorruptedStateExceptions]
        protected virtual void OnUnhandledException(object sender, EventArgs e) { StopApplication(); }

        /// <inheritdoc />
        protected override void OnDisposed(object sender, EventArgs e)
        {
            m_container.Release();
            base.OnDisposed(sender, e);
        }

        private Assembly OnAssemblyResolve(object sender, ResolveEventArgs e)
        {
            Assembly assembly = null;
            try
            {
                assembly = Assembly.Load(e.Name);
            }
            catch
            {
                string[] Parts = e.Name.Split(',');
                string File = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + Parts[0].Trim() + ".dll";

                assembly = Assembly.LoadFrom(File);
            }

            return assembly;
        }

        private void LoadAssembliesInDirectory()
        {
            var location = Assembly.GetEntryAssembly().Location;
            if (string.IsNullOrWhiteSpace(location))
                return;
            var dir = new FileInfo(location).Directory;
            if (dir == null)
                return;
            foreach (var file in dir.EnumerateFiles("*.dll"))
            {
                try
                {
                    Assembly.LoadFile(file.FullName);
                }
                catch (Exception) { }
            }
        }
        #endregion
    }
}