using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions
{
	public class ActivitySession : ActivitySessionBase
	{
		public ActivitySession(string controllerPool, string activityRef, bool autoSave)
			: base(controllerPool, activityRef, autoSave)
		{
		}

		protected override void CreateActivity()
		{
			CurrentActivityController = ActivityControllerPoolerProxy.AllocateController(ControllerPool);
			CurrentActivity = CurrentActivityController.StartActivity(ActivityRef);
			HasActiveActivity = true;
		}
	}
}