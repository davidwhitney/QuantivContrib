﻿using System;
using Quantiv.Runtime;

namespace QuantivContrib.Core.FluentQuantiv
{
    public class QueryableEntity
    {
        public Entity QuantivEntity { get; set; }

        public int EntityId { get; set; }

        public QueryableEntity(Entity quantivEntity)
        {
            QuantivEntity = quantivEntity;
        }

        public QueryableAttributeValue this[string attributeRef]
        {
            get { return new QueryableAttributeValue(QuantivEntity.GetAttributeValue(attributeRef)); }
        }

        public static implicit operator Entity(QueryableEntity me)
        {
            return me.QuantivEntity;
        }
    }

    public class QueryableAttributeValue
    {
        public object AttributeValue { get; private set; }

        public QueryableAttributeValue(object attributeValue)
        {
            AttributeValue = attributeValue;
        }

        public bool Like(string value)
        {
            throw new NotImplementedException();
        }

        public static implicit operator string (QueryableAttributeValue me)
        {
            return me.AttributeValue.ToString();
        }

        public static implicit operator QueryableAttributeValue(string them)
        {
            return new QueryableAttributeValue(them);
        }

        public static implicit operator int (QueryableAttributeValue me)
        {
            return (int)me.AttributeValue;
        }

        public static implicit operator QueryableAttributeValue(int me)
        {
            return new QueryableAttributeValue(me);
        }
    }
}
