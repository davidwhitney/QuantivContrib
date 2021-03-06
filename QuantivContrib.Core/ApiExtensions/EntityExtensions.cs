﻿using System;
using Entity = Quantiv.Runtime.Entity;

namespace QuantivContrib.Core.ApiExtensions
{
    public static class EntityExtensions
    {
        public static TAttributeValueType Get<TAttributeValueType>(this Entity entity, string attributeRef)
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

        public static void Set<TAttributeValueType>(this Entity entity, string attributeRef, TAttributeValueType value)
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
