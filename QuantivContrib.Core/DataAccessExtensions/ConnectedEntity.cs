using System;
using Quantiv.Runtime;
using QuantivContrib.Core.ApiExtensions;

namespace QuantivContrib.Core.DataAccessExtensions
{
    public class ConnectedEntity: IDisposable, IConnectedEntity
    {
        private readonly IActivitySession _activitySession;
        private readonly string _entityClassRef;
        private readonly Entity _entity;
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

        private Entity Retrieve(string entityClassRef, int id, string retrievalPlanRef = null)
        {
            var manager = _activitySession.CurrentActivity.GetEntityManager(entityClassRef);
            var retriever = manager.CreateEntityRetriever();

            if (!string.IsNullOrEmpty(retrievalPlanRef))
            {
                retriever.RetrievalPlanRef = retrievalPlanRef;
            }

            return retriever.Retrieve(entityClassRef + "Id", id);
        }

        public T Get<T>(string attributeRef)
        {
            return _entity.Get<T>(attributeRef);
        }

        public void Set<T>(string attributeRef, T value)
        {
            _entity.Set(attributeRef, value);
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