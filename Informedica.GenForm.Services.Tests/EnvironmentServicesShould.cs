using System.Linq;
using System.Web;
using Informedica.GenForm.Services.Environments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Services.Tests
{
    [TestClass]
    public class EnvironmentServicesShould
    {
        [TestMethod]
        public void ReturnAListOfGenFormEnvironmentsWithAtLeastATestGenForm()
        {
            var list = EnvironmentServices.GetEnvironments();
            Assert.IsTrue(list.Any(e => e.Name == "TestGenForm"));
        }

        [TestMethod]
        public void PutTheHttpContextInObjectFactorySoItCanBeUsedAsAConnectionCache()
        {
            var context = Isolate.Fake.Instance<HttpContextBase>();
            Isolate.Fake.StaticMethods(typeof(ObjectFactory));

            EnvironmentServices.SetHttpContext(context);
            Isolate.Verify.WasCalledWithAnyArguments(() => ObjectFactory.Configure(x => x.For<HttpContextBase>().Use(context)));
        }
    }
}
