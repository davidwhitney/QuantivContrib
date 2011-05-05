using Quantiv.Runtime.Support;
using QuantivContrib.Core.DataAccessExtensions.ComWrappers;

namespace QuantivContrib.Core.DataAccessExtensions
{
	class LegacyActivitySession : ActivitySessionBase
	{
		public LegacyActivitySession(string controllerPool, string activityRef, bool autoSave)
			: base(controllerPool, activityRef, autoSave)
		{
		}

		protected override void CreateActivity()
		{
			CurrentActivityController = new QuantivComActivityControllerWrapper(ActivityControllerPooler.AllocateController(ControllerPool));
			CurrentActivity = CurrentActivityController.StartActivity(ActivityRef);
			HasActiveActivity = true;
		}
	}
}