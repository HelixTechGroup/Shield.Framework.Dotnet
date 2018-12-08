using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Shield.Framework.Collections;

namespace Shield.Framework.Platform.Logging
{
    public sealed class DefaultLogProvider : ILogProvider
    {
        public event Action<IDispose> OnDispose;

        private const int m_queueSize = 2;
        private bool m_disposed;
        private readonly ConcurrentQueue<ILogEntry> m_logQueue;
        private Task m_LogTask;
        private readonly object m_logLock;
        private readonly ConcurrentList<ILogger> m_loggers;

        public bool Disposed
        {
            get { return m_disposed; }
        }

        public DefaultLogProvider()
        {
            m_logQueue = new ConcurrentQueue<ILogEntry>();
            m_logLock = new object();
            m_loggers = new ConcurrentList<ILogger>();
            m_LogTask = Task.CompletedTask;
            //m_LogThread = new BackgroundWorker();
            //m_LogThread.WorkerSupportsCancellation = false;
            //m_LogThread.DoWork += (s, e) => Flush();
        }

        ~DefaultLogProvider()
        {
            Dispose(false);
        }

        public void AddLogger(ILogger logProvider)
        {
            if (!m_loggers.Contains(logProvider))
                m_loggers.Add(logProvider);
        }

        public void LogInfo(string message)
        {
            Log(new LogEntry(message, Category.Info));
        }

        public void LogWarn(string message)
        {
            Log(new LogEntry(message, Category.Warn));
        }

        public void LogError(string message)
        {
            Log(new LogEntry(message, Category.Exception));
        }

        public void LogDebug(string message)
        {
            Log(new LogEntry(message, Category.Debug));
        }

        public void LogException(Exception exception)
        {
            string trace = exception.StackTrace;

            if (exception.StackTrace.Length > 1300)
                trace = exception.StackTrace.Substring(0, 1300) + " [...] (traceback cut short)";

            LogError(string.Format("{0}\n{1}\n{2}",
                                     exception.Message,
                                     exception.Source + " raised a " + exception.GetType(),
                                     trace));
        }

        public void Log(string message, Category category, Priority priority)
        {
            Log(new LogEntry(message, category));
        }

        public void Log(ILogEntry entry)
        {
            Enqueue(entry);
        }

        private void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            m_loggers.Dispose();
            
            if (OnDispose != null)
                OnDispose(this);
            m_disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }        
        
        private void Enqueue(ILogEntry entry)
        {
            lock (m_logLock)
            {
                m_logQueue.Enqueue(entry);               
            }

            lock(m_LogTask)
            {
                if (m_LogTask.IsCompleted)
                    m_LogTask = PlatformProvider.Services.Dispatcher.BackgroundDispatcher.RunAsync(Flush);
            }
        }

        private void Flush()
        {
            lock (m_logLock)
            {
                if (m_logQueue.Count < m_queueSize)
                    return;

                while (m_logQueue.Count > 0)
                {
                    ILogEntry entry;
                    m_logQueue.TryDequeue(out entry);
                    foreach (var provider in m_loggers)
                        provider.Flush(entry);

                    entry.Dispose();
                }
            }
        }
    }
}
