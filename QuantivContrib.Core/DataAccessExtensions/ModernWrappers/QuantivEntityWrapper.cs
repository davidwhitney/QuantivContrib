using System;
using Quantiv.Runtime;
using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions.ModernWrappers
{
    public class QuantivEntityWrapper: IQuantivEntity
    {
        private readonly Entity _entity;

        public QuantivEntityWrapper(Entity entity)
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
			if (activity is QuantivActivityWrapper)
			{
				_entity.Save((QuantivActivityWrapper)activity);
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