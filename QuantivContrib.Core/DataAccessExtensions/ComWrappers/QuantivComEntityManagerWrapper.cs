using Quantiv.Runtime.COM;
using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions.ComWrappers
{
	public class QuantivComEntityManagerWrapper : IQuantivEntityManager
	{
		private readonly EntityManager _manager;

		public QuantivComEntityManagerWrapper(EntityManager manager)
		{
			_manager = manager;
		}

		public IQuantivEntity CreateEntity()
		{
			return new QuantivComEntityWrapper(_manager.CreateEntity());
		}

		public IQuantivEntityRetriever CreateEntityRetriever()
		{
			return new QuantivComEntityRetrieverWrapper(_manager);
		}
	}
}