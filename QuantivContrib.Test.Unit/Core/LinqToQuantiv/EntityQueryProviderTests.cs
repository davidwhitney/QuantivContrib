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

            quantiv.Load("WebDirectory").By.SearchCondition
                   .Lookup(x=> x.AttributeRef == "Something").Where(x=>x.AttributeValue == "aaa")
                   .ToList()
                   .Where(x=>x.Id == 1)
                   .FirstOrDefault();
        }

    }
}
