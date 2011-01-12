using System;
using Quantiv.Runtime;

namespace QuantivContrib.Core.DataAccessExtensions
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
            var manager = GetEntityManager(entityClassRef);
            var retriever = manager.CreateEntityRetriever();

            if (!string.IsNullOrEmpty(retrievalPlanRef))
            {
                retriever.RetrievalPlanRef = retrievalPlanRef;
            }

            return retriever.Retrieve(entityClassRef + "Id", id);
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

            try
            {
                ActivityControllerPooler.ReleaseController(CurrentActivityController);
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
        
        private void CreateActivity()
        {
            CurrentActivityController = ActivityControllerPooler.AllocateController(_controllerPool);
            CurrentActivity = CurrentActivityController.StartActivity(_activityRef);
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

            _disposed = true;
        }
    }
}