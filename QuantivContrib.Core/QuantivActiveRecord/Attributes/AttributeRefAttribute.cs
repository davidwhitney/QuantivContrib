using System;

namespace QuantivContrib.Core.QuantivActiveRecord.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AttributeRefAttribute: Attribute
    {
        public string AttributeRef { get; set; }

        public AttributeRefAttribute()
        {
        }

        public AttributeRefAttribute(string attributeRef)
        {
            AttributeRef = attributeRef;
        }
    }
}