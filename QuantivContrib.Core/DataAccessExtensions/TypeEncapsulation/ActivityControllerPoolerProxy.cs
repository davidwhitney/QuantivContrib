using System;
using Quantiv.Runtime;
using QuantivContrib.Core.DataAccessExtensions.ModernWrappers;

namespace QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation
{
	public static class ActivityControllerPoolerProxy
	{
		public static void ReleaseController(object activityController)
		{
			if(activityController is QuantivActivityControllerWrapper)
			{
				ActivityControllerPooler.ReleaseController((QuantivActivityControllerWrapper) activityController);
				return;
			}

			throw new NotImplementedException("Com not yet supported.");
		}

		public static IQuantivActivityController AllocateController(string controllerPool, bool createComObject = false)
		{
			if(!createComObject)
			{
				return new QuantivActivityControllerWrapper(ActivityControllerPooler.AllocateController(controllerPool));
			}
			
			throw new NotImplementedException("Com not yet supported.");
		}
	}
}
