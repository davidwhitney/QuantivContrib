using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core.Interceptor;
using QuantivContrib.Core.Attributes;

namespace QuantivContrib.Core
{
    public class EntityProxy: IInterceptor 
    {
        private readonly Dictionary<MethodBase, string> _databaseColumnPropertyMap;

        public EntityProxy()
        {
            _databaseColumnPropertyMap = new Dictionary<MethodBase, string>();
        }

        public void Intercept(IInvocation invocation)
        {
            if (!IsAProperty(invocation.Method)
                || !(invocation.InvocationTarget is DomainEntityBase)
                || !PropertyHasAttributeRefAttribute(invocation.Method))
            {
                invocation.Proceed();
                return;
            }

            var parentObject = (DomainEntityBase)invocation.InvocationTarget;

            if (invocation.Method.Name.StartsWith("get_"))
            {
                invocation.ReturnValue = GetAttributeValueFromReflectedProperty(invocation.Method, parentObject);
            }
            else if (invocation.Method.Name.StartsWith("set_"))
            {
                SetAttributeValueFromReflectedProperty(invocation.Method, invocation.Arguments[0], parentObject);
            }
        }

        private static bool IsAProperty(MethodBase invocation)
        {
            return invocation.Name.StartsWith("get_") || invocation.Name.StartsWith("set_");
        }

        protected object GetAttributeValueFromReflectedProperty(MethodBase reflectedMethodMetadata, DomainEntityBase invocationTarget)
        {
            string columnName = GetDatabaseColumnNameForProperty(reflectedMethodMetadata);
            return invocationTarget.QuantivEntity.GetAttributeValue(columnName);
        }

        protected void SetAttributeValueFromReflectedProperty<T>(MethodBase reflectedMethodMetadata, T value, DomainEntityBase invocationTarget)
        {
            string columnName = GetDatabaseColumnNameForProperty(reflectedMethodMetadata);
            invocationTarget.QuantivEntity.SetAttributeValue(columnName, value);
        }

        private string GetDatabaseColumnNameForProperty(MethodBase reflectedMethodMetadata)
        {
            if (!_databaseColumnPropertyMap.ContainsKey(reflectedMethodMetadata))
            {
                string extractedPropertyName = ExtractPropertyNameFromMethodBase(reflectedMethodMetadata);
                string dbColumnOverrideFromAttribute = ExtractDbColumnNameFromAttribute(reflectedMethodMetadata, extractedPropertyName);
                string columnName = !string.IsNullOrEmpty(dbColumnOverrideFromAttribute) ? dbColumnOverrideFromAttribute : extractedPropertyName;

                try
                {
                    _databaseColumnPropertyMap.Add(reflectedMethodMetadata, columnName);
                }
                catch(ArgumentException)
                {
                    // Key was already added, perhaps by another thread, nothing to worry about.
                    // Rather catch than lock and throttle the method.
                }
            }

            return _databaseColumnPropertyMap[reflectedMethodMetadata];
        }

        protected string ExtractPropertyNameFromMethodBase(MethodBase reflectedMethodMetadata)
        {
            if (reflectedMethodMetadata.Name.StartsWith("get_"))
            {
                return reflectedMethodMetadata.Name.Replace("get_", "");
            }

            if (reflectedMethodMetadata.Name.StartsWith("set_"))
            {
                return reflectedMethodMetadata.Name.Replace("set_", "");
            }

            return reflectedMethodMetadata.Name;
        }

        protected string ExtractDbColumnNameFromAttribute(MethodBase reflectedMethodMetadata, string propertyName)
        {
            var databaseColumnNameAttributes = reflectedMethodMetadata.DeclaringType
                .GetProperty(propertyName)
                .GetCustomAttributes(typeof(AttributeAttribute), true)
                .ToList();

            if (databaseColumnNameAttributes.Count > 0)
            {
                return ((AttributeAttribute)databaseColumnNameAttributes[0]).AttributeRef;
            }

            return string.Empty;
        }

        protected bool PropertyHasAttributeRefAttribute(MethodBase property)
        {
            var propertyName = ExtractPropertyNameFromMethodBase(property);

            var databaseColumnNameAttributes = property.DeclaringType
                .GetProperty(propertyName)
                .GetCustomAttributes(typeof(AttributeAttribute), true)
                .ToList();

            return databaseColumnNameAttributes.Count > 0;
        }
    }
}