using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneration_Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public sealed class IgnoreEqualityAttribute : Attribute
    { }
}
