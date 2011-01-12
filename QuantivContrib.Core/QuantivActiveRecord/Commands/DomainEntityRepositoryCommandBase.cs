using Quantiv.Runtime;

namespace QuantivContrib.Core.QuantivActiveRecord.Commands
{
    public abstract class DomainEntityRepositoryCommandBase : CommandBase
    {
        public abstract void Execute(Activity activity);
    }
}
