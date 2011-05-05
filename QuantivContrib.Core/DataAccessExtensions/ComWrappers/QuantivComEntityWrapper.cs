using System;
using Quantiv.Runtime.COM;
using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions.ComWrappers
{
    public class QuantivComEntityWrapper: IQuantivEntity
    {
        private readonly Entity _entity;

		public QuantivComEntityWrapper(Entity entity)
        {
            _entity = entity;
        }

        public object GetAttributeValue(string attributeRef)
        {
            return _entity.GetAttributeValue(attributeRef);
        }

        public void SetAttributeValue(string attributeRef, object newValue)
        {
            _entity.SetAttributeValue(attributeRef, newValue);
        }

		public void Save(IQuantivActivity activity)
		{
			if (activity is QuantivComActivityWrapper)
			{
				_entity.Save((QuantivComActivityWrapper)activity);
			}
			else
			{
				throw new InvalidOperationException("Wrong Activity type");
			}
		}

		public TAttributeValueType Get<TAttributeValueType>(string attributeRef)
		{
			if (string.IsNullOrEmpty(attributeRef))
			{
				throw new ArgumentNullException("attributeRef");
			}

			return (TAttributeValueType)_entity.GetAttributeValue(attributeRef);
		}

		public void Set<TAttributeValueType>(string attributeRef, TAttributeValueType value)
		{
			if (string.IsNullOrEmpty(attributeRef))
			{
				throw new ArgumentNullException("attributeRef");
			}

			_entity.SetAttributeValue(attributeRef, value);
		}
    }
}