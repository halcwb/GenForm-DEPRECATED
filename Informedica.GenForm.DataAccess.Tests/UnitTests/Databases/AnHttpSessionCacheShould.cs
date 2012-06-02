using System.Web;
using System.Data;
using Informedica.DataAccess.Configurations;
using Informedica.GenForm.DataAccess.Databases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Databases
{
    [TestClass]
    public class AnHttpSessionCacheShould
    {
        private HttpSessionStateBase _session;
        private IDbConnection _connection;
        private IConnectionCache _cache;

        [TestInitialize]
        public void Init()
        {
            _session = Isolate.Fake.Instance<HttpSessionStateBase>();
            _connection = Isolate.Fake.Instance<IDbConnection>();
            _cache = new HttpSessionCache(_session);

            Isolate.WhenCalled(() => _session[HttpSessionCache.Connection] = null).ReturnRecursiveFake();
        }

        [Isolated]
        [TestMethod]
        public void UseTheHttpSessionCollectionToStoreAConnection()
        {
            _cache.SetConnection(_connection);
            Isolate.Verify.WasCalledWithExactArguments(() => _session[HttpSessionCache.Connection] = _connection);
            
        }

        [Isolated]
        [TestMethod]
        public void UseTheHttpSessionCollectionToRetrieveAConnection()
        {
            _cache.GetConnection();
            Isolate.Verify.WasCalledWithAnyArguments(() => _session[HttpSessionCache.Connection]);
        }

        [Isolated]
        [TestMethod]
        public void ReturnTrueWhenNoConnectionAndIsEmptyIsCalled()
        {
            Assert.IsTrue(_cache.HasNoConnection);
        }

        [Isolated]
        [TestMethod]
        public void ReturnFalseWhenHasConnectionAndIsEmptyIsCalled()
        {
            Isolate.WhenCalled(() => _session[HttpSessionCache.Connection]).WillReturn(_connection);

            Assert.IsFalse(_cache.HasNoConnection);
        }

        [Isolated]
        [TestMethod]
        public void RemoveTheConnectionItemFromTheSessionAfterClearIsCalled()
        {
            _cache.Clear();

            Isolate.Verify.WasCalledWithAnyArguments(() => _session.Remove(HttpSessionCache.Connection));

        }
    }
}
