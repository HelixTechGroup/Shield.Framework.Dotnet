#region Usings
#endregion

namespace Shield.Framework.Services.Logging
{
    public interface ILogProvider : IDispose
    {
        #region Methods
        void Flush(ILogEntry entry);
        #endregion
    }
}