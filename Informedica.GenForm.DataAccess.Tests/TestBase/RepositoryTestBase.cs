﻿using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.DataAccess.Tests.TestBase
{
    [TestClass]
    public abstract class RepositoryTestBase<TRepos, TBo,TDao> 
        where TRepos: IRepository<TBo>
        where TBo:class 
        where TDao:class 

    {
        protected TRepos Repos;
        protected TBo Bo;
        protected IDataMapper<TBo, TDao> Mapper;
        protected TDao Dao;
        protected GenFormDataContext Context;

        protected RepositoryTestBase(){ IsolateRepositoryFromContext(); }

        private void IsolateRepositoryFromContext()
        {
            InitializeGenForm();
            SetupRepository();
            IsolateFromMapper();
            IsolateFromDataContext();
        }

        private static void InitializeGenForm()
        {
            GenFormApplication.Initialize();
        }

        private void IsolateFromDataContext()
        {
            Context = CreateFakeDatabaseContext();
            Isolate.WhenCalled(() => Context.SubmitChanges()).IgnoreCall();
        }

        private void IsolateFromMapper()
        {
            Mapper = CreateFakeMapper();
            Dao = CreateFakeDao();
            Isolate.WhenCalled(() => Mapper.MapFromBoToDao(Bo, Dao)).IgnoreCall();
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

        private static TDao CreateFakeDao()
        {
            return Isolate.Fake.Instance<TDao>();
        }

        private static IDataMapper<TBo, TDao> CreateFakeMapper()
        {
            var mapper = Isolate.Fake.Instance<IDataMapper<TBo,TDao>>();
            ObjectFactory.Inject(mapper);
            return mapper;
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