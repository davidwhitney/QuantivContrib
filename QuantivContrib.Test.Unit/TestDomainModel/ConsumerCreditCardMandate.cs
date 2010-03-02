using QuantivContrib.Core;
using QuantivContrib.Core.Attributes;

namespace QuantivContrib.Test.Unit.TestDomainModel
{
    [Entity("ConsumerCreditCardMandate")]
    public class ConsumerCreditCardMandate : DomainEntityBase
    {
        [Id, Attribute]
        public virtual int ConsumerCreditCardMandateId { get; set; }

        [Attribute]
        public virtual int FundraiserRevenueStreamId { get; set; }

        [Attribute]
        public virtual decimal Amount { get; set; }

        // Deliberately unmapped.
        public virtual int ConsumerCreditCardId { get; set; }

        [Attribute]
        public virtual int DonationOriginId { get; set; }

        [Attribute]
        public virtual int DonationSourceId { get; set; }

        [Attribute("IsUKTaxPayer")]
        public virtual bool IsUkTaxPayer { get; set; }
    }
}