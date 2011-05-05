using Quantiv.Runtime;
using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions.ModernWrappers
{
	public class QuantivEntityRetrieverWrapper : IQuantivEntityRetriever
	{
		public EntityRetriever EntityRetriever { get; private set; }
	
		public QuantivEntityRetrieverWrapper(EntityRetriever entityRetriever)
		{
			EntityRetriever = entityRetriever;
		}

		public string RetrievalPlanRef
		{
			get { return EntityRetriever.RetrievalPlanRef; }
			set { EntityRetriever.RetrievalPlanRef = value; }
		}

		public IQuantivEntity Retrieve(string attributeRef, int id)
		{
			return new QuantivEntityWrapper(EntityRetriever.Retrieve(attributeRef, id));
		}
	}
}