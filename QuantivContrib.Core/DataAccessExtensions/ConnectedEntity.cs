using System;
using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions
{
    public class ConnectedEntity: IDisposable, IConnectedEntity
    {
        private readonly IActivitySession _activitySession;
        private readonly string _entityClassRef;
        private readonly IQuantivEntity _entity;
        private bool _disposed;

        public ConnectedEntity(IActivitySession activitySession, string entityClassRef, int id)
        {
            _activitySession = activitySession;
            _entityClassRef = entityClassRef;

            _entity = Retrieve(_entityClassRef, id);
        }

        ~ConnectedEntity()
        {
            Dispose(false);
        }

        private IQuantivEntity Retrieve(string entityClassRef, int id, string retrievalPlanRef = null)
        {
            return _activitySession.Retrieve(entityClassRef, id, retrievalPlanRef);
        }

        public T Get<T>(string attributeRef)
        {
            return (T)_entity.GetAttributeValue(attributeRef);
        }

        public void Set<T>(string attributeRef, T value)
        {
            _entity.SetAttributeValue(attributeRef, value);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) { return; }

            _disposed = true;
        }
    }
}