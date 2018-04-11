using System;
using Shield.Framework.Collections;
using Shield.Framework.Extensions;

namespace Shield.Framework.Exceptions
{
    public sealed class FrameworkExceptionProvider
    {
        private readonly ConcurrentList<Type> m_frameworkExceptionTypes;

        public FrameworkExceptionProvider()
        {
            m_frameworkExceptionTypes = new ConcurrentList<Type>();
        }

        public void RegisterFrameworkException<T>() where T : Exception
        {
            RegisterFrameworkException(typeof(T));
        }

        public void RegisterFrameworkException(Type exceptionType)
        {
            IsFrameworkExceptionRegistered(exceptionType);
            m_frameworkExceptionTypes.Add(exceptionType);
        }

        public bool IsFrameworkExceptionRegistered<T>()
        {
            return IsFrameworkExceptionRegistered(typeof(T));
        }

        public bool IsFrameworkExceptionRegistered(Type exceptionType)
        {
            if (!exceptionType.ContainsType(typeof(Exception)))
                throw new ArgumentException(string.Format("Argument {0} is not an Exception type.", exceptionType.FullName), nameof(exceptionType));

            return m_frameworkExceptionTypes.Contains(exceptionType);
        }

        public Exception GetFrameworkException(Exception exception)
        {
            var rootException = exception;

            try
            {
                while (true)
                {
                    if (rootException == null)
                    {
                        rootException = exception;
                        break;
                    }

                    if (!IsFrameworkException(rootException))
                        break;

                    rootException = rootException.InnerException;
                }
            }
            catch
            {
                rootException = exception;
            }
            return rootException;
        }

        public T GenerateFrameworkException<T>(string message) where T : Exception, new()
        {
            if (!IsFrameworkExceptionRegistered<T>())
                RegisterFrameworkException<T>();

            return ExceptionFactory.GenerateException<T>(message);
        }

        private bool IsFrameworkException(Exception exception)
        {
            var isFrameworkException = IsFrameworkExceptionRegistered(exception.GetType());
            var childIsFrameworkException = false;

            if (exception.InnerException != null)
                childIsFrameworkException = IsFrameworkExceptionRegistered(exception.InnerException.GetType());

            return isFrameworkException || childIsFrameworkException;
        }
    }
}