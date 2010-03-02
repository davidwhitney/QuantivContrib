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
            return classRefAttribute != null ? ((EntityAttribute)classRefAttribute).ClassRef : type.Name;
        } 
    }
}