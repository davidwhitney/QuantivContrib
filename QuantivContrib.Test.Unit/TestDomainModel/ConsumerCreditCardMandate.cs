using System;
using System.Reflection;
using Quantiv5UsageExamples.QuantivContrib;

namespace Quantiv5UsageExamples.DomainModel
{
    [EntityClassRef("ConsumerCreditCardMandate")]
    public class ConsumerCreditCardMandate : DomainEntityBase
    {
        [Id, AttributeRef]
        public virtual int ConsumerCreditCardMandateId { get; set; }

        [AttributeRef]
        public virtual int FundraiserRevenueStreamId { get; set; }

        [AttributeRef]
        public virtual decimal Amount { get; set; }

        [AttributeRef]
        public virtual int ConsumerCreditCardId { get; set; }

        [AttributeRef]
        public virtual int DonationOriginId { get; set; }

        [AttributeRef]
        public virtual int DonationSourceId { get; set; }

        [AttributeRef("IsUKTaxPayer")]
        public virtual bool IsUkTaxPayer { get; set; }
    }
}