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
}
