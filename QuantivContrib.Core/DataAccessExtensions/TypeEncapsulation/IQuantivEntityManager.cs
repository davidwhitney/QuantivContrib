namespace QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation
{
	public interface IQuantivEntityManager
	{
		IQuantivEntity CreateEntity();
		IQuantivEntityRetriever CreateEntityRetriever();
	}
}