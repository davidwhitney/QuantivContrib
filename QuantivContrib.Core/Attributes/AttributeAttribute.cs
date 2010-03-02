using System;

namespace QuantivContrib.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AttributeAttribute: Attribute
    {
        public string ColumnName { get; set; }

        public AttributeAttribute()
        {
        }

        public AttributeAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}