using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shield.Framework.Logging.Providers
{
    public abstract class TextLogProvider : LogProvider
    {
        protected TextWriter m_writer;

        public override void Flush(ILogEntry entry)
        {
            var message = string.Format("[{2}]:{0}:{1}", entry.LogTime, entry.Message, 
                entry.Category.ToString().ToUpper(CultureInfo.InvariantCulture));

            m_writer.WriteLine(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (!m_disposed)
                m_writer.Dispose();

            base.Dispose(disposing);
        }
    }
}
