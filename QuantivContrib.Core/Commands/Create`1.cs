﻿using Quantiv.Runtime;

namespace QuantivContrib.Core.Commands
{
    public class Create<TTypeOfObjectToCreate> : DomainEntityRepositoryCommandBase<TTypeOfObjectToCreate> where TTypeOfObjectToCreate : DomainEntityBase, new()
    {
        public override TTypeOfObjectToCreate Execute(Activity activity)
        {
            var quantivEntity = activity.GetEntityManager(ExtractEntityNameFromType(typeof(TTypeOfObjectToCreate))).CreateEntity();

            var domainEntity = ProxyGenerator.CreateClassProxy<TTypeOfObjectToCreate>(new EntityProxy());
            domainEntity.QuantivEntity = quantivEntity;

            return domainEntity;
        }
    }
}