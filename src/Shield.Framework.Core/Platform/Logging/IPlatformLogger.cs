namespace Shield.Framework.Platform.Logging
{
    public interface IPlatformLogger : IDispose
    {
        void Flush(IPlatformLogEntry entry);
    }
}
