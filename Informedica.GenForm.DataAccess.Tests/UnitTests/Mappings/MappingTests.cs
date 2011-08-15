using Informedica.GenForm.Assembler.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    [TestClass]
    public abstract class MappingTests
    {
        protected SessionContext _context;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _context = new SessionContext();
            _context.CurrentSession().BeginTransaction();
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _context.CurrentSession().Transaction.Rollback();
            _context.Dispose();
            _context = null;
        }
    }
}
