#region Usings
using Shield.Framework.Environment;
using Shield.Framework.IoC.DependencyInjection;
#endregion

namespace Shield.Framework
{
    public class Shield
    {
        #region Properties
        public IContainer Container
        {
            get { return Context.Container; }
        }

        public IApplicationContext Context { get; }

        public static Shield CurrentApplication { get; private set; }

        public IApplicationEnvironment Environment
        {
            get { return Context.Environment; }
        }
        #endregion

        internal Shield(IApplicationContext application)
        {
            Context = application;
            CurrentApplication = this;
        }

        #region Methods
        public static Shield Initialize(IApplicationContext context)
        {
            return new Shield(context);
        }
        #endregion
    }
}