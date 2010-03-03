using MbUnit.Framework;
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

                var amount = mandate.Amount;

                mandate.ConsumerCreditCardId = 1;
                mandate.DonationOriginId = 1;
                mandate.DonationSourceId = 1;
                mandate.IsUkTaxPayer = true;

                session.SaveNewEntity(mandate);
            }
        }
    }
}
