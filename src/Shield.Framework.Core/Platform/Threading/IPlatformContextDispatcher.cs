using System.Threading;

namespace Shield.Framework.Platform.Threading
{
    public interface IPlatformContextDispatcher : IPlatformDispatcher
    {
        SynchronizationContext PreviousContext { get; }

        SynchronizationContext CurrentContext { get; }

        SynchronizationContext CreateContext();

        IPlatformContextDispatcher SetContext(SynchronizationContext context);
    }
}
