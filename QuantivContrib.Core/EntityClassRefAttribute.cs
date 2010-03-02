using System;

namespace Quantiv5UsageExamples.QuantivContrib
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EntityClassRefAttribute: Attribute
    {
        public string ClassRef { get; set; }

        public EntityClassRefAttribute(string classRef)
        {
            ClassRef = classRef;
        }
    }
}