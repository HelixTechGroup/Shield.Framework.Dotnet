#region Usings
using System;
using System.Threading;
using System.Threading.Tasks;
using Shield.Framework.Platform.Threading;
#endregion

namespace Shield.Framework.Extensions
{
    public static class ActionExtensions
    {
        #region UIThread
        public static void OnUiThread(this Action action, Action callback = null)
        {
            IoCProvider.Container.Resolve<IPlatformUiDispatcher>().Run(action, callback);
        }

        public static Task OnUiThreadAsync(this Action action, Action<Task> callback = null)
        {
            return IoCProvider.Container.Resolve<IPlatformUiDispatcher>().RunAsync(action, callback);
        }

        public static void OnUiThread<T>(this Action<T> action, T parameter, Action callback = null)
        {
            IoCProvider.Container.Resolve<IPlatformUiDispatcher>().Run(action, parameter, callback);
        }

        public static Task OnUiThreadAsync<T>(this Action<T> action, T parameter, Action<Task> callback = null)
        {
            return IoCProvider.Container.Resolve<IPlatformUiDispatcher>().RunAsync(action, parameter, callback);
        }
        #endregion

        #region BackgroundThread
        public static void OnNewThread(this Action action, Action callback = null)
        {
            IoCProvider.Container.Resolve<IPlatformBackgroundDispatcher>().Run(action, callback);
        }

        public static Task OnNewThreadAsync(this Action action, Action<Task> callback = null)
        {
            return IoCProvider.Container.Resolve<IPlatformBackgroundDispatcher>().RunAsync(action, callback);
        }

        public static void OnNewThread<T>(this Action<T> action, T parameter, Action callback = null)
        {
            IoCProvider.Container.Resolve<IPlatformBackgroundDispatcher>().Run(action, parameter, callback);
        }

        public static Task OnNewThreadAsync<T>(this Action<T> action, T parameter, Action<Task> callback = null)
        {
            return IoCProvider.Container.Resolve<IPlatformBackgroundDispatcher>().RunAsync(action, parameter, callback);
        }
        #endregion

        #region ContextThread
        public static void OnNewThread(this Action action, SynchronizationContext context, Action callback = null)
        {
            IoCProvider.Container.Resolve<IPlatformContextDispatcher>().SetContext(context).Run(action, callback);
        }

        public static Task OnNewThreadAsync(this Action action, SynchronizationContext context, Action<Task> callback = null)
        {
            return IoCProvider.Container.Resolve<IPlatformContextDispatcher>().SetContext(context).RunAsync(action, callback);
        }

        public static void OnNewThread<T>(this Action<T> action, SynchronizationContext context, T parameter, Action callback = null)
        {
            IoCProvider.Container.Resolve<IPlatformContextDispatcher>().SetContext(context).Run(action, parameter, callback);
        }

        public static Task OnNewThreadAsync<T>(this Action<T> action, SynchronizationContext context, T parameter, Action<Task> callback = null)
        {
            return IoCProvider.Container.Resolve<IPlatformContextDispatcher>().SetContext(context).RunAsync(action, parameter, callback);
        }
        #endregion
    }
}