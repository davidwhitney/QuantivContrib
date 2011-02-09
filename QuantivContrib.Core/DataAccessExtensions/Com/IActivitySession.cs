using Quantiv.Runtime.COM;

namespace QuantivContrib.Core.DataAccessExtensions.Com
{
    public interface IActivitySession
    {
        bool ReadOnly { get; set; }
        bool AutoSave { get; set; }
        ActivityController CurrentActivityController { get; }
        Activity CurrentActivity { get; }
        Entity Create(string entityClassRef);
        Entity Retrieve(string entityClassRef, int id, string retrievalPlanRef = null);
        bool TryRetrieve(string entityClassRef, int id, out Entity entity, string retrievalPlanRef = null);
        void Save(Entity entity);
        EntityManager GetEntityManager(string entityClassRef);
        void Commit();
    }
}