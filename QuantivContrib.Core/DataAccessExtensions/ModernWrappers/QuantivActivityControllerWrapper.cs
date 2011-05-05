using System;
using Quantiv.Runtime;
using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions.ModernWrappers
{
	public class QuantivActivityControllerWrapper: IQuantivActivityController
	{
		public ActivityController ActivityController { get; private set; }

		public QuantivActivityControllerWrapper(ActivityController activityController)
		{
			ActivityController = activityController;
		}

		public void EndCurrentActivity()
		{
			ActivityController.EndCurrentActivity();
		}

		public IQuantivActivity StartActivity(string activityRef)
		{
			return new QuantivActivityWrapper(ActivityController.StartActivity(activityRef));
		}

		public void Post()
		{
			ActivityController.Post();
		}

		public static implicit operator ActivityController(QuantivActivityControllerWrapper source)
		{
			return source.ActivityController;
		}
	}
}