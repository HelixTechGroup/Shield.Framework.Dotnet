namespace Shield.Framework.Platform.Logging
{
    public interface ILogger : IDispose
    {
        void Flush(ILogEntry entry);
    }
}
