using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shield.Framework.Extensibility
{
    public sealed class ModuleInfoGroup : IModuleLibraryItem, IList<ModuleInfo>
    {
        private readonly Collection<ModuleInfo> m_modules = new Collection<ModuleInfo>();
        private string m_ref;
        private InitializationMode m_initializationMode;

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

        public int Count
        {
            get { return m_modules.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsSynchronized
        {
            get { return ((ICollection)m_modules).IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return ((ICollection)m_modules).SyncRoot; }
        }

        public ModuleInfo this[int index]
        {
            get { return m_modules[index]; }
            set { m_modules[index] = value; }
        }

        public IEnumerator<ModuleInfo> GetEnumerator()
        {
            return m_modules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ModuleInfo item)
        {
            ForwardValues(item);
            m_modules.Add(item);
        }

        public bool Remove(ModuleInfo item)
        {
            return m_modules.Remove(item);
        }

        public void RemoveAt(int index)
        {
            m_modules.RemoveAt(index);
        }

        public void Insert(int index, ModuleInfo item)
        {
            m_modules.Insert(index, item);
        }

        public void Insert(int index, object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!(value is ModuleInfo moduleInfo))
                throw new ArgumentException("The value must be of type ModuleInfo.", nameof(value));

            m_modules.Insert(index, moduleInfo);
        }

        public void Clear()
        {
            m_modules.Clear();
        }

        public bool Contains(ModuleInfo item)
        {
            return m_modules.Contains(item);
        }

        public int IndexOf(object value)
        {
            return m_modules.IndexOf((ModuleInfo)value);
        }

        public int IndexOf(ModuleInfo item)
        {
            return m_modules.IndexOf(item);
        }

        public void CopyTo(ModuleInfo[] array, int arrayIndex)
        {
            m_modules.CopyTo(array, arrayIndex);
        }

        private void ForwardValues(ModuleInfo moduleInfo)
        {
            if (moduleInfo == null)
                throw new ArgumentNullException(nameof(moduleInfo));

            if (moduleInfo.Ref == null)
                moduleInfo.Ref = m_ref;

            if (moduleInfo.InitializationMode == InitializationMode.WhenAvailable && m_initializationMode != InitializationMode.WhenAvailable)
                moduleInfo.InitializationMode = m_initializationMode;
        }
    }
}