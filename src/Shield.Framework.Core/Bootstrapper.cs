#region Usings
using System;
using Microsoft.Practices.ServiceLocation;
using Shield.Framework.Extensibility;
using Shield.Framework.IoC;
using Shield.Framework.IoC.Default;
using Shield.Framework.Logging;
using Shield.Framework.Logging.Providers;
using Shield.Framework.Messaging;
#endregion

namespace Shield.Framework
{
    public abstract class Bootstrapper : IDispose
    {
        public event Action<IDispose> OnDispose;

        protected bool m_disposed;
        protected ILogger m_logger;
        protected IModuleLibrary m_library;
        protected IIoCContainer m_container;

        public bool Disposed
        {
            get { return m_disposed; }
        }

        ~Bootstrapper()
        {
            Dispose(false);
        }

        #region Methods        
        public virtual void Run()
        {
            CreateLogger();

            CreatePlatform();
            ConfigurePlatform();            

            CreateModuleLibrary();
            ConfigureModuleLibrary();

            CreateContainer();
            ConfigureContainer();
            ConfigureServiceLocator();

            RegisterFrameworkExceptionTypes();

            InitializePlatform();
            InitializeModules();            
        }

        protected virtual void CreateLogger()
        {
            m_logger = new DefaultLogger();
#if DEBUG
            m_logger.AddProvider(new ConsoleLogProvider());
#endif
        }

        protected abstract void CreatePlatform();

        protected abstract void ConfigurePlatform();

        protected virtual void CreateModuleLibrary()
        {
        }

        protected virtual void ConfigureModuleLibrary() { }

        protected virtual void CreateContainer()
        {
            m_container = new DefaultIoCContainer();
        }

        protected virtual void ConfigureContainer()
        {
            m_container.Register(m_logger);
            m_container.Register<IMessageAggregator, MessageAggregator>();
        }

        protected virtual void ConfigureServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => m_container.Resolve<IServiceLocator>());
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

        protected virtual void InitializeModules() { }

        protected virtual void OnApplicationStartup(object sender, EventArgs e) { }

        protected virtual void OnApplicationExit(object sender, EventArgs e) { }

        protected virtual void OnUnhandledException(object sender, EventArgs e) { }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            m_container.Release();

            if (OnDispose != null)
                OnDispose(this);
            m_disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}