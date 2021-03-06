using System;
using System.Linq;
using Quantiv.Runtime;
using QuantivContrib.Core.QuantivActiveRecord.Commands;

namespace QuantivContrib.Core.QuantivActiveRecord
{
    public class ActiveRecordActivityScope: IDisposable
    {
        public bool ReadOnly { get; set; }
        public bool AutoSave { get; set; }

        private ActivityController _activityController;
        private Activity _activity;

        private readonly string _controllerPool;
        private readonly string _activityRef;

        private bool _disposed;
        private readonly System.Collections.Generic.List<DomainEntityBase> _attachedEntities;

        public ActiveRecordActivityScope(string controllerPool, string activityRef)
        {
            AutoSave = true;
            _attachedEntities = new System.Collections.Generic.List<DomainEntityBase>();
            
            _controllerPool = controllerPool;
            _activityRef = activityRef;

            CreateActivity();
        }
        ~ActiveRecordActivityScope()
        {
            Dispose(false);
        }

        public T Create<T>() where T : DomainEntityBase, new()
        {
            var entity = new Create<T>().Execute(_activity);
            _attachedEntities.Add(entity);
            return entity;
        }

        public T Retrieve<T>(int id) where T : DomainEntityBase, new()
        {
            var entity = new Retrieve<T>(id).Execute(_activity);
            _attachedEntities.Add(entity);
            return entity;
        }

        public T Retrieve<T>(string propertyName, object value) where T : DomainEntityBase, new()
        {
            var entity = new Retrieve<T>(propertyName, value).Execute(_activity);
            _attachedEntities.Add(entity);
            return entity;
        }

        public SearchConditionList BuildSearchConditionsForEntity<T>() where T : DomainEntityBase, new()
        {
            return new CreateSearchConditions<T>().Execute(_activity);
        }

        public System.Collections.Generic.List<T> List<T>() where T : DomainEntityBase, new()
        {
            return new List<T>().Execute(_activity);
        }

        public System.Collections.Generic.List<T> List<T>(string comment) where T : DomainEntityBase, new()
        {
            return List<T>(new ListPreferences {Comment = comment});
        }

        public System.Collections.Generic.List<T> List<T>(ListPreferences preferences) where T : DomainEntityBase, new()
        {
            var listCommand = new List<T>
                              {
                                  SearchConditions = preferences.SearchConditionList,
                                  RetrievalPlan = preferences.RetrievalPlan,
                                  RetrievalComment = preferences.Comment
                              };

            var collection = listCommand.Execute(_activity);

            foreach (var entity in collection)
            {
                _attachedEntities.Add(entity);
            }

            return collection;
        }

        public CustomDBProc BuildStoredProcedure(string procName)
        {
            return _activity.CreateCustomDBProc(procName);
        }

        public void Save(DomainEntityBase unsavedEntity)
        {
            new Save(unsavedEntity).Execute(_activity);
        }

        public void Commit()
        {
            Commit(true);
        }

        private void Commit(bool createNewActivity)
        {
            if (ReadOnly) { return; }

            _activityController.Post();

            try
            {
                ActivityControllerPooler.ReleaseController(_activityController);
            }
            catch
            {
                // Not threadsafe. Apparently Quantiv Pooling is a badly implemented
            }

            if (createNewActivity)
            {
                CreateActivity();
            }
        }

        private void AutoSaveDirtyEntities()
        {
            foreach (var entity in _attachedEntities.Where(entity => entity.Dirty))
            {
                new Save(entity).Execute(_activity);
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
            if (_disposed) { return; }

            if (disposing && AutoSave)
            {
                AutoSaveDirtyEntities();
                Commit(false);
            }

            _disposed = true;
        }
    }
}