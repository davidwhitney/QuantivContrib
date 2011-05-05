using System;
using Quantiv.Runtime.COM;
using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions.ComWrappers
{
	public class QuantivComActivityControllerWrapper: IQuantivActivityController
	{
		public ActivityController ActivityController { get; private set; }

		public QuantivComActivityControllerWrapper(ActivityController activityController)
		{
			ActivityController = activityController;
		}

		public void EndCurrentActivity()
		{
			ActivityController.EndCurrentActivity();
		}

		public IQuantivActivity StartActivity(string activityRef)
		{
			return new QuantivComActivityWrapper(ActivityController.StartActivity(activityRef));
		}

		public void Post()
		{
			ActivityController.Post();
		}

		public static implicit operator ActivityController(QuantivComActivityControllerWrapper source)
		{
			return source.ActivityController;
		}
	}
}