using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shield.Framework.Services.Extensibility
{
    public sealed class ModuleInfo : IModuleLibraryItem
    {
        private string m_name;
        private string m_type;
        private Collection<string> m_dependencies;
        private InitializationMode m_initializationMode;
        private string m_ref;
        private ModuleState m_state;

        public string Name
        {
            get { return m_name; }
        }

        public string Type
        {
            get { return m_type; }
        }

        public ICollection<string> Dependencies
        {
            get { return m_dependencies; }
        }

        public InitializationMode InitializationMode
        {
            get { return m_initializationMode; }
            set { m_initializationMode = value; }
        }

        public string Ref
        {
            get { return m_ref; }
            set { m_ref = value; }
        }

        public ModuleState State
        {
            get { return m_state; }
            set { m_state = value; }
        }

        public ModuleInfo()
            : this(null, null, new string[0])
        {
        }

        public ModuleInfo(string name, string type) : this(name, type, new string[0])
        {
        }

        public ModuleInfo(string name, string type, params string[] dependsOn)
        {
            if (dependsOn == null)
                throw new ArgumentNullException(nameof(dependsOn));

            m_name = name;
            m_type = type ;
            m_dependencies = new Collection<string>();
            foreach (var dependency in dependsOn)
                m_dependencies.Add(dependency);
        }
    }
}