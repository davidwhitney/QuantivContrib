using Quantiv.Runtime;

namespace QuantivContrib.Core.Commands
{
    public class CreateSearchConditions<TSearchableEntityType> : CommandBase where TSearchableEntityType : DomainEntityBase, new()
    {
        public SearchConditionList Execute(Activity activity)
        {
            return activity.GetEntityManager(ExtractEntityNameFromType(typeof(TSearchableEntityType)))
                           .CreateEntityListRetriever()
                           .SearchConditionList;

        }
    }
}
