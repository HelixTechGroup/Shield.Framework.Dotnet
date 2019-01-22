using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Shield.Framework.Collections;

namespace Shield.Framework.Platform.Logging.Default
{
    public sealed class DefaultPlatformLogProvider : IPlatformLogProvider
    {
        public event Action<IDispose> OnDispose;

        private const int m_queueSize = 2;
        private bool m_disposed;
        private readonly ConcurrentQueue<IPlatformLogEntry> m_logQueue;
        private Task m_LogTask;
        private readonly object m_logLock;
        private readonly ConcurrentList<IPlatformLogger> m_loggers;

        public bool Disposed
        {
            get { return m_disposed; }
        }

        public DefaultPlatformLogProvider()
        {
            m_logQueue = new ConcurrentQueue<IPlatformLogEntry>();
            m_logLock = new object();
            m_loggers = new ConcurrentList<IPlatformLogger>();
            m_LogTask = Task.CompletedTask;
            //m_LogThread = new BackgroundWorker();
            //m_LogThread.WorkerSupportsCancellation = false;
            //m_LogThread.DoWork += (s, e) => Flush();
        }

        ~DefaultPlatformLogProvider()
        {
            Dispose(false);
        }

        public void AddLogger(IPlatformLogger logProvider)
        {
            if (!m_loggers.Contains(logProvider))
                m_loggers.Add(logProvider);
        }

        public void LogInfo(string message)
        {
            Log(new PlatformLogEntry(message, PlatformLogCategory.Info));
        }

        public void LogWarn(string message)
        {
            Log(new PlatformLogEntry(message, PlatformLogCategory.Warn));
        }

        public void LogError(string message)
        {
            Log(new PlatformLogEntry(message, PlatformLogCategory.Exception));
        }

        public void LogDebug(string message)
        {
            Log(new PlatformLogEntry(message, PlatformLogCategory.Debug));
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

        public void Log(string message, PlatformLogCategory category, PlatformLogPriority priority)
        {
            Log(new PlatformLogEntry(message, category));
        }

        public void Log(IPlatformLogEntry entry)
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
        
        private void Enqueue(IPlatformLogEntry entry)
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
                    IPlatformLogEntry entry;
                    m_logQueue.TryDequeue(out entry);
                    foreach (var provider in m_loggers)
                        provider.Flush(entry);

                    entry.Dispose();
                }
            }
        }
    }
}
