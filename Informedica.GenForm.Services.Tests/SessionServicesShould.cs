using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Context;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Services.Tests
{
    [TestClass]
    public class SessionServicesShould
    {
        private ISessionFactory _fact;

        [TestInitialize]
        public void Init()
        {
            _fact = Isolate.Fake.Instance<ISessionFactory>();
            ObjectFactory.Configure(x => x.For<ISessionFactory>().HybridHttpOrThreadLocalScoped().Use(_fact));

            Isolate.Fake.StaticMethods<CurrentSessionContext>();
        }

        [Isolated]
        [TestMethod]
        public void GetASessionFromTheSessionFactoryToOpenASession()
        {
            SessionServices.OpenSession();

            Isolate.Verify.WasCalledWithAnyArguments(() => _fact.OpenSession());
        }

        [Isolated]
        [TestMethod]
        public void BindTheSesssionToTheCurrentSessionWhenOpeningASession()
        {
            var session = Isolate.Fake.Instance<ISession>();

            SessionServices.OpenSession();
            Isolate.Verify.WasCalledWithAnyArguments(() => CurrentSessionContext.Bind(session));
        }

        [Isolated]
        [TestMethod]
        public void UseACachedConnectionWhenOpeningASession()
        {
            var conn = Isolate.Fake.Instance<IDbConnection>();
            ObjectFactory.Configure(x => x.For<IDbConnection>().HybridHttpOrThreadLocalScoped().Use(conn));

            SessionServices.OpenSession();
            Isolate.Verify.WasCalledWithExactArguments(() => _fact.OpenSession(conn));
        }

    }
}
