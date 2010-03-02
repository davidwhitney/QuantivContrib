using System;

namespace QuantivContrib.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EntityClassAttribute: Attribute
    {
        public string ClassRef { get; set; }

        public EntityClassAttribute(string classRef)
        {
            ClassRef = classRef;
        }
    }
}