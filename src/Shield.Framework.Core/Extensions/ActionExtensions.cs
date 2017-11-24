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
            PlatformProvider.UIDispatcher.Run(action, callback);
        }

        public static Task OnUIThreadAsync(this Action action, Action<Task> callback = null)
        {
            return PlatformProvider.UIDispatcher.RunAsync(action, callback);
        }        

        public static void OnUIThread<T>(this Action<T> action, T parameter, Action callback = null)
        {
            PlatformProvider.UIDispatcher.Run(action, parameter, callback);
        }

        public static Task OnUIThreadAsync<T>(this Action<T> action, T parameter, Action<Task> callback = null)
        {
            return PlatformProvider.UIDispatcher.RunAsync(action, parameter, callback);
        }
        #endregion

        #region BackgroundThread
        public static void OnNewThread(this Action action, Action callback = null)
        {
            PlatformProvider.BackgroundDispatcher.Run(action, callback);
        }

        public static Task OnNewThreadAsync(this Action action, Action<Task> callback = null)
        {
            return PlatformProvider.BackgroundDispatcher.RunAsync(action, callback);
        }

        public static void OnNewThread<T>(this Action<T> action, T parameter, Action callback = null)
        {
            PlatformProvider.BackgroundDispatcher.Run(action, parameter, callback);
        }

        public static Task OnNewThreadAsync<T>(this Action<T> action, T parameter, Action<Task> callback = null)
        {
            return PlatformProvider.BackgroundDispatcher.RunAsync(action, parameter, callback);
        }
        #endregion
    }
}
