using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Informedica.GenForm.Mvc3.Environments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Context;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    [TestClass]
    public class NHibernateSessionAttributeShould
    {
        private HttpSessionState _sessionState;
        private NHibernateSessionAttribute _attribute;
        private ActionExecutedContext _fakeContext;

        [TestInitialize]
        public void Init()
        {
            _sessionState = Isolate.Fake.Instance<HttpSessionState>();
            Isolate.WhenCalled(() => _sessionState["Environment"]).WillReturn("Test");
            Isolate.WhenCalled(() => HttpContext.Current.Session).WillReturn(_sessionState);

            _fakeContext = Isolate.Fake.Instance<ActionExecutedContext>();
            _attribute = new NHibernateSessionAttribute();
        }

        [Isolated]
        [TestMethod]
        public void GetTheCurrentEnvironmentFromTheHttpSessionStateClass()
        {
            _attribute.OnActionExecuted(_fakeContext);
            Isolate.Verify.WasCalledWithAnyArguments(() => _sessionState["Environment"]);
        }


        [Isolated]
        [TestMethod]
        public void UseTheCurrentSessionContextToUnbindAndCloseTheSession()
        {
            var factory = Isolate.Fake.Instance<ISessionFactory>();
            Isolate.WhenCalled(() => MvcApplication.GetSessionFactory("Test")).WillReturn(factory);
            var session = Isolate.Fake.Instance<ISession>();
            Isolate.WhenCalled(() => CurrentSessionContext.Unbind(factory)).WillReturn(session);

            _attribute.OnActionExecuted(_fakeContext);

            Isolate.Verify.WasCalledWithAnyArguments(() => CurrentSessionContext.Unbind(factory));
            Isolate.Verify.WasCalledWithAnyArguments(() => session.Close());
        }
    }
}
