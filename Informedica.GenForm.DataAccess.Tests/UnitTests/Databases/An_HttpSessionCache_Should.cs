using System.Web;
using System.Data;
using Informedica.DataAccess.Configurations;
using Informedica.GenForm.DataAccess.Databases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Databases
{
    [TestClass]
    public class An_HttpSessionCache_Should
    {
        private HttpSessionStateBase _session;
        private IDbConnection _connection;
        private IConnectionCache _cache;

        [TestInitialize]
        public void Init()
        {
            _session = Isolate.Fake.Instance<HttpSessionStateBase>();
            _connection = Isolate.Fake.Instance<IDbConnection>();
            _cache = new HttpSessionStateCache(_session);

            Isolate.WhenCalled(() => _session[HttpSessionStateCache.ConnectionSetting] = null).ReturnRecursiveFake();
        }

        [Isolated]
        [TestMethod]
        public void UseTheHttpSessionCollectionToStoreAConnection()
        {
            _cache.SetConnection(_connection);
            Isolate.Verify.WasCalledWithExactArguments(() => _session[HttpSessionStateCache.ConnectionSetting] = _connection);
            
        }

        [Isolated]
        [TestMethod]
        public void UseTheHttpSessionCollectionToRetrieveAConnection()
        {
            _cache.GetConnection();
            Isolate.Verify.WasCalledWithAnyArguments(() => _session[HttpSessionStateCache.ConnectionSetting]);
        }

        [Isolated]
        [TestMethod]
        public void return_true_when_HasNoConnection_is_called_and_no_connection_is_cached()
        {
            Assert.IsTrue(_cache.HasNoConnection);
        }

        [Isolated]
        [TestMethod]
        public void return_false_when_HasNoConnection_is_called_and_a_connection_is_cached()
        {
            Isolate.WhenCalled(() => _session[HttpSessionStateCache.ConnectionSetting]).WillReturn(_connection);

            Assert.IsFalse(_cache.HasNoConnection);
        }

        [Isolated]
        [TestMethod]
        public void RemoveTheConnectionItemFromTheSessionAfterClearIsCalled()
        {
            _cache.Clear();

            Isolate.Verify.WasCalledWithAnyArguments(() => _session.Remove(HttpSessionStateCache.ConnectionSetting));

        }
    }
}
