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

            var typedEntities = new System.Collections.Generic.List<TTypeOfObjectToCreate>();
            foreach (var quantivEntity in entityList)
            {
                var domainEntity = ProxyGenerator.CreateClassProxy<TTypeOfObjectToCreate>(new EntityProxy());
                domainEntity.QuantivEntity = quantivEntity;
                typedEntities.Add(domainEntity);
            }

            return typedEntities;
        }
    }
}
