using FluentNHibernate.Mapping;

namespace QuantivContrib.NHibernateCompatability.Test.Unit
{
    public class EventGivingGroupTestObjectMap: ClassMap<EventGivingGroupTestObject>
    {
        public EventGivingGroupTestObjectMap()
        {
            Table("BB01_EventGivingGroup");
            Id(x => x.Id).Column("EventGivingGroupId")
                .Index("EventGivingGroupId")
                .Unique()
                .GeneratedBy
                .Custom<QuantivIdGenerator>(x => x.AddParam("counterRef", "EventGivingGroup"));


            Map(x => x.Ref).Column("EventGivingGroupRef");
            Map(x => x.Name).Column("EventGivingGroupName");
            Map(x => x.Attribution);
            Map(x => x.Status);
            Map(x => x.Colour);
            Map(x => x.Consumer, "ConsumerId");
            Map(x => x.DesignId);
            Map(x => x.DesignPaletteId);
            Map(x => x.DhAmountTaxColumnStatus);
            Map(x => x.DhCommentColumnStatus);
            Map(x => x.DhDateColumnStatus);
            Map(x => x.DhImageColumnStatus);
            Map(x => x.DhNameColumnStatus);
            Map(x => x.DisplayPhoto);
            Map(x => x.DonationEmail);
            Map(x => x.EmployerId);
            Map(x => x.ExpiryDate);
            Map(x => x.ExternalAmount);
            Map(x => x.FundId);
            Map(x => x.GivingGroup, "GivingGroupId");
            Map(x => x.SubscribedToFundraisingTipsEmail, "HTEmail");
            Map(x => x.ImageCaption);
            Map(x => x.ImageChoice);
            Map(x => x.ImpendingCompletionDateEmailSent);
            Map(x => x.IsCharityFunded);
            Map(x => x.IsTeam);
            Map(x => x.Keywords);
            Map(x => x.LastIncentiveEmailAmount);
            Map(x => x.OlrHeaderIsAssigned, "OLRHeaderIsAssigned");
            Map(x => x.OlrHeaderValue1, "OLRHeaderValue1");
            Map(x => x.OlrHeaderValue2, "OLRHeaderValue2");
            Map(x => x.OlrHeaderValue3, "OLRHeaderValue3");
            Map(x => x.OlrHeaderValue4, "OLRHeaderValue4");
            Map(x => x.OlrHeaderValue5, "OLRHeaderValue5");
            Map(x => x.OlrHeaderValue6, "OLRHeaderValue6");
            Map(x => x.PageExpiredEmailSent);
            Map(x => x.PersonalMessage);
            Map(x => x.PhotoCaption);
            Map(x => x.PhotoUrl, "PhotoURL");
            Map(x => x.PledgeReleaseEmailsSent);
            Map(x => x.ReachedCompletionDateEmailSent);
            Map(x => x.TargetAmount);
            Map(x => x.TargetAmountReached);
            Map(x => x.Team, "TeamId");
            Map(x => x.ThankYouMessage);
            Map(x => x.UpdateEmail);
            Map(x => x.AmountRaised).Formula("(SELECT sum(bb01_eventcontribution.amount) from bb01_eventcontribution inner join bb01_processstatus on bb01_eventcontribution.processstatusid = bb01_processstatus.processstatusid where bb01_eventcontribution.eventgivinggroupid=EventGivingGroupId and bb01_processstatus.ProcessStatusCategory = 'Valid')").LazyLoad();
            Map(x => x.AmountRaisedGiftAid).Formula("(SELECT sum(bb01_eventcontribution.estimatedtaxreclaim) from bb01_eventcontribution inner join bb01_processstatus on bb01_eventcontribution.processstatusid = bb01_processstatus.processstatusid where bb01_eventcontribution.eventgivinggroupid=EventGivingGroupId and bb01_processstatus.ProcessStatusCategory = 'Valid')").LazyLoad();

            Map(x => x.EventFundraiserRevenueStreamId);
            Map(x => x.WebDirectoryId);
        }
        
    }
}