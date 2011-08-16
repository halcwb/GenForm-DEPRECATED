using Informedica.GenForm.Assembler.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Tests
{
    [TestClass]
    public abstract class TestSessionContext
    {
        protected SessionContext Context;

        [TestInitialize]
        public void MyTestInitialize()
        {
            Context = new SessionContext();
            Context.CurrentSession().BeginTransaction();
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            Context.CurrentSession().Transaction.Rollback();
            Context.Dispose();
            Context = null;
        }
    }
}
