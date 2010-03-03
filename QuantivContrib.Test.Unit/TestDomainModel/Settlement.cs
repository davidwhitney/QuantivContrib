using QuantivContrib.Core;
using QuantivContrib.Core.Attributes;

namespace QuantivContrib.Test.Unit.TestDomainModel
{
    [Entity]
    public class Settlement : DomainEntityBase
    {
        [Id, AttributeRef]
        public virtual int InternalSettlementId { get; set; }

        [AttributeRef]
        public virtual string SettlementId { get; set; }

        [AttributeRef]
        public virtual System.DateTime? SettlementDate { get; set; }

        [AttributeRef]
        public virtual decimal CreditTotal { get; set; }

        [AttributeRef]
        public virtual int CreditApprovedCount { get; set; }

        [AttributeRef]
        public virtual int CreditDeclinedCount { get; set; }

        [AttributeRef]
        public virtual string DepositTotal { get; set; }

        [AttributeRef]
        public virtual int DepositApprovedCount { get; set; }

        [AttributeRef]
        public virtual int DepositDeclinedCount { get; set; }

        [AttributeRef]
        public virtual string SettlementStateCode { get; set; }

        [AttributeRef]
        public virtual string SettlementDescription { get; set; }

        [AttributeRef]
        public virtual string SettlementProcessorName { get; set; }

        [AttributeRef]
        public virtual int ProcessStatusId { get; set; }
    }
}