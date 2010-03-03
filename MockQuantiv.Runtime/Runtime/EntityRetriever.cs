using System;

namespace Quantiv.Runtime
{
    public class EntityRetriever
    {
        public string RetrievalPlanRef { get; set; }
        
        public Entity Retrieve(string column, int id)
        {
            return new Entity();
        }

        public Entity Retrieve(string propertyName, object value)
        {
            return new Entity();
        }
    }
}