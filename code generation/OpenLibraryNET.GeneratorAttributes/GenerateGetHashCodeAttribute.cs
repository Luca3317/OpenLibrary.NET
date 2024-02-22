using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration_Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class GenerateGetHashCodeAttribute : Attribute
    { }
}
