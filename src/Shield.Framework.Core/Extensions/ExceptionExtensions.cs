using System;
using Shield.Framework.Exceptions;
using Shield.Framework.IoC;

namespace Shield.Framework.Extensions
{
    public static class ExceptionExtensions
    {
        public static Exception GetFrameworkException(this Exception exception)
        {
            return IoCProvider.Container.Resolve<FrameworkExceptionProvider>()
                    .GetFrameworkException(exception);
        }
    }
}