#region Usings
using System.ComponentModel;
#endregion

namespace Shield.Framework.MVVM
{
    public interface INotifyPropertyChangedExtended : INotifyPropertyChanged
    {
        #region Properties
        bool IsNotifying { get; set; }
        #endregion

        #region Methods
        void NotifyOfPropertyChange(string propertyName);

        void Refresh();
        #endregion
    }
}