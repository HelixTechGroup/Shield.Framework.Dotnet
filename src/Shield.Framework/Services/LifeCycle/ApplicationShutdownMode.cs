namespace Shield.Framework.Services.LifeCycle
{
    public enum ApplicationShutdownMode
    {
        /// <summary>
        /// Indicates an implicit call to Application.Shutdown when the last window closes.
        /// </summary>
        OnLastWindowClose,

        /// <summary>
        /// Indicates an implicit call to Application.Shutdown when the main window closes.
        /// </summary>
        OnMainWindowClose,

        /// <summary>
        /// Indicates that the application only exits on an explicit call to Application.Shutdown.
        /// </summary>
        OnExplicitShutdown
    }
}