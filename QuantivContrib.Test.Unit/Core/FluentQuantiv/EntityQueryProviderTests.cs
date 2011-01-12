using System.Linq;
using NUnit.Framework;
using QuantivContrib.Core.ApiExtensions;
using QuantivContrib.Core.FluentQuantiv;

namespace QuantivContrib.Test.Unit.Core.FluentQuantiv
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
                                .Where(x => x.Get<string>("someVal") == "my val");

            var entit = quantiv.Load("WebDirectory").By.Id(1).Fetch();
        }

    }
}
