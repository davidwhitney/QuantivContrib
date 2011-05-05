using System;
using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions
{
    public abstract class ActivitySessionBase: IDisposable, IActivitySession
    {
        public bool ReadOnly { get; set; }
        public bool AutoSave { get; set; }

        public IQuantivActivityController CurrentActivityController { get; protected set; }
        public IQuantivActivity CurrentActivity { get; protected set; }

    	protected readonly string ControllerPool;
    	protected readonly string ActivityRef;
		protected bool HasActiveActivity;

        private bool _disposed;

    	protected ActivitySessionBase(string controllerPool, string activityRef, bool autoSave = false)
        {
            AutoSave = autoSave;
            
            ControllerPool = controllerPool;
            ActivityRef = activityRef;

            CreateActivity();
        }
        ~ActivitySessionBase()
        {
            Dispose(false);
        }

        public IQuantivEntity Create(string entityClassRef)
        {
            return GetEntityManager(entityClassRef).CreateEntity();
        }

        public IQuantivEntity Retrieve(string entityClassRef, int id, string retrievalPlanRef = null)
        {
            var manager = GetEntityManager(entityClassRef);
            var retriever = manager.CreateEntityRetriever();

            if (!string.IsNullOrEmpty(retrievalPlanRef))
            {
                retriever.RetrievalPlanRef = retrievalPlanRef;
            }

            return retriever.Retrieve(entityClassRef + "Id", id);
        }

    	public bool TryRetrieve(string entityClassRef, int id, out IQuantivEntity entity, string retrievalPlanRef)
    	{
    		throw new NotImplementedException();
    	}

    	public void Save(IQuantivEntity entity)
        {
            if (!HasActiveActivity)
            {
                throw new InvalidOperationException("There must be an active activity to perform a save.");
            }

            entity.Save(CurrentActivity);
        }

        public IQuantivEntityManager GetEntityManager(string entityClassRef)
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
				ActivityControllerPoolerProxy.ReleaseController(CurrentActivityController);
                HasActiveActivity = false;
            }
            catch
            {
                // Not threadsafe. Apparently Quantiv Pooling is a badly implemented
            }
        }

    	protected abstract void CreateActivity();

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

            if (HasActiveActivity)
            {
                EndCurrentActivity();
            }

            _disposed = true;
        }
    }
}