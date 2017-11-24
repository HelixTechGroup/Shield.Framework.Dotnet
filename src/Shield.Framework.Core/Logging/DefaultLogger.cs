﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shield.Framework.Collections;
using Shield.Framework.Platform;

namespace Shield.Framework.Logging
{
    public sealed class DefaultLogger : ILogger
    {
        public event Action<IDispose> OnDispose;

        private const int m_queueSize = 2;
        private bool m_disposed;
        private readonly ConcurrentQueue<ILogEntry> m_logQueue;
        private Task m_LogTask;
        private readonly object m_logLock;
        private readonly ConcurrentList<ILogProvider> m_logProviders;

        public bool Disposed
        {
            get { return m_disposed; }
        }

        public DefaultLogger()
        {
            m_logQueue = new ConcurrentQueue<ILogEntry>();
            m_logLock = new object();
            m_logProviders = new ConcurrentList<ILogProvider>();
            m_LogTask = Task.CompletedTask;
            //m_LogThread = new BackgroundWorker();
            //m_LogThread.WorkerSupportsCancellation = false;
            //m_LogThread.DoWork += (s, e) => Flush();
        }

        ~DefaultLogger()
        {
            Dispose(false);
        }

        public void AddProvider(ILogProvider logProvider)
        {
            if (!m_logProviders.Contains(logProvider))
                m_logProviders.Add(logProvider);
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

            m_logProviders.Dispose();
            
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
                    m_LogTask = PlatformProvider.BackgroundDispatcher.RunAsync(Flush);
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
                    foreach (var provider in m_logProviders)
                        provider.Flush(entry);

                    entry.Dispose();
                }
            }
        }
    }
}
