using System;
using System.Diagnostics;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Assembler.Contexts;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Linq;
using StructureMap;

namespace Informedica.NHibernate.Tests
{
    /// <summary>
    /// Summary description for NHibernateRepositoryShould
    /// </summary>
    [TestClass]
    public class NHibernateRepositoryOfTypeBrand
    {
        private TestContext testContextInstance;
        private static NHibernateRepository<Brand, Guid, BrandDto> _repository;
        private Brand _brand;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            _repository = new BrandRepository(GenFormApplication.SessionFactory);
        }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Should()
        {
            using (GetContext())
            {
                BeInstantiated();
                BeAbleToUseTypedSessionQuery();
                BeAbleGiveACountOfZeroWhenNothingSaved();
                NotThrowAnErrorWhenAddingANewBrand();
                AfterSaveReturnACountOfOne();
                BeAbleToFindTheNewBrand();
                TheFoundBrandShouldReferenceTheSameObject();
                ReturnTrueForContainsBrand();
                ReturnFalseForAnotherBrand();
                ReturnsTrueForNewBrandWithTheSameName();
                CanRemoveTheInsertedBrand();   
            }
        }

        private IDisposable GetContext()
        {
            return ObjectFactory.GetInstance<SessionContext>();
        }

        private void CanRemoveTheInsertedBrand()
        {
            _repository.Remove(_brand);
            Assert.IsTrue(_repository.Count == 0, new StackFrame().GetMethod().Name);
        }

        private void ReturnsTrueForNewBrandWithTheSameName()
        {
            Assert.IsTrue(_repository.Contains(Brand.Create(new BrandDto{ Name = _brand.Name}), new BrandComparer()), new StackFrame().GetMethod().Name);
        }


        private void ReturnFalseForAnotherBrand()
        {
            Assert.IsFalse(_repository.Contains(Brand.Create(new BrandDto())), new StackFrame().GetMethod().Name);
        }

        private void ReturnTrueForContainsBrand()
        {
            Assert.IsTrue(_repository.Contains(_brand), new StackFrame().GetMethod().Name);
        }

        private void TheFoundBrandShouldReferenceTheSameObject()
        {
            var brand = _repository.Single(x => x.Name == _brand.Name);
            Assert.AreSame(_brand, brand, new StackFrame().GetMethod().Name);
        }

        private void BeAbleToFindTheNewBrand()
        {
            var brand = _repository.Single(x => x.Name == "Dynatra");
            Assert.IsNotNull(brand, new StackFrame().GetMethod().Name);
        }

        private void AfterSaveReturnACountOfOne()
        {
            Assert.IsTrue(_repository.Count == 1, new StackFrame().GetMethod().Name);
        }


        private void BeInstantiated()
        {
            Assert.IsNotNull(_repository, new StackFrame().GetMethod().Name);
        }

        private void BeAbleToUseTypedSessionQuery()
        {
            try
            {
                var session = GenFormApplication.SessionFactory.OpenSession();
                Assert.IsTrue(session.Query<Brand>().Count() == 0);

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString(), new StackFrame().GetMethod().Name);
            }
        }

        private void BeAbleGiveACountOfZeroWhenNothingSaved()
        {
            Assert.AreEqual(0, _repository.Count, new StackFrame().GetMethod().Name);
        }

        private void NotThrowAnErrorWhenAddingANewBrand()
        {
            try
            {
                _repository.Add(GetBrand());

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString(), new StackFrame().GetMethod().Name);
            }
        }

        private Brand GetBrand()
        {
            return _brand ?? (_brand = Brand.Create(new BrandDto { Name = "Dynatra" }));
        }
    }
}
