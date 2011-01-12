using System.Collections.Generic;

namespace QuantivContrib.Core.LinqToQuantiv
{
    public class EntityQueryParts
    {
        public string EntityTypeRef { get; set; }
        public string Something { get; set; }
        public int Id { get; set; }

        public Dictionary<string, string> QueryParts { get; set; }
    }

    public class QueryPair
    {
        public string AttributeRef { get; set; }
    }

    public class AttributeRefQuery
    {
        public string AttributeRef { get; set; }
    }
    public class AttributeRefQueryTarget
    {
        public string AttributeValue { get; set; }
    }
}