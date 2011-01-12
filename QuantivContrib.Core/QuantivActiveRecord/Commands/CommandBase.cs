using System;
using System.Linq;
using Castle.DynamicProxy;
using Quantiv.Runtime;
using QuantivContrib.Core.QuantivActiveRecord.Attributes;

namespace QuantivContrib.Core.QuantivActiveRecord.Commands
{
    public abstract class CommandBase
    {
        protected readonly ProxyGenerator ProxyGenerator;

        protected CommandBase()
        {
            ProxyGenerator = new ProxyGenerator();
        }

        protected static string ExtractEntityNameFromType(Type type)
        {
            var classRefAttribute = type.GetCustomAttributes(typeof(EntityAttribute), false).FirstOrDefault();
            
            if (classRefAttribute != null)
            {
                var classRefFromAttribute = ((EntityAttribute)classRefAttribute).ClassRef;
                return !string.IsNullOrEmpty(classRefFromAttribute) ? classRefFromAttribute : type.Name;
            }
            
            return type.Name;
        }

        protected TProxiedType CreateProxiedEntity<TProxiedType>(Entity quantivEntity) where TProxiedType : DomainEntityBase, new()
        {
            var domainEntity = ProxyGenerator.CreateClassProxy<TProxiedType>(new EntityProxy());
            domainEntity.QuantivEntity = quantivEntity;
            return domainEntity;
        }
    }
}