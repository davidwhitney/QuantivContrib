using System;
using System.Linq;
using Castle.DynamicProxy;
using Quantiv.Runtime;
using QuantivContrib.Core.Attributes;

namespace QuantivContrib.Core
{
    public class ActivityScope: IDisposable
    {
        private readonly ProxyGenerator _generator;

        private ActivityController _activityController;
        private Activity _activity;

        private readonly string _controllerPool;
        private readonly string _activityRef;

        private bool _disposed;

        public ActivityScope(string controllerPool, string activityRef)
        {
            _generator =  new ProxyGenerator();
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
            var quantivEntity = _activity.GetEntityManager(ExtractEntityNameFromType(typeof(T))).CreateEntity();

            var domainEntity = _generator.CreateClassProxy<T>(new EntityProxy());
            domainEntity.QuantivEntity =  quantivEntity;

            return domainEntity;
        }

        public T Retrieve<T>(int id) where T : DomainEntityBase, new()
        {
            string idColumn = string.Format("{0}Id", ExtractEntityNameFromType(typeof(T)));

            var identifyingProperty = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(IdAttribute), true).ToList().Count > 0)
                .FirstOrDefault();

            if(identifyingProperty != null)
            {
                idColumn = identifyingProperty.Name;
            }

            var quantivEntity = _activity.GetEntityManager(ExtractEntityNameFromType(typeof(T)))
                .CreateEntityRetriever()
                .Retrieve(idColumn, id);

            var domainEntity = _generator.CreateClassProxy<T>(new EntityProxy());
            domainEntity.QuantivEntity = quantivEntity;

            return domainEntity;
        }

        public T Retrieve<T>(string propertyName, object value) where T : DomainEntityBase, new()
        {
            var quantivEntity = _activity.GetEntityManager(ExtractEntityNameFromType(typeof(T)))
                .CreateEntityRetriever()
                .Retrieve(propertyName, value);

            var domainEntity = _generator.CreateClassProxy<T>(new EntityProxy());
            domainEntity.QuantivEntity = quantivEntity;

            return domainEntity;
        }

        public void SaveNewEntity(DomainEntityBase unsavedEntity)
        {
            unsavedEntity.QuantivEntity.Save(_activity);
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

        private static string ExtractEntityNameFromType(Type type)
        {
            var classRefAttribute = type.GetCustomAttributes(typeof(EntityAttribute), false).FirstOrDefault();
            return classRefAttribute != null ? ((EntityAttribute) classRefAttribute).ClassRef : type.Name;
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