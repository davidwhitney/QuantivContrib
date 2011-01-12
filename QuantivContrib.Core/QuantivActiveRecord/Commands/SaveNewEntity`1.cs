using Quantiv.Runtime;

namespace QuantivContrib.Core.QuantivActiveRecord.Commands
{
    public class Save : DomainEntityRepositoryCommandBase
    {
        public DomainEntityBase UnsavedEntity { get; private set; }

        public Save(DomainEntityBase unsavedEntity)
        {
            UnsavedEntity = unsavedEntity;
        }

        public override void Execute(Activity activity)
        {
            UnsavedEntity.QuantivEntity.Save(activity);
            UnsavedEntity.Dirty = false;
        }
    }
}
