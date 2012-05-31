using System.Web;
using System.Data;
using Informedica.GenForm.DataAccess.Databases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Databases
{
    [TestClass]
    public class AnHttpContextConnectionCacheShould
    {
        private HttpContextBase _context;
        private IDbConnection _connection;
        private HttpContextConnectionCache _cache;

        [TestInitialize]
        public void Init()
        {
            _context = Isolate.Fake.Instance<HttpContextBase>();
            _connection = Isolate.Fake.Instance<IDbConnection>();
            _cache = new HttpContextConnectionCache(_context);

            Isolate.WhenCalled(() => _context.Session[HttpContextConnectionCache.Connection] = null).ReturnRecursiveFake();
        }

        [Isolated]
        [TestMethod]
        public void UseTheHttpSessionCollectionToStoreAConnection()
        {
            _cache.SetConnection(_connection);
            Isolate.Verify.WasCalledWithExactArguments(() => _context.Session[HttpContextConnectionCache.Connection] = _connection);
            
        }

        [Isolated]
        [TestMethod]
        public void UseTheHttpSessionCollectionToRetrieveAConnection()
        {
            _cache.GetConnection();
            Isolate.Verify.WasCalledWithAnyArguments(() => _context.Session[HttpContextConnectionCache.Connection]);
        }

        [Isolated]
        [TestMethod]
        public void ReturnTrueWhenNoConnectionAndIsEmptyIsCalled()
        {
            Assert.IsTrue(_cache.IsEmpty);
        }

        [Isolated]
        [TestMethod]
        public void ReturnFalseWhenHasConnectionAndIsEmptyIsCalled()
        {
            Isolate.WhenCalled(() => _context.Session[HttpContextConnectionCache.Connection]).WillReturn(_connection);

            Assert.IsFalse(_cache.IsEmpty);
        }
    }
}
