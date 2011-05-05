namespace QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation
{
    public interface IQuantivActivity
    {
    	IQuantivEntityManager GetEntityManager(string entityClassRef);
    }
}
