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
            invocation.Proceed();
        }

        protected T GetAttributeValueFromReflectedProperty<T>(MethodBase reflectedMethodMetadata)
        {
            string columnName = GetDatabaseColumnNameForProperty(reflectedMethodMetadata);
            return (T)QuantivEntity.GetAttributeValue(columnName);
        }

        protected void SetAttributeValueFromReflectedProperty<T>(MethodBase reflectedMethodMetadata, T value)
        {
            string columnName = GetDatabaseColumnNameForProperty(reflectedMethodMetadata);
            QuantivEntity.SetAttributeValue(columnName, value);
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
                .GetCustomAttributes(typeof(AttributeRefAttribute), true)
                .ToList();

            if (databaseColumnNameAttributes.Count > 0)
            {
                return ((AttributeRefAttribute)databaseColumnNameAttributes[0]).ColumnName;
            }

            return string.Empty;
        }
    }
}