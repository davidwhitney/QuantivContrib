using NUnit.Framework;
using QuantivContrib.Core.ApiExtensions;
using QuantivContrib.Core.DataAccessExtensions;

namespace QuantivContrib.Test.Unit.Core
{
    [TestFixture]
    public class EntityExtensionTests
    {
        [Test]
        public void MethodName_State_ExpectedResult()
        {
            using(var session = new EntityActivitySession("BB01_Donation"))
            {
                var webDirectory = session.Retrieve("WebDirectory", 1);
                var pageShortName = webDirectory.Get<string>("WebDirectoryName");
            }


            using (var session = new EntityActivitySession("BB01_Donation"))
            using (var webDirectoryEntity = new ConnectedEntity(session, "WebDirectory", 1))
            {
                var pageShortName = webDirectoryEntity.Get<string>("WebDirectoryName");
                //webDirectoryEntity.Set("WebDirectory", pageShortName);

                session.Commit();
            }
        }
    }
}
