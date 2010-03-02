using System;

namespace QuantivContrib.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AttributeRefAttribute: Attribute
    {
        public string ColumnName { get; set; }

        public AttributeRefAttribute()
        {
        }

        public AttributeRefAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}