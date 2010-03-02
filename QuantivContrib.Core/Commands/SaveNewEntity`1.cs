using Quantiv.Runtime;

namespace QuantivContrib.Core.Commands
{
    public class SaveNewEntity : DomainEntityRepositoryCommandBase
    {
        public DomainEntityBase UnsavedEntity { get; private set; }

        public SaveNewEntity(DomainEntityBase unsavedEntity)
        {
            UnsavedEntity = unsavedEntity;
        }

        public override void Execute(Activity activity)
        {
            UnsavedEntity.QuantivEntity.Save(activity);
        }
    }
}
