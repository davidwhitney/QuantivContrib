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

            /*var entity = quantiv.Load("WebDirectory").By.SearchConditions
                                .AttributeRef("Something").EqualTo("aaa")
                                .Fetch()
                                .Where(x => x.Get<string>("someVal") == "my val")
                                .Where(x => x.EntityName == "WebDirectory");*/


            //var ent = quantiv.Where(x => x.EntityName == "WebDirectory").ToList();
/*
            var query = new QuantivEntityQuery("WebDirectory").Where(x => x.EntityId == 1)
                                                              .Where(x => x.Get<string>("WebDirectoryName") == "davids-page")
                                                              .FirstOrDefault();*/

            var query2 = new QuantivEntityQuery("WebDirectory").Where(x => x.EntityId == 1 && x.Get<string>("WebDirectoryName") == "davids-page")
                                                               .FirstOrDefault();


            //var entit = quantiv.Load("WebDirectory").By.Id(1).Fetch();
        }

    }
}
