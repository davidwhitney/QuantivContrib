using System;
using System.Diagnostics;
using Quantiv.Runtime;

namespace QuantivContrib.Core.DataAccessExtensions
{
	public class ActivityScope : IDisposable
	{
		public ActivityController ActivityController { get; private set; }
		public Activity Activity { get; private set; }

		private readonly string _controllerPool;
		private readonly string _activityRef;

		public ActivityScope(string controllerPool, string activityRef)
		{
			_controllerPool = controllerPool;
			_activityRef = activityRef;

			CreateActivity();
		}
		~ActivityScope()
		{
			FreeActivityController();
		}

		public void Post()
		{
			ActivityController.Post();
		}

		private void CreateActivity()
		{
			ActivityController = ActivityControllerPooler.AllocateController(_controllerPool);
			Activity = ActivityController.StartActivity(_activityRef);
		}

		public void Dispose()
		{
			FreeActivityController();
			GC.SuppressFinalize(this);
		}

		private void FreeActivityController()
		{
			try
			{
				ActivityController.EndCurrentActivity();
				ActivityControllerPooler.ReleaseController(ActivityController);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Failed to free activity controller in activity scope. " + ex);
			}
		}
	}
}