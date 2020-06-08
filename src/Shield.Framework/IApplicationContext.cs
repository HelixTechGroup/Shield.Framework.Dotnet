#region Usings
using Patchwork.Framework.Environment;
using Shield.Framework.Services;
using Shin.Framework;
using Shin.Framework.IoC.DependencyInjection;
#endregion

namespace Shield.Framework
{
    public interface IApplicationContext : IDispose, IInitialize
    {
        #region Properties
        IContainer Container { get; }
        IApplicationContext Current { get; }
        IApplicationEnvironment Environment { get; }
        ILifeCycleService LifeCycle { get; }
        ILogger Log { get; }
        //IMessageAggregator MessageAggregator { get; }
        IModuleLoaderService Module { get; }
        IThreadService Thread { get; }
        #endregion
    }
}