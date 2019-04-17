#region Usings
#endregion

#region Usings
using Shield.Framework.Platform;
#endregion

namespace Shield.Framework
{
    public abstract class PlatformProvider<TEnvironment, TServices>
        where TServices : IPlatformServices
        where TEnvironment : IPlatformEnvironment
    {
        #region Properties
        public static TEnvironment Environment
        {
            get { return IoCProvider.Container.Resolve<TEnvironment>(); }
        }

        public static TServices Services
        {
            get { return IoCProvider.Container.Resolve<TServices>(); }
        }
        #endregion
    }
}