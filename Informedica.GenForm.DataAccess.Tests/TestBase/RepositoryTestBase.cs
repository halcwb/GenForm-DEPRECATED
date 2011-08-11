using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.DataAccess.Tests.TestBase
{
    [TestClass]
    public abstract class RepositoryTestBase<TRepos, TBo> 
        where TRepos: IRepositoryLinqToSql<TBo>
        where TBo:class 

    {
        protected TRepos Repos;
        protected TBo Bo;
        protected GenFormDataContext Context;

        protected RepositoryTestBase(){ IsolateRepositoryFromContext(); }

        private void IsolateRepositoryFromContext()
        {
            InitializeGenForm();
            SetupRepository();
            IsolateFromDataContext();
        }

        protected static void InitializeGenForm()
        {
            GenFormApplication.Initialize();
        }

        private void IsolateFromDataContext()
        {
            Context = CreateFakeDatabaseContext();
            Isolate.WhenCalled(() => Context.SubmitChanges()).IgnoreCall();
        }

 
        private void SetupRepository()
        {
            Repos = CreateRepository();
            Bo = ObjectFactory.GetInstance<TBo>();
        }

        private static GenFormDataContext CreateFakeDatabaseContext()
        {
            var context = Isolate.Fake.Instance<GenFormDataContext>();
            ObjectFactory.Inject(context);
            return context;
        }

        private static TRepos CreateRepository()
        {
            return ObjectFactory.GetInstance<TRepos>();
        }

        protected void AssertVerify(Exception e, string message)
        {
            if (e.GetType() != typeof(VerifyException)) throw e;
            Assert.Fail(message + ": " + e);
        }
    }
}
