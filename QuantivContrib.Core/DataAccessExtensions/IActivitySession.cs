using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions
{
    public interface IActivitySession
    {
        bool ReadOnly { get; set; }
        bool AutoSave { get; set; }
        IQuantivActivityController CurrentActivityController { get; }
		IQuantivActivity CurrentActivity { get; }
        IQuantivEntity Create(string entityClassRef);
        IQuantivEntity Retrieve(string entityClassRef, int id, string retrievalPlanRef = null);
		bool TryRetrieve(string entityClassRef, int id, out IQuantivEntity entity, string retrievalPlanRef = null);
        void Save(IQuantivEntity entity);
		IQuantivEntityManager GetEntityManager(string entityClassRef);
        void Commit();
    }
}