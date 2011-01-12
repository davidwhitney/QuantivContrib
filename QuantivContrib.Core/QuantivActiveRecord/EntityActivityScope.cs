namespace QuantivContrib.Core.QuantivActiveRecord
{
    public class EntityActivityScope : ActiveRecordActivityScope
    {
        public EntityActivityScope(string controllerPool) 
            : base(controllerPool, "_EntityActivity")
        {
        }
    }
}