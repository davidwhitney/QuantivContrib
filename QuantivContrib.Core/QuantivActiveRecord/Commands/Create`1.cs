using Quantiv.Runtime;

namespace QuantivContrib.Core.QuantivActiveRecord.Commands
{
    public class Create<TTypeOfObjectToCreate> : DomainEntityRepositoryCommandBase<TTypeOfObjectToCreate> where TTypeOfObjectToCreate : DomainEntityBase, new()
    {
        public override TTypeOfObjectToCreate Execute(Activity activity)
        {
            var quantivEntity = activity.GetEntityManager(ExtractEntityNameFromType(typeof(TTypeOfObjectToCreate))).CreateEntity();
            return CreateProxiedEntity<TTypeOfObjectToCreate>(quantivEntity);
        }
    }
}
