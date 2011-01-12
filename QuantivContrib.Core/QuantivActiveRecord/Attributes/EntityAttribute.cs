using System;

namespace QuantivContrib.Core.QuantivActiveRecord.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EntityAttribute: Attribute
    {
        public string ClassRef { get; set; }

        public EntityAttribute()
        {
        }

        public EntityAttribute(string classRef)
        {
            ClassRef = classRef;
        }
    }
}