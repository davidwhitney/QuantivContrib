using Quantiv.Runtime;

namespace QuantivContrib.Core.Commands
{
    public abstract class DomainEntityRepositoryCommandBase : CommandBase
    {
        public abstract void Execute(Activity activity);
    }
}
