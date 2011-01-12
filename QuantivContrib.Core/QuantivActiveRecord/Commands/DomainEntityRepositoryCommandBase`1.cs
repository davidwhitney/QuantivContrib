using Quantiv.Runtime;

namespace QuantivContrib.Core.QuantivActiveRecord.Commands
{
    public abstract class DomainEntityRepositoryCommandBase<TReturnType> : CommandBase where TReturnType : DomainEntityBase, new()
    {
        public abstract TReturnType Execute(Activity activity);
    }
}