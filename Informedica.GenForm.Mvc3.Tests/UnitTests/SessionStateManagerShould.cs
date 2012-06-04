using System.Data;
using System.Web;
using Informedica.DataAccess.Configurations;
using Informedica.GenForm.DataAccess;
using Informedica.GenForm.Mvc3.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    [TestClass]
    public class SessionStateManagerShould
    {
        private HttpSessionStateBase _sessionState;
        private ISessionFactory _factory;
        private IDbConnection _connection;

        [TestInitialize]
        public void Init()
        {
            _sessionState = Isolate.Fake.Instance<HttpSessionStateBase>();
            _factory = Isolate.Fake.Instance<ISessionFactory>();
            _connection = null;
        }

        [Isolated]
        [TestMethod]
        public void ConfigureObjectFactoryWithTheSessionFactoryFromHttpSessionStateIfNotNull()
        {
            Isolate.WhenCalled(() => _sessionState[SessionStateManager.SessionFactorySetting]).WillReturn(_factory);
            Isolate.WhenCalled(() => _sessionState[SessionStateManager.ConnectionSetting]).WillReturn(_connection);
            
            SessionStateManager.UseSessionFactoryFromApplicationOrSessionState(_sessionState);
            Assert.AreEqual(_factory, ObjectFactory.GetInstance<ISessionFactory>());
        }

        [Isolated]
        [TestMethod]
        public void GetTheConnectionFromHttpSessionStateIfNotNull()
        {
            Isolate.WhenCalled(() => _sessionState[SessionStateManager.ConnectionSetting]).WillReturn(_connection);

            Assert.AreEqual(_connection, SessionStateManager.GetConnectionFromSessionState(_sessionState));
        }

        [Isolated]
        [TestMethod]
        public void GetTheSessionFactoryFromTheSessionStateIfNotNull()
        {
            Isolate.WhenCalled(() => _sessionState[SessionStateManager.SessionFactorySetting]).WillReturn(_factory);

            Assert.AreEqual(_factory, SessionStateManager.GetSessionFactoryFromSessionState(_sessionState));
        }

        [Isolated]
        [TestMethod]
        public void GetTheEnvironmentFromTheSessionStateIfSet()
        {
            Isolate.WhenCalled(() => _sessionState[SessionStateManager.EnvironmentSetting]).WillReturn("Test");

            Assert.AreEqual("Test", SessionStateManager.GetEnvironment(_sessionState));
        }

        [Isolated]
        [TestMethod]
        public void GetTheDefaultTestGenFormEnvironmentIfNoEnvironmentInSessionState()
        {
            Assert.AreEqual("TestGenForm", SessionStateManager.GetEnvironment(_sessionState));
        }

        [Isolated]
        [TestMethod]
        public void SetTheEnvironenmentToTheSessionStateIfNotNull()
        {
            Isolate.WhenCalled(() => _sessionState[SessionStateManager.EnvironmentSetting]).WillReturn("Test");

            SessionStateManager.SetEnvironment("Test", _sessionState);

            Isolate.Verify.WasCalledWithAnyArguments(() => _sessionState[SessionStateManager.EnvironmentSetting] = null);
            Assert.AreEqual("Test", SessionStateManager.GetEnvironment(_sessionState));
        }

        [Isolated]
        [TestMethod]
        public void BuildTheDatabaseWhenTheConnectionCacheIsNotEmpty()
        {
            var connection = Isolate.Fake.Instance<IDbConnection>();
            Isolate.WhenCalled(() => _sessionState["connection"]).WillReturn(connection);
            Isolate.WhenCalled(() => _sessionState["environment"]).WillReturn("TestGenForm");
// ReSharper disable UnusedVariable, otherwise ConfigurationManager will not be faked
            var confMan = ConfigurationManager.Instance;
// ReSharper restore UnusedVariable
            Isolate.Fake.StaticMethods<ConfigurationManager>();

            var session = Isolate.Fake.Instance<ISession>();
            Isolate.WhenCalled(() => SessionFactoryManager.BuildSchema("TestGenForm", session)).IgnoreCall();

            SessionStateManager.SetupDatabase(_sessionState);
            Isolate.Verify.WasCalledWithAnyArguments(() => SessionFactoryManager.BuildSchema("TestGenForm", session));
        }

        [Isolated]
        [TestMethod]
        public void NotBuildTheDatabaseWhenTheConnectionCacheIsEmpty()
        {
            Isolate.WhenCalled(() => _sessionState["connection"]).WillReturn(null);
            Isolate.WhenCalled(() => _sessionState["environment"]).WillReturn("TestGenForm");
// ReSharper disable UnusedVariable, otherwise ConfigurationManager will not be faked
            var confMan = ConfigurationManager.Instance;
// ReSharper restore UnusedVariable
            Isolate.Fake.StaticMethods<ConfigurationManager>();

            var session = Isolate.Fake.Instance<ISession>();
            Isolate.WhenCalled(() => SessionFactoryManager.BuildSchema("TestGenForm", session)).IgnoreCall();

            SessionStateManager.SetupDatabase(_sessionState);
            Isolate.Verify.WasNotCalled(() => SessionFactoryManager.BuildSchema("TestGenForm", session));
        }
    }
}
