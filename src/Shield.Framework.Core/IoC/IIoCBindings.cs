namespace Shield.Framework.IoC
{
    /// <summary>Interface for IoC(Inversion of Control) bindings.</summary>
    public interface IIoCBindings
    {
        #region Methods
        /// <summary>Loads this object.</summary>
        void Load();

        void Unload();
        #endregion
    }
}