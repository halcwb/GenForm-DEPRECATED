using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.Services;
using Informedica.GenForm.Mvc3.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Informedica.Utilities;
using Informedica.GenForm.Library.DomainModel.Products;
using StructureMap;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Library.Tests.UnityTests
{
    
    
    /// <summary>
    ///This is a test class for IProductServicesTest and is intended
    ///to contain all IProductServicesTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProductServicesShould
    {


        private TestContext testContextInstance;

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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            GenFormApplication.Initialize();
        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //    ProductAssembler.RegisterDependencies();
        //}
        
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        internal virtual IProductServices GetProductServices()
        {
            return ObjectFactory.GetInstance<IProductServices>();
        }

        /// <summary>
        ///A test for GetEmptyProduct
        ///</summary>
        [TestMethod]
        public void BeAbleToReturnAnEmptyProduct()
        {
            var services = GetProductServices(); 
            Assert.IsTrue(ObjectExaminer.ObjectHasEmptyProperties(services.GetEmptyProduct()), 
                          "services did not return an empty product");
        }


        [TestMethod]
        public void HaveAhelperClassToDetermineWhetherProductIsEmpty()
        {
            IProduct product = ObjectFactory.GetInstance<IProduct>();
            Assert.IsTrue(ObjectExaminer.ObjectHasEmptyProperties(product), "helper method should return true");

            product.ProductName = "Not empty";
            Assert.IsFalse(ObjectExaminer.ObjectHasEmptyProperties(product), "helper method should return false");
        }

        [Isolated]
        [TestMethod]
        public void CallProductRepositoryToSaveAdrug()
        {
            var product = ObjectFactory.GetInstance<IProduct>();

            var repos = GetFakeRepository<IProductRepository, IProduct>(product);
            ObjectFactory.Inject(repos);

            try
            {
                GetProductServices().SaveProduct(product);
                Isolate.Verify.WasCalledWithExactArguments(() => repos.Insert(product));
            }
            catch (Exception e)
            {
                AssertExceptionType(e, "product repository was not called to save product");
            }
        }

        [Isolated]
        [TestMethod]
        public void CallProductRepositoryToAddAnewBrand()
        {
            var brand = ObjectFactory.GetInstance<IBrand>();

            var repos = GetFakeRepository<IBrandRepository, IBrand>(brand);
            ObjectFactory.Inject(repos);

            try
            {
                GetProductServices().AddNewBrand(brand);
                Isolate.Verify.WasCalledWithExactArguments(() => repos.Insert(brand));
            }
            catch (Exception e)
            {
                AssertExceptionType(e, "Brand repository was not called to add brand");
            }

        }

        private void AssertExceptionType(Exception e, String message)
        {
            if (e.GetType() != typeof(VerifyException)) throw e;
            Assert.Fail(message);
        }

        [Isolated]
        [TestMethod]
        public void CallProductRepositoryToAddNewSubstance()
        {
            var substance = ObjectFactory.GetInstance<Substance>();

            var repos = GetFakeRepository<ISubstanceRepository, ISubstance>(substance);
            ObjectFactory.Inject(repos);

            try
            {
                GetProductServices().AddNewSubstance(substance);
                Isolate.Verify.WasCalledWithExactArguments(() => repos.Insert(substance));
            }
            catch (Exception e)
            {
                AssertExceptionType(e, "Substance repository was not called to save substance");
            }
        }

        [Isolated]
        [TestMethod]
        public void AddNewSubstanceToRepository()
        {
            var substance = ObjectFactory.GetInstance<ISubstance>();

            try
            {
                GetProductServices().AddNewSubstance(substance);
            }
            catch (Exception e)
            {
                Assert.Fail("substance could not be inserted in repository: " + e);
            }
        }


        private T GetFakeRepository<T, TC>(TC item) where T: IRepository<TC>
        {
            var repos = Isolate.Fake.Instance<T>();
            Isolate.WhenCalled(() => repos.Insert(item)).IgnoreCall();

            return repos;
        }

    }
}
