using QuantivContrib.Core;
using QuantivContrib.Core.Attributes;

namespace QuantivContrib.Test.Unit.TestDomainModel
{
    [Entity]
    public class WebDirectory : DomainEntityBase
    {
        [Id, AttributeRef]
        public virtual int WebDirectoryId { get; set; }

        [AttributeRef]
        public virtual string WebDirectoryName { get; set; }

        [AttributeRef]
        public virtual int DomainId { get; set; }

        [AttributeRef]
        public virtual bool IsReserved { get; set; }

        [AttributeRef]
        public virtual int FundraiserId { get; set; }

        [AttributeRef]
        public virtual int EventGivingGroupId { get; set; }

        [AttributeRef]
        public virtual int AffiliateDesignId { get; set; }

        [AttributeRef]
        public virtual int WhiteLabelId { get; set; }

        [AttributeRef]
        public virtual int GiftGivingGroupId { get; set; }

        [AttributeRef("EventGivingGroup/DesignId")]
        public virtual int EventGivingGroupDesignId { get; set; }

        [AttributeRef("EventGivingGroup/Status")]
        public virtual int EventGivingGroupStatus { get; set; }
    }
}