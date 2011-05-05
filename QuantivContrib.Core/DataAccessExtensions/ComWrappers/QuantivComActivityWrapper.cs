using Quantiv.Runtime.COM;
using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions.ComWrappers
{
	public class QuantivComActivityWrapper:IQuantivActivity
	{
		public Activity Activity { get; private set; }

		public QuantivComActivityWrapper(Activity activity)
		{
			Activity = activity;
		}

		public IQuantivEntityManager GetEntityManager(string entityClassRef)
		{
			var manager = Activity.GetEntityManager(entityClassRef);
			return new QuantivComEntityManagerWrapper(manager);
		}

		public static implicit operator Activity(QuantivComActivityWrapper source)
		{
			return source.Activity;
		}
	}
}