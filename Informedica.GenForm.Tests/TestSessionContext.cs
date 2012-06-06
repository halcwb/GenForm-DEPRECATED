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
    public class TestSessionContext
    {
        protected SessionContext Context;
        private readonly bool _commit;

        public TestSessionContext()
        {
            
        }

        protected TestSessionContext(Boolean commit)
        {
            _commit = commit;
            Initialize();
        }

        protected ISessionFactory SessionFactory
        {
            get { return ObjectFactory.GetInstance<ISessionFactory>(); }
        }

        private static void Initialize()
        {
            //ObjectFactory.Configure(x => x.For<ISessionFactory>().HybridHttpOrThreadLocalScoped().Use(GenFormApplication.TestSessionFactory));
        }

        [TestInitialize]
        public void MyTestInitialize()
        {
            Context = new SessionContext();
            SessionFactoryManager.BuildSchema(SessionFactoryManager.Test, Context.CurrentSession());
            Context.CurrentSession().Transaction.Begin();

            var fact = Context.CurrentSession().SessionFactory;
            ObjectFactory.Configure(x => x.For<ISessionFactory>().Use(fact));
        }

        [TestMethod]
        public void TestMe()
        {
            var fact = SessionFactory;
            Assert.IsNotNull(fact);
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
