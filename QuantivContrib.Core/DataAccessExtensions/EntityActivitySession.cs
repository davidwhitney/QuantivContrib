namespace QuantivContrib.Core.DataAccessExtensions
{
    public class EntityActivitySession: ActivitySession
    {
        public EntityActivitySession(string controllerPool, bool autoSave = false) 
            : base(controllerPool, "_EntityActivity", autoSave)
        {
        }
    }
}