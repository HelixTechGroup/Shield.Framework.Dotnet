using System;
using System.ComponentModel;

namespace Shield.Framework.Services.Extensibility
{
    public class ModuleDownloadProgressChangedEventArgs : ProgressChangedEventArgs
    {
        public ModuleDownloadProgressChangedEventArgs(ModuleInfo moduleInfo, long bytesReceived, long totalBytesToReceive)
            : base(CalculateProgressPercentage(bytesReceived, totalBytesToReceive), null)
        {
            if (moduleInfo == null)
                throw new ArgumentNullException(nameof(moduleInfo));

            this.ModuleInfo = moduleInfo;
            this.BytesReceived = bytesReceived;
            this.TotalBytesToReceive = totalBytesToReceive;
        }
       
        public ModuleInfo ModuleInfo { get; private set; }
       
        public long BytesReceived { get; private set; }

        public long TotalBytesToReceive { get; private set; }

        private static int CalculateProgressPercentage(long bytesReceived, long totalBytesToReceive)
        {
            if ((bytesReceived == 0L) || (totalBytesToReceive == 0L) || (totalBytesToReceive == -1L))
                return 0;

            return (int)((bytesReceived * 100L) / totalBytesToReceive);
        }
    }
}