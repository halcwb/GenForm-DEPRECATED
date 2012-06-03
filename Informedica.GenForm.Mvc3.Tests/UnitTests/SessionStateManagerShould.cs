using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Informedica.GenForm.Mvc3.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Context;
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
        private ISession _session;

        [TestInitialize]
        public void Init()
        {
            _sessionState = Isolate.Fake.Instance<HttpSessionStateBase>();
            _factory = Isolate.Fake.Instance<ISessionFactory>();
            _session = Isolate.Fake.Instance<ISession>();
        }

        [Isolated]
        [TestMethod]
        public void ConfigureObjectFactoryWithTheSessionFactoryFromHttpSessionStateIfNotNull()
        {
            Isolate.WhenCalled(() => _sessionState["sessionfactory"]).WillReturn(_factory);
            Isolate.WhenCalled(() => _sessionState["connection"]).WillReturn(_connection);
            
            SessionStateManager.UseSessionFactoryFromApplicationOrSessionState(_sessionState);
            Assert.AreEqual(_factory, ObjectFactory.GetInstance<ISessionFactory>());
        }

        [Isolated]
        [TestMethod]
        public void GetTheConnectionFromHttpSessionStateIfNotNull()
        {
            Isolate.WhenCalled(() => _sessionState["connection"]).WillReturn(_connection);

            Assert.AreEqual(_connection, SessionStateManager.GetConnectionFromSessionState(_sessionState));
        }

        [Isolated]
        [TestMethod]
        public void GetTheSessionFactoryFromTheSessionStateIfNotNull()
        {
            Isolate.WhenCalled(() => _sessionState["sessionfactory"]).WillReturn(_factory);

            Assert.AreEqual(_factory, SessionStateManager.GetSessionFactoryFromSessionState(_sessionState));
        }
    }
}
