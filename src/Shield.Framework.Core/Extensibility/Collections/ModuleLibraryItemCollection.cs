using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Shield.Framework.Extensions;

namespace Shield.Framework.Extensibility.Collections
{
    internal class ModuleLibraryItemCollection : Collection<IModuleLibraryItem>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected override void InsertItem(int index, IModuleLibraryItem item)
        {
            base.InsertItem(index, item);
            OnNotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        protected void OnNotifyCollectionChanged(NotifyCollectionChangedEventArgs eventArgs)
        {
            CollectionChanged.Raise(this, eventArgs);        
        }
    }
}
