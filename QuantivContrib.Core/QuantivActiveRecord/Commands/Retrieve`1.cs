using System.Linq;
using Quantiv.Runtime;
using QuantivContrib.Core.QuantivActiveRecord.Attributes;

namespace QuantivContrib.Core.QuantivActiveRecord.Commands
{
    public class Retrieve<TTypeOfObjectToRetrieve> : DomainEntityRepositoryCommandBase<TTypeOfObjectToRetrieve> where TTypeOfObjectToRetrieve : DomainEntityBase, new()
    {
        public string FetchByPropertyName { get; private set; }
        public object FetchValue { get; private set; }
        public string RetrievalPlan { get; set; }

        private readonly bool _fetchById;

        public Retrieve(int id)
        {
            FetchValue = id;
            _fetchById = true;
        }

        public Retrieve(string propertyName, object value)
        {
            FetchByPropertyName = propertyName;
            FetchValue = value;
        }

        public override TTypeOfObjectToRetrieve Execute(Activity activity)
        {
            if(_fetchById)
            {
                FetchByPropertyName = DetermineIdFieldName();
            }

            var quantivEntityRetriever = activity.GetEntityManager(ExtractEntityNameFromType(typeof (TTypeOfObjectToRetrieve))).CreateEntityRetriever();

            ConfigureRetrivalPlan(quantivEntityRetriever);
            var quantivEntity = quantivEntityRetriever.Retrieve(FetchByPropertyName, FetchValue);
            return CreateProxiedEntity<TTypeOfObjectToRetrieve>(quantivEntity);
        }

        private void ConfigureRetrivalPlan(EntityRetriever quantivEntityRetriever)
        {
            if(!string.IsNullOrEmpty(RetrievalPlan))
            {
                quantivEntityRetriever.RetrievalPlanRef = RetrievalPlan;
            }
        }

        private static string DetermineIdFieldName()
        {
            var idField = string.Format("{0}Id", ExtractEntityNameFromType(typeof(TTypeOfObjectToRetrieve)));

            var identifyingProperty = typeof(TTypeOfObjectToRetrieve).GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(IdAttribute), true).ToList().Count > 0)
                .FirstOrDefault();

            if (identifyingProperty != null)
            {
                idField = identifyingProperty.Name;
            }

            return idField;
        }
    }
}
