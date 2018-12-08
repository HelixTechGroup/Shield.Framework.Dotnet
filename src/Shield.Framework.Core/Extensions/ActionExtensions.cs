using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shield.Framework.Platform;

namespace Shield.Framework.Extensions
{
    public static class ActionExtensions
    {
        #region UIThread
        public static void OnUIThread(this Action action, Action callback = null)
        {
            PlatformProvider.Services.Dispatcher.UIDispatcher.Run(action, callback);
        }

        public static Task OnUIThreadAsync(this Action action, Action<Task> callback = null)
        {
            return PlatformProvider.Services.Dispatcher.UIDispatcher.RunAsync(action, callback);
        }        

        public static void OnUIThread<T>(this Action<T> action, T parameter, Action callback = null)
        {
            PlatformProvider.Services.Dispatcher.UIDispatcher.Run(action, parameter, callback);
        }

        public static Task OnUIThreadAsync<T>(this Action<T> action, T parameter, Action<Task> callback = null)
        {
            return PlatformProvider.Services.Dispatcher.UIDispatcher.RunAsync(action, parameter, callback);
        }
        #endregion

        #region BackgroundThread
        public static void OnNewThread(this Action action, Action callback = null)
        {
            PlatformProvider.Services.Dispatcher.BackgroundDispatcher.Run(action, callback);
        }

        public static Task OnNewThreadAsync(this Action action, Action<Task> callback = null)
        {
            return PlatformProvider.Services.Dispatcher.BackgroundDispatcher.RunAsync(action, callback);
        }

        public static void OnNewThread<T>(this Action<T> action, T parameter, Action callback = null)
        {
            PlatformProvider.Services.Dispatcher.BackgroundDispatcher.Run(action, parameter, callback);
        }

        public static Task OnNewThreadAsync<T>(this Action<T> action, T parameter, Action<Task> callback = null)
        {
            return PlatformProvider.Services.Dispatcher.BackgroundDispatcher.RunAsync(action, parameter, callback);
        }
        #endregion
    }
}
