using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Informedica.GenForm.Mvc3.Environments;
using Informedica.GenForm.Services.Environments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Context;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    [TestClass]
    public class NHibernateSessionAttributeShould
    {
        private HttpSessionState _sessionState;
        private NHibernateSessionAttribute _attribute;
        private ActionExecutedContext _executedContext;
        private ActionExecutingContext _executingContext;
        private HttpContextBase _httpContext;
        private HttpSessionStateBase _sessionBase;
        private IDbConnection _connection;
        private ISessionFactory _factory;
        private ISession _session;

        [TestInitialize]
        public void Init()
        {
            _connection = Isolate.Fake.Instance<IDbConnection>();
            _factory = Isolate.Fake.Instance<ISessionFactory>();

            _sessionState = Isolate.Fake.Instance<HttpSessionState>();
            Isolate.WhenCalled(() => _sessionState["Environment"]).WillReturn("Test");
            Isolate.WhenCalled(() => HttpContext.Current.Session).WillReturn(_sessionState);

            _executedContext = Isolate.Fake.Instance<ActionExecutedContext>();
            _attribute = new NHibernateSessionAttribute();

            _executingContext = Isolate.Fake.Instance<ActionExecutingContext>();
            _httpContext = Isolate.Fake.Instance<HttpContextBase>();
            _sessionBase = Isolate.Fake.Instance<HttpSessionStateBase>();
            Isolate.WhenCalled(() => _executingContext.HttpContext).WillReturn(_httpContext);
            Isolate.WhenCalled(() => _httpContext.Session).WillReturn(_sessionBase);

            Isolate.Fake.StaticMethods<EnvironmentServices>();
            _session = Isolate.Fake.Instance<ISession>();
            Isolate.WhenCalled(() => CurrentSessionContext.Bind(_session)).IgnoreCall();
        }

        [Isolated]
        [TestMethod]
        public void UseTheSessionFactoryFromHttpSessionStateIfNotNull()
        {
            Isolate.WhenCalled(() => _sessionBase["sessionfactory"]).WillReturn(_factory);
            Isolate.WhenCalled(() => _sessionBase["connection"]).WillReturn(_connection);

            _attribute.OnActionExecuting(_executingContext);
            Isolate.Verify.WasCalledWithAnyArguments(() => _factory.OpenSession(_connection));
        }

        [Isolated]
        [TestMethod]
        public void UseTheConnectionFromHttpSessionStateIfNotNull()
        {
            Isolate.WhenCalled(() => _sessionBase["sessionfactory"]).WillReturn(_factory);
            Isolate.WhenCalled(() => _sessionBase["connection"]).WillReturn(_connection);

            _attribute.OnActionExecuting(_executingContext);
            Isolate.Verify.WasCalledWithExactArguments(() => _factory.OpenSession(_connection));
        }

        [Isolated]
        [TestMethod]
        public void UseTheSessionFactoryInTheSessionStateToUnbindASessionOnActionExecuted()
        {
            Isolate.WhenCalled(() => _sessionBase["sessionfactory"]).WillReturn(_factory);
            Isolate.WhenCalled(() => CurrentSessionContext.Unbind(_factory)).WillReturn(_session);

            _attribute.OnActionExecuting(_executingContext);
            _attribute.OnActionExecuted(_executedContext);
            Isolate.Verify.WasCalledWithExactArguments(() => CurrentSessionContext.Unbind(_factory));
        }


        [Isolated]
        [TestMethod]
        public void UseTheCurrentSessionContextToUnbindAndCloseTheSession()
        {
            var factory = Isolate.Fake.Instance<ISessionFactory>();
            Isolate.WhenCalled(() => MvcApplication.GetSessionFactory("Test")).WillReturn(factory);
            ObjectFactory.Configure(x => x.For<ISessionFactory>().Use(factory));
            var session = Isolate.Fake.Instance<ISession>();
            Isolate.WhenCalled(() => CurrentSessionContext.Unbind(factory)).WillReturn(session);

            _attribute.OnActionExecuted(_executedContext);

            Isolate.Verify.WasCalledWithAnyArguments(() => CurrentSessionContext.Unbind(factory));
            Isolate.Verify.WasCalledWithAnyArguments(() => session.Close());
        }
    }
}
