using System.Globalization;
using System.IO;

namespace Shield.Framework.Platform.Logging.Loggers
{
    public abstract class TextLogger : Logger
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
