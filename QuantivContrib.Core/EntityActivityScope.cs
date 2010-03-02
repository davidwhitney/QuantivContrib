namespace QuantivContrib.Core
{
    public class EntityActivityScope: ActivityScope
    {
        public EntityActivityScope(string controllerPool) 
            : base(controllerPool, "_EntityActivity")
        {
        }
    }
}