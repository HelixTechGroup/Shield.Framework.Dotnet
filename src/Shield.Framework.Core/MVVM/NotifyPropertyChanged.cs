#region Usings
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Shield.Framework.Extensions;
#endregion

namespace Shield.Framework.MVVM
{
    public abstract class NotifyPropertyChanged : INotifyPropertyChangedExtended
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsNotifying { get; set; }

        protected NotifyPropertyChanged()
        {
            IsNotifying = true;
        }

        public virtual void NotifyOfPropertyChange([CallerMemberName] string propertyName = null)
        {
            if (!IsNotifying)
                return;
            ((Action)(
                () => OnPropertyChanged(new PropertyChangedEventArgs(propertyName))))
                .OnUiThread();
        }

        public virtual void Refresh()
        {
            NotifyOfPropertyChange(string.Empty);
        }

        public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            if (property != null)
                NotifyOfPropertyChange(property.GetMemberInfo().Name);
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged.Raise(this, e);
        }
    }
}