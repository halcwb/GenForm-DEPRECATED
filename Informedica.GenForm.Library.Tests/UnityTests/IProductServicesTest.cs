using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.Services;
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
    public class IProductServicesTest
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
        public void Product_services_can_return_an_empty_product()
        {
            var services = GetProductServices(); 
            Assert.IsTrue(ObjectExaminer.ObjectHasEmptyProperties(services.GetEmptyProduct()), "services did not return an empty product");
        }


        [TestMethod]
        public void Test_helper_method_to_determine_whether_product_is_empty()
        {
            IProduct product = ObjectFactory.GetInstance<IProduct>();
            Assert.IsTrue(ObjectExaminer.ObjectHasEmptyProperties(product), "helper method should return true");

            product.ProductName = "Not empty";
            Assert.IsFalse(ObjectExaminer.ObjectHasEmptyProperties(product), "helper method should return false");
        }

        [Isolated]
        [TestMethod]
        public void Save_product_calls_product_repository_to_save_the_product()
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
                if (e.GetType() != typeof(VerifyException)) throw;
                Assert.Fail("product repository was not called to save product");
            }
        }

        [Isolated]
        [TestMethod]
        public void Add_new_brand_calls_product_repository_to_add_new_brand()
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
                if (e.GetType() != typeof(VerifyException)) throw;
                Assert.Fail("Brand repository was not called to add brand");
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
