#region Usings
using System;
using Patchwork.Framework;
using Patchwork.Framework.Environment;
using Shield.Framework.Services;
using Shin.Framework;
using Shin.Framework.IoC.DependencyInjection;
#endregion

namespace Shield.Framework
{
    public class ApplicationContext : Initializable, IApplicationContext
    {
        #region Members
        private readonly IContainer m_container;
        private readonly IApplicationContext m_current;
        private ILogger m_log;
        //private IMessageAggregator m_messageAggregator;
        private IModuleLoaderService m_module;
        private IThreadService m_thread;
        #endregion

        #region Properties
        /// <inheritdoc />
        public IContainer Container
        {
            get { return m_container; }
        }

        /// <inheritdoc />
        public IApplicationContext Current
        {
            get { return m_current; }
        }

        /// <inheritdoc />
        public IApplicationEnvironment Environment
        {
            get { return PlatformManager.Environment; }
        }

        /// <inheritdoc />
        public ILifeCycleService LifeCycle
        {
            get { return m_container.Resolve<ILifeCycleService>(); }
        }

        /// <inheritdoc />
        public ILogger Log
        {
            get { return m_container.Resolve<ILogger>(); }
        }

        /// <inheritdoc />
        //public IMessageAggregator MessageAggregator
        //{
        //    get { return m_container.Resolve<IMessageAggregator>(); }
        //}

        /// <inheritdoc />
        public IModuleLoaderService Module
        {
            get { return m_module; }
        }

        /// <inheritdoc />
        public IThreadService Thread
        {
            get { return m_container.Resolve<IThreadService>(); }
        }
        #endregion

        public ApplicationContext(IContainer container)
        {
            m_container = container;
            m_current = this;
        }
    }
}