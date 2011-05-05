using Quantiv.Runtime.COM;
using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions.ComWrappers
{
	public class QuantivComEntityRetrieverWrapper : IQuantivEntityRetriever
	{
		public EntityManager EntityManager { get; set; }

		public QuantivComEntityRetrieverWrapper(EntityManager entityRetriever)
		{
			EntityManager = entityRetriever;
		}

		public string RetrievalPlanRef { get; set; }

		public IQuantivEntity Retrieve(string attributeRef, int id)
		{
			var entity = EntityManager.CreateEntity(false);
			entity.SetRetrievalPlan(RetrievalPlanRef);
			entity.RetrieveWithAttributeValue(attributeRef, id);
			return new QuantivComEntityWrapper(entity);
		}
	}
}