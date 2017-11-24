namespace Shield.Framework.MVVM
{
    public interface IViewModel : INotifyPropertyChangedExtended
    {
        IView View { get; }
    }
}