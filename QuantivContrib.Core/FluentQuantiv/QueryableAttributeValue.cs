using System;

namespace QuantivContrib.Core.FluentQuantiv
{
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