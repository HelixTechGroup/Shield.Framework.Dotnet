using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shield.Framework.IoC.Default
{
    internal sealed class ConstructorInvokeInfo
    {
        internal readonly ParameterInfo[] ParameterInfos;

        Func<object[], object> constructorFunc;

        internal Func<object[], object> ConstructorFunc
        {
            get { return constructorFunc ?? (constructorFunc = ReflectionCompiler.CreateFunc(Constructor)); }
        }

        internal readonly ConstructorInfo Constructor;

        internal ConstructorInvokeInfo(ConstructorInfo constructor)
        {
            Constructor = constructor;
            ParameterInfos = constructor.GetParameters();
        }
    }
}
