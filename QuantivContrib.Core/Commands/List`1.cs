using System.Linq;
using Quantiv.Runtime;

namespace QuantivContrib.Core.Commands
{
    public class List<TTypeOfObjectToCreate> : CommandBase where TTypeOfObjectToCreate : DomainEntityBase, new()
    {
        public string RetrievalComment { get; set; }
        public SearchConditionList SearchConditions { get; set; }
        public string RetrievalPlan { get; set; }

        public System.Collections.Generic.List<TTypeOfObjectToCreate> Execute(Activity activity)
        {
            var entityListRetriever = activity.GetEntityManager(ExtractEntityNameFromType(typeof(TTypeOfObjectToCreate)))
                                              .CreateEntityListRetriever();

            if (!string.IsNullOrEmpty(RetrievalComment))
            {
                entityListRetriever.RetrievalComment = RetrievalComment;
            }

            if (SearchConditions != null)
            {
                entityListRetriever.SearchConditionList.AddConditionList(SearchConditions);
            }

            if(!string.IsNullOrEmpty(RetrievalPlan))
            {
                entityListRetriever.RetrievalPlanRef = RetrievalPlan;
            }

            var entityList = entityListRetriever.Retrieve();
            return entityList.Select(quantivEntity => CreateProxiedEntity<TTypeOfObjectToCreate>(quantivEntity)).ToList();
        }
    }
}
