using System;

namespace QuantivContrib.NHibernateCompatability.Test.Unit
{
    public class EventGivingGroupTestObject
    {
        private int _id;
        public virtual int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                Ref = "EventGivingGroup_" + value;
            }
        }

        public virtual string Ref { get; set; }
        public virtual string Name { get; set; }
        public virtual int Status { get; set; }
        public virtual int Consumer { get; set; }
        public virtual int? ExternalAmount { get; set; }
        public virtual int? TargetAmount { get; set; }
        public virtual bool TargetAmountReached { get; set; }
        public virtual string PhotoUrl { get; set; }
        public virtual string PhotoCaption { get; set; }
        public virtual string DisplayPhoto { get; set; }
        public virtual string DonationEmail { get; set; }
        public virtual string PersonalMessage { get; set; }
        public virtual string ThankYouMessage { get; set; }
        public virtual DateTime? ExpiryDate { get; set; }
        public virtual string ImageChoice { get; set; }
        public virtual string ImageCaption { get; set; }
        public virtual string Colour { get; set; }
        public virtual int GivingGroup { get; set; }
        public virtual bool ImpendingCompletionDateEmailSent { get; set; }
        public virtual bool ReachedCompletionDateEmailSent { get; set; }
        public virtual bool PageExpiredEmailSent { get; set; }
        public virtual bool PledgeReleaseEmailsSent { get; set; }
        public virtual bool IsTeam { get; set; }
        public virtual int FundId { get; set; }
        public virtual int EventFundraiserRevenueStreamId { get; set; }
        public virtual int WebDirectoryId { get; set; }
        public virtual int EmployerId { get; set; }
        public virtual string Attribution { get; set; }
        public virtual int DesignId { get; set; }
        public virtual bool IsCharityFunded { get; set; }
        public virtual int Team { get; set; }
        public virtual float LastIncentiveEmailAmount { get; set; }
        public virtual int DesignPaletteId { get; set; }
        public virtual string Keywords { get; set; }
        public virtual string OlrHeaderValue1 { get; set; }
        public virtual string OlrHeaderValue2 { get; set; }
        public virtual string OlrHeaderValue3 { get; set; }
        public virtual string OlrHeaderValue4 { get; set; }
        public virtual string OlrHeaderValue5 { get; set; }
        public virtual string OlrHeaderValue6 { get; set; }
        public virtual bool? OlrHeaderIsAssigned { get; set; }
        public virtual bool DhNameColumnStatus { get; set; }
        public virtual bool DhDateColumnStatus { get; set; }
        public virtual bool DhAmountTaxColumnStatus { get; set; }
        public virtual bool DhCommentColumnStatus { get; set; }
        public virtual bool? UpdateEmail { get; set; }
        public virtual bool? SubscribedToFundraisingTipsEmail { get; set; }
        public virtual bool DhImageColumnStatus { get; set; }
        public virtual decimal AmountRaised { get; set; }
        public virtual decimal AmountRaisedGiftAid { get; set; }

        public EventGivingGroupTestObject()
        {
            Name = string.Empty;
            PhotoUrl = string.Empty;
            PhotoCaption = string.Empty;
            DisplayPhoto = string.Empty;
            DonationEmail = string.Empty;
            PersonalMessage = string.Empty;
            ThankYouMessage = string.Empty;
            ExpiryDate = null;
            ImageChoice = string.Empty;
            ImageCaption = string.Empty;
            Colour = string.Empty;
            GivingGroup = 0; // Is this correct? Should we auto-generate a GivingGroup or require one in ctor?
            Attribution = string.Empty;
            DesignId = 1;
            Team = 0; // Is this correct or does every EGG have a Team created by default?
            LastIncentiveEmailAmount = 0.00f;
            DesignPaletteId = 0; // ????
        }
    }
}