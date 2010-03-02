using System;
using Castle.Core.Interceptor;

namespace QuantivContrib.Core
{
    public class EntityProxy: IInterceptor 
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}