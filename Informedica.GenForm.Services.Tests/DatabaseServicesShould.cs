using System.Data;
using System.Web;
using Informedica.DataAccess.Configurations;
using Informedica.GenForm.DataAccess;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Library.Services.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Services.Tests
{
    [TestClass]
    public class DatabaseServicesShould
    {
        private HttpSessionStateBase _sessionState;
        private ISessionFactory _factory;
        private ISessionCache _cache;
        private IDatabaseServices _services;

        [TestInitialize]
        public void Init()
        {
            _sessionState = Isolate.Fake.Instance<HttpSessionStateBase>();
            _factory = Isolate.Fake.Instance<ISessionFactory>();

            Isolate.WhenCalled(() => _sessionState[HttpSessionCache.SessionFactorySetting]).WillReturn(_factory);
            _cache = new HttpSessionCache(_sessionState);
            ObjectFactory.Configure(x => x.For<ISessionCache>().Use(_cache));

            Isolate.Fake.StaticMethods(typeof(UserServices));
            // ReSharper disable UnusedVariable, otherwise ConfigurationManager will not be faked
            var confMan = ConfigurationManager.Instance;
            // ReSharper restore UnusedVariable
            Isolate.Fake.StaticMethods<ConfigurationManager>();

            ObjectFactory.Configure(x => x.For<IDatabaseServices>().Use<DatabaseServices>());
            _services = ObjectFactory.GetInstance<IDatabaseServices>();
        }

        [IsolatedAttribute]
        [TestMethodAttribute]
        public void ConfigureObjectFactoryWithTheSessionFactoryFromSessionCacheIfNotNull()
        {
            _services = ObjectFactory.GetInstance<IDatabaseServices>();

            _services.ConfigureSessionFactory();
            Assert.AreEqual(_factory, ObjectFactory.GetInstance<ISessionFactory>());
        }

        [Isolated]
        [TestMethod]
        public void GetTheDefaultTestGenFormEnvironmentIfNoEnvironmentInSessionState()
        {
            Isolate.WhenCalled(() => _sessionState[HttpSessionCache.EnvironmentSetting]).WillReturn("TestGenForm");

            Assert.AreEqual("TestGenForm", _cache.GetEnvironment());
        }

        [Isolated]
        [TestMethod]
        public void BuildTheDatabaseWhenTheConnectionCacheIsNotEmpty()
        {
            var connection = Isolate.Fake.Instance<IDbConnection>();
            var session = IsolateSetupDatabaseMethod(connection);

            _services.InitDatabase();
            Isolate.Verify.WasCalledWithAnyArguments(() => SessionFactoryManager.BuildSchema("TestGenForm", session));
        }

        [Isolated]
        [TestMethod]
        public void NotBuildTheDatabaseWhenTheConnectionCacheIsEmpty()
        {
            var session = IsolateSetupDatabaseMethod(null);
            
            _services.InitDatabase();
            Isolate.Verify.WasNotCalled(() => SessionFactoryManager.BuildSchema("TestGenForm", session));
        }

        [Isolated]
        [TestMethod]
        public void ConfigureTheNewDatabaseWithASystemUserAdminWithPasswordAdmin()
        {
            var connection = Isolate.Fake.Instance<IDbConnection>();
            IsolateSetupDatabaseMethod(connection);

            DatabaseServices.InitializeDatabase(_cache);
            Isolate.Verify.WasCalledWithAnyArguments(() => UserServices.ConfigureSystemUser());
        }

        private ISession IsolateSetupDatabaseMethod(IDbConnection connection)
        {
            Isolate.WhenCalled(() => _sessionState["connection"]).WillReturn(connection);
            Isolate.WhenCalled(() => _sessionState["environment"]).WillReturn("TestGenForm");

            var session = Isolate.Fake.Instance<ISession>();
            Isolate.WhenCalled(() => SessionFactoryManager.BuildSchema("TestGenForm", session)).IgnoreCall();
            return session;
        }

    }
}
