namespace QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation
{
	public interface IQuantivEntityRetriever
	{
		string RetrievalPlanRef { get; set; }
		IQuantivEntity Retrieve(string attributeRef, int id);
	}
}