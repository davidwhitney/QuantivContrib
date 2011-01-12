using System.Diagnostics;
using NUnit.Framework;
using Quantiv.Runtime.Support.Enumerations;
using QuantivContrib.Core;
using QuantivContrib.Core.QuantivActiveRecord;
using QuantivContrib.Test.Unit.TestDomainModel;

namespace QuantivContrib.Test.Unit
{
    [TestFixture]
    public class MisplacedIntegrationTestFixture
    {
        private const string CONTROLLER_POOL = "BB01_Donation";

        [Test]
        public void FullEndToEndExample()
        {
            using (var session = new EntityActivityScope(CONTROLLER_POOL))
            {
                var mandate = session.Create<ConsumerCreditCardMandate>();

                mandate.Amount = 50;
                mandate.ConsumerCreditCardId = 1;
                mandate.DonationOriginId = 1;
                mandate.DonationSourceId = 1;
                mandate.IsUkTaxPayer = true;
            }
        }

        [Test]
        public void CanSearchForRasha25InWebDirectory()
        {
            using (var session = new EntityActivityScope(CONTROLLER_POOL))
            {
                var searchConditions = session.BuildSearchConditionsForEntity<WebDirectory>();
                searchConditions.AddCondition("WebDirectoryName", "rasha25", SearchRelationType.Equal, true);
                searchConditions.AddCondition("DomainId", 1, SearchRelationType.Equal, true);
                searchConditions.AddCondition("IsReserved", 0, SearchRelationType.Equal, true);

                var listPreferences = new ListPreferences {RetrievalPlan = "RequestHandler", SearchConditionList = searchConditions};
                var results = session.List<WebDirectory>(listPreferences);

                Debug.WriteLine(results[0].WebDirectoryId);
                Debug.WriteLine(results[0].WebDirectoryName);
                Debug.WriteLine(results[0].EventGivingGroupDesignId);
            }
        }

        [Test]
        public void CanGetADbProc()
        {
            using (var session = new EntityActivityScope(CONTROLLER_POOL))
            {
                var dbProc = session.BuildStoredProcedure("myProc");
                dbProc.AddInputParameter("a","b");
                var results = dbProc.Execute();
            }
        }

        [Test]
        public void EntityActivityScope_WhenNewItemIsCreatedThenEdited_EditedValuesPersisted()
        {
            const int initialCreditApprovedCount = 10;
            const int ammendedCreditApprovedCount = 30;

            int createdSettlementId;
            using (var session = new EntityActivityScope(CONTROLLER_POOL))
            {
                var settlement = session.Create<Settlement>();
                settlement.CreditApprovedCount = initialCreditApprovedCount;
                createdSettlementId = settlement.InternalSettlementId;
            }

            using (var session = new EntityActivityScope(CONTROLLER_POOL))
            {
                var settlement = session.Retrieve<Settlement>(createdSettlementId);
                settlement.CreditApprovedCount = ammendedCreditApprovedCount;
            }

            using (var session = new EntityActivityScope(CONTROLLER_POOL) { ReadOnly = true })
            {
                var settlement = session.Retrieve<Settlement>(createdSettlementId);
                Assert.AreEqual(ammendedCreditApprovedCount, settlement.CreditApprovedCount, "Ammending a value failed. Did the session flush?");
            }
        }

        [Test]
        public void EntityActivityScope_WhenNewItemIsCreatedThenEditedInAReadOnlySession_EditedValuesNotPersisted()
        {
            const int initialCreditApprovedCount = 10;
            const int ammendedCreditApprovedCount = 30;

            int createdSettlementId;
            using (var session = new EntityActivityScope(CONTROLLER_POOL))
            {
                var settlement = session.Create<Settlement>();
                settlement.CreditApprovedCount = initialCreditApprovedCount;
                createdSettlementId = settlement.InternalSettlementId;
            }

            using (var session = new EntityActivityScope(CONTROLLER_POOL) { ReadOnly = true })
            {
                var settlement = session.Retrieve<Settlement>(createdSettlementId);
                settlement.CreditApprovedCount = ammendedCreditApprovedCount;
            }

            using (var session = new EntityActivityScope(CONTROLLER_POOL) { ReadOnly = true })
            {
                var settlement = session.Retrieve<Settlement>(createdSettlementId);
                Assert.AreEqual(initialCreditApprovedCount, settlement.CreditApprovedCount, "ReadOnly session saved some changes.");
            }
        }
    }
}
