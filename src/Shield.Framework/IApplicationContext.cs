#region Usings
using Shield.Framework.Environment;
using Shield.Framework.IoC.DependencyInjection;
using Shield.Framework.Services;
using Shin.Framework;
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
        ILogService Log { get; }
        IMessageAggregator MessageAggregator { get; }
        IModuleLoaderService Module { get; }
        IThreadService Thread { get; }
        #endregion
    }
}