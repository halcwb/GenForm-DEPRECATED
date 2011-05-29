using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Informedica.GenForm.Library.DomainModel.Products;
using StructureMap;
using TypeMock;
using TypeMock.ArrangeActAssert;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Tests
{
    
    
    /// <summary>
    ///This is a test class for ProductRepositoryTest and is intended
    ///to contain all ProductRepositoryTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProductRepositoryTest
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
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [Isolated]
        [TestMethod]
        public void Save_product_saves_calls_product_mapper_to_map_product_to_database()
        {
            var repository = CreateProductRepository();
            var product = ObjectFactory.GetInstance<IProduct>();

            var mapper = CreateFakeProductMapper();
            var dao = CreateFakeProductDao();
            Isolate.WhenCalled(() => mapper.MapFromBoToDao(product, dao)).IgnoreCall();

            var context = CreateFakeDatabaseContext();
            Isolate.WhenCalled(() => context.SubmitChanges()).IgnoreCall();

            try
            {
                repository.SaveProduct(product);
                Isolate.Verify.WasCalledWithAnyArguments(() => mapper.MapFromBoToDao(product, dao));
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(VerifyException)) throw;
                Assert.Fail("product repository did not call product mapper to map dao to product");
            }
        }

        [Isolated]
        [TestMethod]
        public void Product_repository_submits_product_to_datacontext()
        {
            var repository = CreateProductRepository();
            var product = ObjectFactory.GetInstance<IProduct>();

            var mapper = CreateFakeProductMapper();
            var dao = CreateFakeProductDao();
            Isolate.WhenCalled(() => mapper.MapFromBoToDao(product, dao)).IgnoreCall();

            var context = CreateFakeDatabaseContext();
            Isolate.WhenCalled(() => context.SubmitChanges()).IgnoreCall();

            try
            {
                repository.SaveProduct(product);
                Isolate.Verify.WasCalledWithAnyArguments(() => context.SubmitChanges());
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(VerifyException)) throw;
                Assert.Fail("product repository did not call product mapper to map dao to product");
            }

        }

        private static Product CreateFakeProductDao()
        {
            return Isolate.Fake.Instance<Product>();
        }

        private static ProductMapper CreateFakeProductMapper()
        {
            var mapper = Isolate.Fake.Instance<ProductMapper>();
            ObjectFactory.Inject(mapper);
            return mapper;
        }

        private static GenFormDataContext CreateFakeDatabaseContext()
        {
            var context = Isolate.Fake.Instance<GenFormDataContext>();
            ObjectFactory.Inject(context);

            return context;
        }

        private static ProductRepository CreateProductRepository()
        {
            return new ProductRepository();
        }

    }
}
