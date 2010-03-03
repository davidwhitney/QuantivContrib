using System.Collections.Generic;

namespace Quantiv.Runtime
{
    public class EntityListRetriever
    {
        public string RetrievalPlanRef { get; set; }
        public string RetrievalComment { get; set; }
        public SearchConditionList SearchConditionList { get; set; }
        
        public EntityListRetriever()
        {
            SearchConditionList = new SearchConditionList();
        }

        public List<Entity> Retrieve()
        {
            return new List<Entity>();
        }
    }
}