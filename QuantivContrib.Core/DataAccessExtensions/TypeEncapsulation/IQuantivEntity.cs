namespace QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation
{
    public interface IQuantivEntity
    {
        object GetAttributeValue(string attributeRef);
        void SetAttributeValue(string attributeRef, object newValue);
		void Save(IQuantivActivity activity);

    	TAttributeValueType Get<TAttributeValueType>(string attributeRef);
    	void Set<TAttributeValueType>(string attributeRef, TAttributeValueType value);
    }
}