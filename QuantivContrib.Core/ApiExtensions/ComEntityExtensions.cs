using System;
using Quantiv.Runtime.COM;

namespace QuantivContrib.Core.ApiExtensions
{
    public static class ComEntityExtensions
    {
        public static TAttributeValueType Get<TAttributeValueType>(this IEntity entity, string attributeRef)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if(string.IsNullOrEmpty(attributeRef))
            {
                throw new ArgumentNullException("attributeRef");
            }

            return (TAttributeValueType)entity.GetAttributeValue(attributeRef);
        }

        public static void Set<TAttributeValueType>(this IEntity entity, string attributeRef, TAttributeValueType value)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (string.IsNullOrEmpty(attributeRef))
            {
                throw new ArgumentNullException("attributeRef");
            }

            entity.SetAttributeValue(attributeRef, value);
        }
    }
}