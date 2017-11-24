using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shield.Framework.IoC
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InjectPropertyAttribute : Attribute
    {
    }
}
