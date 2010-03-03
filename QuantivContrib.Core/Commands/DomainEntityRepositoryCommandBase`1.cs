using Quantiv.Runtime;

namespace QuantivContrib.Core.Commands
{
    public abstract class DomainEntityRepositoryCommandBase<TReturnType> : CommandBase where TReturnType : DomainEntityBase, new()
    {
        public abstract TReturnType Execute(Activity activity);

        protected TReturnType CreateProxiedEntity(Entity quantivEntity)
        {
            var domainEntity = ProxyGenerator.CreateClassProxy<TReturnType>(new EntityProxy());
            domainEntity.QuantivEntity = quantivEntity;
            return domainEntity;
        }
    }
}