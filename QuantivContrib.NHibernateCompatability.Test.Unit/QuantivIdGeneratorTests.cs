using NUnit.Framework;

namespace QuantivContrib.NHibernateCompatability.Test.Unit
{
    [TestFixture]
    public class QuantivIdGeneratorTests
    {
        [Test]
        public void Generate_State_ExpectedResult()
        {
            using(var session = SessionProvider.Instance.OpenSession())
            {
                var egg = new EventGivingGroupTestObject {Name = "Counter test update"};
                session.SaveOrUpdate(egg);
                session.Flush();
            }
            
        }
    }
}