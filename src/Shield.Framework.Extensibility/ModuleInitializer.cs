#region Usings
using System;
using System.Globalization;
using Shield.Framework.IoC.DependencyInjection;
using Shield.Framework.Services.Extensibility;
#endregion

namespace Shield.Framework.Extensibility
{
    public sealed class ModuleInitializer : IModuleInitializer
    {
        #region Members
        private readonly IContainer m_container;
        #endregion

        public ModuleInitializer(IContainer container)
        {
            m_container = container;
        }

        #region Methods
        public void Initialize(ModuleInfo moduleInfo)
        {
            var moduleType = Type.GetType(moduleInfo.Type);
            if (moduleType == null)
            {
                throw new ModuleInitializeException(string.Format(CultureInfo.CurrentCulture,
                                                                  "Unable to retrieve the module type {0} from the loaded assemblies.  You may need to specify a more fully-qualified type name.",
                                                                  moduleInfo.Type));
            }

            var module = CreateModule(moduleType);
            if (module != null)
                module.Initialize();
        }

        private IModule CreateModule(Type moduleType)
        {
            return (IModule)m_container.Resolve(moduleType);
        }
        #endregion
    }
}