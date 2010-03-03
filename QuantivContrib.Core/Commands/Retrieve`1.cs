using System.Diagnostics;
using System.Linq;
using Quantiv.Runtime;
using QuantivContrib.Core.Attributes;

namespace QuantivContrib.Core.Commands
{
    public class Retrieve<TTypeOfObjectToRetrieve> : DomainEntityRepositoryCommandBase<TTypeOfObjectToRetrieve> where TTypeOfObjectToRetrieve : DomainEntityBase, new()
    {
        public string FetchByPropertyName { get; private set; }
        public object FetchValue { get; private set; }
        public object RetrievalPlan { get; set; }

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

            Debug.WriteLine(string.Format("Retriving by: {0} and {1}", FetchByPropertyName, FetchValue));

            var quantivEntity = activity.GetEntityManager(ExtractEntityNameFromType(typeof(TTypeOfObjectToRetrieve)))
                                        .CreateEntityRetriever()
                                        .Retrieve(FetchByPropertyName, FetchValue);

            var domainEntity = ProxyGenerator.CreateClassProxy<TTypeOfObjectToRetrieve>(new EntityProxy());
            domainEntity.QuantivEntity = quantivEntity;

            return domainEntity;
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
