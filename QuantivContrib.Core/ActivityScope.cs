using System;
using Quantiv.Runtime;
using QuantivContrib.Core.Commands;

namespace QuantivContrib.Core
{
    public class ActivityScope: IDisposable
    {
        private ActivityController _activityController;
        private Activity _activity;

        private readonly string _controllerPool;
        private readonly string _activityRef;

        private bool _disposed;

        public ActivityScope(string controllerPool, string activityRef)
        {
            _controllerPool = controllerPool;
            _activityRef = activityRef;
            CreateActivity();
        }
        ~ActivityScope()
        {
            Dispose(false);
        }

        public T Create<T>() where T : DomainEntityBase, new()
        {
            return new Create<T>().Execute(_activity);
        }

        public T Retrieve<T>(int id) where T : DomainEntityBase, new()
        {
            return new Retrieve<T>(id).Execute(_activity);
        }

        public T Retrieve<T>(string propertyName, object value) where T : DomainEntityBase, new()
        {
            return new Retrieve<T>(propertyName, value).Execute(_activity);
        }

        public void SaveNewEntity(DomainEntityBase unsavedEntity)
        {
            new SaveNewEntity(unsavedEntity).Execute(_activity);
        }

        public void Flush()
        {
            Flush(true);
        }

        private void Flush(bool createNewActivity)
        {
            _activityController.Post();
            if (createNewActivity)
            {
                CreateActivity();
            }
        }
        
        private void CreateActivity()
        {
            _activityController = ActivityControllerPooler.AllocateController(_controllerPool);
            _activity = _activityController.StartActivity(_activityRef);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Flush(false);
                }
                _disposed = true;
            }
        }
    }
}