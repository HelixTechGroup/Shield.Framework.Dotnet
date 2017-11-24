#region Usings
using System;
#endregion

namespace Shield.Framework.Extensibility
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ModuleDependencyAttribute : Attribute
    {
        #region Properties
        public string ModuleName { get; }
        #endregion

        public ModuleDependencyAttribute(string moduleName)
        {
            ModuleName = moduleName;
        }
    }
}