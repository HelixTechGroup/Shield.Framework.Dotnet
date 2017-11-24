#region Usings
using System;
#endregion

namespace Shield.Framework.Logging
{
    public sealed class LogEntry : ILogEntry, IEquatable<LogEntry>
    {
        public event Action<IDispose> OnDispose;

        #region Members
        private readonly object m_entryLock;
        private readonly Guid m_id;
        private readonly Category m_category;
        private bool m_disposed;
        private string m_logDate;
        private string m_logTime;
        private string m_message;        
        #endregion

        #region Properties
        public bool Disposed
        {
            get { return m_disposed; }
        }

        public Guid Id
        {
            get { return m_id; }
        }

        public Category Category
        {
            get { return m_category; }
        }

        public string LogDate
        {
            get { return m_logDate; }
        }

        public string LogTime
        {
            get { return m_logTime; }
        }

        public string Message
        {
            get
            {
                lock(m_entryLock) { return m_message; }
            }
            set
            {
                lock (m_entryLock)
                { 
                    m_message = value;
                    SetLogDate();
                }
            }
        }

        public Priority Priority => throw new NotImplementedException();

        #endregion

        public LogEntry() : this("", Category.Info) {}

        public LogEntry(Category level) : this("", level) {}

        public LogEntry(string message, Category level)
        {
            m_id = Guid.NewGuid();
            m_entryLock = new object();
            m_message = message;
            m_category = level;
            SetLogDate();
        }

        ~LogEntry()
        {
            Dispose(false);
        }

        #region Methods
        public static bool operator ==(LogEntry left, LogEntry right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LogEntry left, LogEntry right)
        {
            return !Equals(left, right);
        }

        private void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            if (OnDispose != null)
                OnDispose(this);
            m_disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = m_id.GetHashCode();
                hashCode = (hashCode * 397) ^ m_entryLock.GetHashCode();
                return hashCode;
            }
        }

        public bool Equals(LogEntry other)
        {
            return other != null;
        }
        
        public override bool Equals(object obj)
        {
            var svc = obj as LogEntry;
            return svc != null && Equals(svc);
        }

        private void SetLogDate()
        {
            m_logDate = DateTime.Now.ToString("yyyy-MM-dd");
            m_logTime = DateTime.Now.ToString("hh:mm:ss.fff tt");
        }
        #endregion
    }
}