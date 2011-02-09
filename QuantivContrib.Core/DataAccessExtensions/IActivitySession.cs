using Quantiv.Runtime;

namespace QuantivContrib.Core.DataAccessExtensions
{
    public interface IActivitySession
    {
        bool ReadOnly { get; set; }
        bool AutoSave { get; set; }
        ActivityController CurrentActivityController { get; }
        Activity CurrentActivity { get; }
        Entity Create(string entityClassRef);
        Entity Retrieve(string entityClassRef, int id, string retrievalPlanRef = null);
        void Save(Entity entity);
        EntityManager GetEntityManager(string entityClassRef);
        void Commit();
    }
}