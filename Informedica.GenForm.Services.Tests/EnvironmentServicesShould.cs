using System;
using System.Linq;
using System.Web;
using Informedica.DataAccess.Configurations;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Services.Environments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Services.Tests
{
    [TestClass]
    public class EnvironmentServicesShould
    {
        private HttpSessionStateBase _context;
        private HttpSessionStateCache _stateCache;

        [TestInitialize]
        public void Init()
        {
            _context = Isolate.Fake.Instance<HttpSessionStateBase>();
            _stateCache = new HttpSessionStateCache(_context);
        }

        [TestMethod]
        public void ReturnAListOfGenFormEnvironmentsWithAtLeastATestGenForm()
        {
            var list = EnvironmentServices.GetEnvironments();
            Assert.IsTrue(list.Any(e => e.Name == "TestGenForm"));
        }

        [Isolated]
        [TestMethod]
        public void ConfigureObjectFactoryWithAHttpConnectionCacheToCacheAConnection()
        {
            Isolate.Fake.StaticMethods(typeof(ObjectFactory));

            EnvironmentServices.SetHttpSessionCache(_context);
            Isolate.Verify.WasCalledWithAnyArguments(() => ObjectFactory.Configure(x => x.For<IConnectionCache>().Use(_stateCache)));
        }

        [Isolated]
        [TestMethod]
        public void MakeSureObjectFactoryReturnsAConnectionCacheWhenSetHttpContextIsCalled()
        {
            EnvironmentServices.SetHttpSessionCache(_context);
            try
            {
                ObjectFactory.GetInstance<IConnectionCache>().GetConnection();

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }
    }
}
