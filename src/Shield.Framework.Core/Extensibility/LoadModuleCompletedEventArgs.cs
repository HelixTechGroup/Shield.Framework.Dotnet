using System;

namespace Shield.Framework.Extensibility
{
    public class LoadModuleCompletedEventArgs : EventArgs
    {
        public LoadModuleCompletedEventArgs(ModuleInfo moduleInfo, Exception error)
        {
            if (moduleInfo == null)
                throw new ArgumentNullException(nameof(moduleInfo));

            ModuleInfo = moduleInfo;
            Error = error;
        }

        public ModuleInfo ModuleInfo { get; private set; }

        public Exception Error { get; private set; }

        public bool IsErrorHandled { get; set; }
    }
}