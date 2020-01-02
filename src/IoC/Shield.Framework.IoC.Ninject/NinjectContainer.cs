#region Usings
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ninject;
using Ninject.Activation.Caching;
using Ninject.Modules;
using Shield.Framework.Collections;
using Shield.Framework.IoC.DependencyInjection;
using Shield.Framework.IoC.Exceptions;
using Shield.Framework.IoC.Extensions;
using Shield.Framework.IoC.Extensions.ChildKernel;
#endregion

namespace Shield.Framework.IoC
{
    public sealed class NinjectContainer : IContainer
    {
        public event Action<IDispose> OnDispose;

        #region Members
        private readonly IKernel m_kernel;        
        private readonly ConcurrentList<IContainer> m_childrenContainers;
        private readonly IContainer m_parentContainer;
        private bool m_disposed;
        #endregion

        public bool Disposed
        {
            get { return m_disposed; }
        }

        public NinjectContainer()
        {
            try
            {
                m_kernel = new StandardKernel();
                m_childrenContainers = new ConcurrentList<IContainer>();
            }
            catch (FileLoadException fex)
            {
                throw new NinjectModuleNotFoundException(string.Format("Could not load module: {0}", fex.FileName));
            }

            m_kernel.RegisterTypeWithValue<IContainer>(this, true);
        }

        private NinjectContainer(NinjectContainer parentContainer)
        {
            try
            {                
                m_kernel = new ChildKernel(parentContainer.m_kernel);
                parentContainer.m_childrenContainers.Add(this);
                m_parentContainer = parentContainer;
                m_childrenContainers = new ConcurrentList<IContainer>();
            }
            catch (FileLoadException fex)
            {
                throw new NinjectModuleNotFoundException(string.Format("Could not load module: {0}", fex.FileName));
            }

            m_kernel.RegisterTypeWithValue<IContainer>(this, true);
        }

        ~NinjectContainer()
        {
            Dispose(false);
        }

        #region Methods
        public IContainer CreateChildContainer()
        {
            return new NinjectContainer(this);
        }

        public void Load(params IBindings[] bindings)
        {
            var modules = bindings.Cast<INinjectModule>();
            m_kernel.Load(modules);
        }

        public void Unload(params IBindings[] bindings)
        {
            var modules = bindings.Cast<INinjectModule>();
            m_kernel.Unload(modules);
        }

        public void Register<T>(T value, bool asSingleton = true,string key = null, bool overrideExisting = false)
        {
            m_kernel.RegisterTypeWithValue<T>(value, asSingleton, key, overrideExisting);            
        }

        public void Register(Type T, bool asSingleton = true, string key = null, bool overrideExisting = false)
        {
            m_kernel.RegisterType(T, asSingleton, key, overrideExisting);
        }

        public void Register<T, C>(C value, bool asSingleton = true, string key = null, bool overrideExisting = false) where C : class, T
        {
            m_kernel.RegisterTypeWithValue<T, C>(value as C, asSingleton, key, overrideExisting);
        }

        public void Register<T, C>(bool asSingleton = true, string key = null, bool overrideExisting = false) where C : class, T
        {
            m_kernel.RegisterType<T, C>(asSingleton, key, overrideExisting);
        }

        public void Register(Type T, Type C, bool asSingleton = true, string key = null, bool overrideExisting = false)
        {
            m_kernel.RegisterType(T, C, asSingleton, key, overrideExisting);
        }

        public void Register(object value, bool asSingleton = true, string key = null, bool overrideExisting = false)
        {
            m_kernel.RegisterTypeWithValue(value.GetType(), value, asSingleton, key, overrideExisting);
        }

        public void Register(Type T, object value, bool asSingleton = true, string key = null, bool overrideExisting = false)
        {
            m_kernel.RegisterTypeWithValue(T, value, asSingleton, key, overrideExisting);
        }

        public void Register<T>(bool asSingleton = true, string key = null, bool overrideExisting = false)
        {
            m_kernel.RegisterType<T>(asSingleton, key, overrideExisting);
        }

        public void Unregister<T>(string key = null)
        {
            Unregister(Resolve<T>(key));
        }

        public void Unregister(Type T, string key = null)
        {
            Unregister(Resolve(T, key));
        }

        public void Unregister(object value)
        {           
            m_kernel.Release(value);
        }

        public void UnregisterAll(Type T)
        {            
            foreach (var i in ResolveAll(T))
                Unregister(i);

            m_kernel.Unbind(T);
        }

        public void UnregisterAll<T>()
        {
            Unregister(typeof(T));
        }

        public T Resolve<T>(string key = null)
        {
            return !string.IsNullOrWhiteSpace(key) ? m_kernel.Get<T>(key) : m_kernel.Get<T>();
        }

        public object Resolve(Type T, string key = null)
        {
            return !string.IsNullOrWhiteSpace(key) ? m_kernel.Get(T, key) : m_kernel.Get(T);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return m_kernel.GetAll<T>();
        }

        public IEnumerable<object> ResolveAll(Type T)
        {
            return m_kernel.GetAll(T);
        }

        public T TryResolve<T>(string key = null)
        {
            return !string.IsNullOrWhiteSpace(key) ? m_kernel.TryGet<T>(key) : m_kernel.TryGet<T>();
        }

        public object TryResolve(Type T, string key = null)
        {
            return !string.IsNullOrWhiteSpace(key) ? m_kernel.TryGet(T, key) : m_kernel.TryGet(T);
        }
        public void Release()
        {
            var mods = m_kernel.GetModules()
                .Where(m => m is IApplicationBindings);

            foreach (var m in mods)
                m_kernel.Unload(m.Name);

            m_kernel.Components.Get<ICache>().Clear();
            m_kernel.Dispose();
        }

        public bool IsRegistered<T>()
        {
            return m_kernel.IsRegistered<T>();
        }

        public bool IsRegistered(Type T)
        {
            return m_kernel.IsRegistered(T);
        }

        private void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            if (disposing)
                Release();

            OnDispose?.Invoke(this);
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