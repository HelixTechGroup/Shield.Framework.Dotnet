using Shield.Framework.Platform.IO;
using Shield.Framework.Platform.Logging;
using Shield.Framework.Platform.Threading;

namespace Shield.Framework.Platform
{
	public interface IPlatformServices
	{		
		IPlatformLogProvider Logger { get; }

		IPlatformDispatcherProvider Dispatcher { get; }

		IPlatformStorageProvider Storage { get; }
	}
}