#region Usings
using System;
#endregion

namespace Shield.Framework.Extensibility
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ModuleNameAttribute : Attribute
    {
        #region Properties
        public string Name { get; set; }
        #endregion

        public ModuleNameAttribute(string name)
        {
            Name = name;
        }
    }
}