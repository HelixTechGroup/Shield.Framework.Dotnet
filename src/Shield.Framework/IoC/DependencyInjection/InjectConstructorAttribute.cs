using System;

namespace Shield.Framework.IoC.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class InjectConstructorAttribute : Attribute
    {
    }
}
