using NUnit.Framework;
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
                                .Fetch();

            var entit = quantiv.Load("WebDirectory").By.Id(1).Fetch();
        }

    }
}
