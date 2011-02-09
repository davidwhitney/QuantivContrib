using System;
using Quantiv.Runtime.COM;
using Quantiv.Runtime.Support;

namespace QuantivContrib.Core.DataAccessExtensions.Com
{
    public class ActivitySession: IDisposable, IActivitySession
    {
        public bool ReadOnly { get; set; }
        public bool AutoSave { get; set; }

        public ActivityController CurrentActivityController { get; private set; }
        public Activity CurrentActivity { get; private set; }

        private readonly string _controllerPool;
        private readonly string _activityRef;

        private bool _disposed;
        private bool _hasActiveActivity;

        public ActivitySession(string controllerPool, string activityRef, bool autoSave = false)
        {
            AutoSave = autoSave;
            
            _controllerPool = controllerPool;
            _activityRef = activityRef;

            CreateActivity();
        }
        ~ActivitySession()
        {
            Dispose(false);
        }

        public Entity Create(string entityClassRef)
        {
            return GetEntityManager(entityClassRef).CreateEntity();
        }

        public Entity Retrieve(string entityClassRef, int id, string retrievalPlanRef = null)
        {
            Entity entity;
            var success = TryRetrieve(entityClassRef, id, out entity, retrievalPlanRef);

            if(!success)
            {
                throw new InvalidOperationException(string.Format("Failed to retrieve '{0}' with Id '{1}'.", entityClassRef, id));
            }

            return entity;
        }

        public bool TryRetrieve(string entityClassRef, int id, out Entity entity, string retrievalPlanRef = null)
        {
            entity = GetEntityManager(entityClassRef).CreateEntity(false);
            var searchConditionList = entity.GetSearchConditionList();
            searchConditionList.AddCondition(entityClassRef + "Id", id, TSearchRelationType.SearchRelationType_Equal, true);

            if (!string.IsNullOrWhiteSpace(retrievalPlanRef))
            {
                entity.SetRetrievalPlan(retrievalPlanRef);
            }
            
            var success = entity.Retrieve();
            return success;
        }

        public EntityManager GetEntityManager(string entityClassRef)
        {
            return CurrentActivity.GetEntityManager(entityClassRef);
        }

        public void Commit()
        {
            Commit(true);
        }

        private void Commit(bool createNewActivity)
        {
            if (ReadOnly) { return; }

            CurrentActivityController.Post();
            EndCurrentActivity();

            if (createNewActivity)
            {
                CreateActivity();
            }
        }

        private void EndCurrentActivity()
        {
            try
            {
                CurrentActivityController.EndCurrentActivity();
                ActivityControllerPooler.ReleaseController(CurrentActivityController);
                _hasActiveActivity = false;
            }
            catch
            {
                // Not threadsafe. Apparently Quantiv Pooling is a badly implemented
            }
        }

        private void CreateActivity()
        {
            CurrentActivityController = ActivityControllerPooler.AllocateController(_controllerPool);
            CurrentActivity = CurrentActivityController.StartActivity(_activityRef);
            _hasActiveActivity = true;
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
                Commit(false);
            }

            if(_hasActiveActivity)
            {
                EndCurrentActivity();
            }

            _disposed = true;
        }
    }
}