#region Usings
using System;
using System.Runtime.ExceptionServices;
using System.Security;
using Shield.Framework.Extensions;
using Shield.Framework.IoC.DependencyInjection;
using Shield.Framework.Services;
using Shield.Framework.Services.Extensibility;

//using CommonServiceLocator;
#endregion

namespace Shield.Framework
{
    public abstract class Application : IDispose
    {
        #region Events
        public event EventHandler Disposed;

        /// <inheritdoc />
        public event EventHandler Disposing;
        #endregion

        #region Members
        protected IContainer m_container;
        protected bool m_isDisposed;
        protected IModuleLibrary m_library;
        protected ILogService m_logger;
        #endregion

        #region Properties
        public bool IsDisposed
        {
            get { return m_isDisposed; }
        }
        #endregion

        ~Application()
        {
            Dispose(false);
        }

        #region Methods
        protected abstract void CreateApplication();

        public virtual void Run()
        {
            RegisterFrameworkExceptionTypes();

            CreateLogger();
            CreateContainer();
            CreateModuleLibrary();
            CreatePlatform();
            CreateApplication();

            ConfigureContainer();
            ConfigureModuleLibrary();
            ConfigurePlatform();

            InitializePlatform();
            InitializeModules();
        }

        protected virtual void CreateLogger()
        {
            //            m_logger = new DefaultPlatformLogProvider();
            //#if DEBUG
            //            m_logger.AddLogger(new ConsoleLogger());
            //#endif
        }

        protected virtual void CreatePlatform() { }

        protected virtual void ConfigurePlatform() { }

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
            //m_container = new DefaultIoCContainer();
        }

        protected virtual void ConfigureContainer()
        {
            //IoCProvider.Container = m_container;
            ////ServiceLocator.SetLocatorProvider(() => m_container.Resolve<IServiceLocator>());

            //m_container.Register(m_logger);
            //m_container.Register(m_library);
            //m_container.Register<IModuleInitializer, ModuleInitializer>();
            //m_container.Register<IModuleManager, ModuleManager>();
            //m_container.Register<IMessageAggregator, MessageAggregator>();
            //m_container.Register<IPlatformEnvironment, PlatformEnvironment>();
            //m_container.Register<IPrivateApplicationFileSystem, PrivateApplicationFileSystem>();

            ////Defaults
            //m_container.Register<IBackgroundDispatcherService, DefaultPlatformBackgroundDispatcher>();
            //m_container.Register<ISynchronizationContextThreadDispatcherService, DefaultPlatformContextDispatcher>();
            //m_container.Register<IPlatformMainDispatcher, DefaultPlatformUiDispatcher>();
        }

        protected virtual void RegisterFrameworkExceptionTypes() { }

        protected virtual void InitializePlatform()
        {
            RegisterApplicationEvents();
        }

        protected virtual void RegisterApplicationEvents()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        protected virtual void InitializeModules()
        {
            //var manager = m_container.Resolve<IModuleManager>();
            //if (manager == null)
            //    throw new InvalidOperationException("Could not resolve IModuleManager");

            //manager.Initialize();
        }

        protected virtual void OnApplicationStartup(object sender, EventArgs e) { }

        protected virtual void OnApplicationExit(object sender, EventArgs e) { }

        [SecurityCritical]
        [HandleProcessCorruptedStateExceptions]
        protected virtual void OnUnhandledException(object sender, EventArgs e) { }

        protected virtual void OnDisposed()
        {
            Disposed.Raise(this, EventArgs.Empty);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_isDisposed)
                return;

            if (!disposing)
                return;

            OnDisposed();
            Disposed.Dispose();
            m_container.Release();
            m_isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}