using Quantiv.Runtime;
using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions.ModernWrappers
{
	public class QuantivEntityManagerWrapper : IQuantivEntityManager
	{
		private readonly EntityManager _manager;

		public QuantivEntityManagerWrapper(EntityManager manager)
		{
			_manager = manager;
		}

		public IQuantivEntity CreateEntity()
		{
			return new QuantivEntityWrapper(_manager.CreateEntity());
		}

		public IQuantivEntityRetriever CreateEntityRetriever()
		{
			return new QuantivEntityRetrieverWrapper(_manager.CreateEntityRetriever());
		}
	}
}