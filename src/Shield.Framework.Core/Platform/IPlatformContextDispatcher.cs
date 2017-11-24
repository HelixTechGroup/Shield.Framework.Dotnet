using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shield.Framework.Platform
{
    public interface IPlatformContextDispatcher : IPlatformDispatcher
    {
        SynchronizationContext PreviousContext { get; }

        SynchronizationContext CurrentContext { get; }

        SynchronizationContext CreateContext();

        void SetContext(SynchronizationContext context);
    }
}
