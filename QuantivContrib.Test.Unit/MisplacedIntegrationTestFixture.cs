using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuantivContrib.Core;
using QuantivContrib.Test.Unit.TestDomainModel;

namespace QuantivContrib.Test.Unit
{
    public class MisplacedIntegrationTestFixture
    {
        private const string ControllerPoolName = "BB01_Donation";

        public void FullEndToEndExample()
        {
            using (var session = new EntityActivityScope(ControllerPoolName))
            {
                var mandate = session.Create<ConsumerCreditCardMandate>();

                mandate.Amount = 1;
                mandate.ConsumerCreditCardId = 1;
                mandate.DonationOriginId = 1;
                mandate.DonationSourceId = 1;
                mandate.IsUkTaxPayer = true;

                session.SaveNewEntity(mandate);

                var retrievedMandate = session.Retrieve<ConsumerCreditCardMandate>(73738);
                var ukTaxPayer = retrievedMandate.IsUkTaxPayer;
            }
        }
    }
}
