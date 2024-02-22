using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneration_Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class CollectionValueEqualityAttribute : Attribute
    { }
}
