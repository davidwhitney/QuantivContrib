using System;

namespace QuantivContrib.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AttributeAttribute: Attribute
    {
        public string AttributeRef { get; set; }

        public AttributeAttribute()
        {
        }

        public AttributeAttribute(string attributeRef)
        {
            AttributeRef = attributeRef;
        }
    }
}