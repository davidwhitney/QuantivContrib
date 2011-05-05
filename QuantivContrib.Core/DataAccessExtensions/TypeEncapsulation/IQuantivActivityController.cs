namespace QuantivContrib.Core.DataAccessExtensions.TypeEncapsulation
{
	public interface IQuantivActivityController
	{
		void EndCurrentActivity();
		IQuantivActivity StartActivity(string activityRef);
		void Post();
	}
}