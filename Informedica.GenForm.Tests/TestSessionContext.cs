using System;
using Informedica.GenForm.Assembler.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Tests
{
    [TestClass]
    public abstract class TestSessionContext
    {
        protected SessionContext Context;
        private readonly bool _commit;

        protected TestSessionContext(Boolean commit)
        {
            _commit = commit;
        }

        [TestInitialize]
        public void MyTestInitialize()
        {
            if (_commit) DatabaseCleaner.CleanDataBase();
            Context = new SessionContext();
            Context.CurrentSession().BeginTransaction();
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            try
            {
                if (_commit)
                {
                    Context.CurrentSession().Transaction.Commit();
                }
                else Context.CurrentSession().Transaction.Rollback();

            }
            catch (Exception)
            {
                Context.CurrentSession().Transaction.Rollback();
                throw;
            }
            finally
            {
                Context.Dispose();
                Context = null;
            }
        }
    }
}
