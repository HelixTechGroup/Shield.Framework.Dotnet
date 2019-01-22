#region Usings
using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security;
//using CommonServiceLocator;
using Shield.Framework.Extensibility;
using Shield.Framework.IoC;
using Shield.Framework.IoC.Default;
using Shield.Framework.Messaging;
using Shield.Framework.Platform;
using Shield.Framework.Platform.Logging;
using Shield.Framework.Platform.Logging.Default;
using Shield.Framework.Platform.Logging.Loggers;
#endregion

namespace Shield.Framework
{
	public abstract class Bootstrapper : IDispose
	{
		public event Action<IDispose> OnDispose;

		protected bool m_disposed;
		protected IPlatformLogProvider m_logger;
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
			RegisterFrameworkExceptionTypes();

			CreateLogger();
			CreateContainer();
			CreateModuleLibrary();
			CreatePlatform();

			ConfigureContainer();
			ConfigureModuleLibrary();            
			ConfigurePlatform();                                                                                             

			InitializePlatform();
			InitializeModules();            
		}

		protected virtual void CreateLogger()
		{
			m_logger = new DefaultPlatformLogProvider();
#if DEBUG
			m_logger.AddLogger(new ConsoleLogger());
#endif
		}

		protected virtual void CreatePlatform() { }

		protected virtual void ConfigurePlatform()
		{
			PlatformProvider.Services = m_container.Resolve<IPlatformServices>();
			PlatformProvider.Environment = m_container.Resolve<IPlatformEnvironment>();
			PlatformProvider.Environment.DetectPlatform();
		}

		protected virtual void CreateModuleLibrary()
		{
			m_library = new ModuleLibrary();
		}

		protected virtual void ConfigureModuleLibrary()
		{
			var manager = m_container.Resolve<IModuleManager>();
			if (manager == null)
				throw new InvalidOperationException("Could not resolve IModuleManager");

			var loaders = m_container.ResolveAll<IModuleLoader>();
			var moduleLoaders = loaders as IModuleLoader[] ?? loaders.ToArray();
			if (loaders != null && !moduleLoaders.Any())
				throw new InvalidOperationException("Could not resolve IModuleLoader");

			foreach (var loader in moduleLoaders)
				manager.AddLoader(loader);
		}

		protected virtual void CreateContainer()
		{
			m_container = new DefaultIoCContainer();	        
		}

		protected virtual void ConfigureContainer()
		{
			IoCProvider.Container = m_container;
			//ServiceLocator.SetLocatorProvider(() => m_container.Resolve<IServiceLocator>());

			m_container.Register(m_logger);
			m_container.Register(m_library);
			m_container.Register<IModuleInitializer, ModuleInitializer>();
			m_container.Register<IModuleManager, ModuleManager>();
			m_container.Register<IMessageAggregator, MessageAggregator>();
			m_container.Register<IPlatformServices, PlatformServices>();
			m_container.Register<IPlatformEnvironment, PlatformEnvironment>();
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
			var manager = m_container.Resolve<IModuleManager>();
			if (manager == null)
				throw new InvalidOperationException("Could not resolve IModuleManager");

			manager.Run();
		}
		
		protected virtual void OnApplicationStartup(object sender, EventArgs e) { }

		protected virtual void OnApplicationExit(object sender, EventArgs e) { }

		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
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