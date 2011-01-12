using System.Linq;
using NUnit.Framework;
using QuantivContrib.Core.LinqToQuantiv;

namespace QuantivContrib.Test.Unit.Core.LinqToQuantiv
{
    [TestFixture]
    public class EntityQueryProviderTests
    {
        [Test]
        public void MethodName_State_ExpectedResult()
        {
            var quantiv = new QuantivDatabase();

            var entity = quantiv.Load("WebDirectory").By.SearchConditions
                                   .AttributeRef("Something").EqualTo("aaa")
                                   .Fetch()
                                   .Where(x=>x.Id == 1)
                                   .FirstOrDefault();

            var entit = quantiv.Load("WebDirectory").By.Id(1)
                                   .Fetch()
                                   .Where(x=>x.Id == 1)
                                   .FirstOrDefault();
        }

    }
}
