namespace QuantivContrib.Core.DataAccessExtensions
{
    public interface IConnectedEntity
    {
        T Get<T>(string attributeRef);
        void Set<T>(string attributeRef, T value);
    }
}