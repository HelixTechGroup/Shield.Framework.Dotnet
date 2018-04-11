using System;
using System.Globalization;
using Shield.Framework.Extensibility.Exceptions;
using Shield.Framework.IoC;

namespace Shield.Framework.Extensibility
{
    public sealed class ModuleInitializer : IModuleInitializer
    {
        private readonly IIoCContainer m_container;

        public ModuleInitializer(IIoCContainer container)
        {
            m_container = container;
        }

        public void Initialize(ModuleInfo moduleInfo)
        {
            var moduleType = Type.GetType(moduleInfo.Type);
            if (moduleType == null)
                throw new ModuleInitializeException(string.Format(CultureInfo.CurrentCulture, 
                                                                  "Unable to retrieve the module type {0} from the loaded assemblies.  You may need to specify a more fully-qualified type name.", moduleInfo.Type));

            var module = CreateModule(moduleType);
            if (module != null)
                module.Initialize();
        }

        private IModule CreateModule(Type moduleType)
        {
            return (IModule)m_container.Resolve(moduleType);
        }
    }
}