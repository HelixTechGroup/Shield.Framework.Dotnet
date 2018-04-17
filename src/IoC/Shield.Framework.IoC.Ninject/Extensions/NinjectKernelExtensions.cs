using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace Shield.Framework.IoC.Extensions
{
    public static class NinjectKernelExtensions
    {
        public static bool IsRegistered<TService>(this IKernel kernel)
        {
            return kernel.IsRegistered(typeof(TService));
        }

        public static bool IsRegistered(this IKernel kernel, Type type)
        {
            return kernel.CanResolve(kernel.CreateRequest(type, _ => true, new IParameter[] { }, false, false));
        }

        public static void RegisterType<TFrom, TTo>(this IKernel kernel, bool asSingleton, string key = null, bool overrideExisting = false)
        {
            kernel.RegisterType(typeof(TFrom), typeof(TTo), asSingleton, key, overrideExisting);
        }

        public static void RegisterType(this IKernel kernel,
                                        Type from,
                                        Type to,
                                        bool asSingleton,
                                        string key = null,
                                        bool overrideExisting = false)
        {
            // Don't do anything if there are already bindings registered
            if (kernel.IsRegistered(from) && !overrideExisting)
                return;

            // Register the types
            var binding = kernel.Rebind(from).To(to);
            if (asSingleton)
                binding.InSingletonScope();
            else
                binding.InTransientScope();

            if (!string.IsNullOrWhiteSpace(key))
                binding.Named(key);
        }

        public static void RegisterType<TFrom>(this IKernel kernel, bool asSingleton, string key = null, bool overrideExisting = false)
        {
            kernel.RegisterType(typeof(TFrom), asSingleton, key, overrideExisting);
        }

        public static void RegisterType(this IKernel kernel, Type TFrom, bool asSingleton, string key = null, bool overrideExisting = false)
        {
            // Don't do anything if there are already bindings registered
            if (kernel.IsRegistered(TFrom) && !overrideExisting)
                return;

            // Register the types
            var binding = kernel.Rebind(TFrom).ToSelf();
            if (asSingleton)
                binding.InSingletonScope();
            else
                binding.InTransientScope();

            if (!string.IsNullOrWhiteSpace(key))
                binding.Named(key);
        }

        public static void RegisterTypeWithValue<TFrom>(this IKernel kernel,
                                                        object value,
                                                        bool asSingleton,
                                                        string key = null,
                                                        bool overrideExisting = false)
        {
            kernel.RegisterTypeWithValue(typeof(TFrom), value, asSingleton, key, overrideExisting);
        }

        public static void RegisterTypeWithValue(this IKernel kernel,
                                                 Type from,
                                                 object value,
                                                 bool asSingleton,
                                                 string key = null,
                                                 bool overrideExisting = false)
        {
            // Don't do anything if there are already bindings registered
            if (kernel.IsRegistered(from) && !overrideExisting)
                return;

            // Register the types
            var binding = kernel.Rebind(from).ToConstant(value);
            if (asSingleton)
                binding.InSingletonScope();
            else
                binding.InTransientScope();

            if (!string.IsNullOrWhiteSpace(key))
                binding.Named(key);
        }

        public static void RegisterTypeWithValue<TFrom, TTo>(this IKernel kernel,
                                                             TTo value,
                                                             bool asSingleton,
                                                             string key = null,
                                                             bool overrideExisting = false)
        {
            kernel.RegisterTypeWithValue(typeof(TFrom), typeof(TTo), value, asSingleton, key, overrideExisting);
        }

        public static void RegisterTypeWithValue(this IKernel kernel,
                                        Type from,
                                        Type to,
                                        object value,
                                        bool asSingleton,
                                        string key = null,
                                        bool overrideExisting = false)
        {
            // Don't do anything if there are already bindings registered
            if (kernel.IsRegistered(from) && !overrideExisting)
                return;

            // Register the types
            var binding = kernel.Rebind(from).ToConstant(value);
            if (asSingleton)
                binding.InSingletonScope();
            else
                binding.InTransientScope();

            if (!string.IsNullOrWhiteSpace(key))
                binding.Named(key);
        }

        public static void Unload(this IKernel kernel, IEnumerable<INinjectModule> modules)
        {
            foreach (INinjectModule module in modules)
                kernel.Unload(module.Name);
        }
    }
}
