using System;
using System.Linq;
using Castle.DynamicProxy;
using QuantivContrib.Core.Attributes;

namespace QuantivContrib.Core.Commands
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
    }
}