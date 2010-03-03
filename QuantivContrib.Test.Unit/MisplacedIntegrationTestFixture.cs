using System.Diagnostics;
using MbUnit.Framework;
using Quantiv.Runtime.Support.Enumerations;
using QuantivContrib.Core;
using QuantivContrib.Test.Unit.TestDomainModel;

namespace QuantivContrib.Test.Unit
{
    [TestFixture]
    public class MisplacedIntegrationTestFixture
    {
        [Test]
        public void FullEndToEndExample()
        {
            using (var session = new EntityActivityScope("BB01_Donation"))
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
            using (var session = new EntityActivityScope("BB01_Donation"))
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
            using (var session = new EntityActivityScope("BB01_Donation"))
            {
                var dbProc = session.BuildStoredProcedure("myProc");
                dbProc.AddInputParameter("a","b");
                var results = dbProc.Execute();
            }
        }

        [Test]
        public void CreateAndThenEditViaAutoSave()
        {
            int internalSettlementId;

            using (var session = new EntityActivityScope("BB01_Donation"))
            {
                var settlement = session.Create<Settlement>();
                settlement.CreditApprovedCount = 10;

                internalSettlementId = settlement.InternalSettlementId;
            }

            using (var session = new EntityActivityScope("BB01_Donation"))
            {
                var settlement = session.Retrieve<Settlement>(internalSettlementId);
                settlement.CreditApprovedCount = 30;
            }
        }

        [Test]
        public void CreateAndThenEdit_ShouldLooseChangesDueToReadOnlyMode()
        {
            int internalSettlementId;

            using (var session = new EntityActivityScope("BB01_Donation"))
            {
                var settlement = session.Create<Settlement>();
                settlement.CreditApprovedCount = 10;

                internalSettlementId = settlement.InternalSettlementId;
            }

            using (var session = new EntityActivityScope("BB01_Donation") { ReadOnly = true })
            {
                var settlement = session.Retrieve<Settlement>(internalSettlementId);
                settlement.CreditApprovedCount = 30;
            }
        }
    }
}
