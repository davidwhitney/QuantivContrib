using System.Collections.Generic;

namespace Quantiv.Runtime
{
    public class Entity
    {
        private readonly Dictionary<object, object> _mockDataStore;

        public Entity()
        {
            _mockDataStore = new Dictionary<object, object>();
        }

        public void Save(Activity activity)
        {
        }

        public void SetAttributeValue(string columnName, object value)
        {
            if(!_mockDataStore.ContainsKey(columnName))
            {
                _mockDataStore.Add(columnName, null);
            }
            _mockDataStore[columnName] = value;
        }

        public object GetAttributeValue(string columnName)
        {
            return _mockDataStore[columnName];
        }
    }
}