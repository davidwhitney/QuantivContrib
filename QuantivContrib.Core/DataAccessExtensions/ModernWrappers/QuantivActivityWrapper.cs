using Quantiv.Runtime;
using QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation;

namespace QuantivContrib.Core.DataAccessExtensions.ModernWrappers
{
	public class QuantivActivityWrapper:IQuantivActivity
	{
		public Activity Activity { get; private set; }

		public QuantivActivityWrapper(Activity activity)
		{
			Activity = activity;
		}

		public IQuantivEntityManager GetEntityManager(string entityClassRef)
		{
			var manager = Activity.GetEntityManager(entityClassRef);
			return new QuantivEntityManagerWrapper(manager);
		}

		public static implicit operator Activity(QuantivActivityWrapper source)
		{
			return source.Activity;
		}
	}
}