using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Assembler.Contexts;
using Informedica.GenForm.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using StructureMap;

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
            Initialize();
        }

        private static void Initialize()
        {
            ObjectFactory.Configure(x => x.For<ISessionFactory>().HybridHttpOrThreadLocalScoped().Use(GenFormApplication.TestSessionFactory));
        }

        [TestInitialize]
        public void MyTestInitialize()
        {
            Context = new SessionContext();
            SessionFactoryManager.BuildSchema(Context.CurrentSession());
            Context.CurrentSession().Transaction.Begin();
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
