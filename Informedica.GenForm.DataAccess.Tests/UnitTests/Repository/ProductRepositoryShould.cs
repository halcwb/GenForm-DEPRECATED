using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.DataAccess.Tests.TestBase;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Repository
{   
    /// <summary>
    ///This is a test class for ProductRepositoryTest and is intended
    ///to contain all ProductRepositoryTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProductRepositoryShould: RepositoryTestBase<IProductRepository, IProduct, Product>
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
        public void CallProductMapperToMapProductToDao()
        {
            try
            {
                Repos.Insert(Bo);
                Isolate.Verify.WasCalledWithAnyArguments(() => Mapper.MapFromBoToDao(Bo, Dao));
            }
            catch (Exception e)
            {
                AssertVerify(e, "product repository did not call product mapper to map dao to product");
                throw;
            }
        }

        [Isolated]
        [TestMethod]
        public void CallSubmitChangesOnContextToInsertProduct()
        {
            try
            {
                Repos.Insert(Bo);
                Isolate.Verify.WasCalledWithAnyArguments(() => Context.SubmitChanges());
            }
            catch (Exception e)
            {
                AssertVerify(e, "product repository did not call submit changes on context");
            }
        }

        [TestMethod]
        public void BeAbleToInsertAvalidProductWithNoSubstances()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            var product = ObjectFactory.With(dto).GetInstance<IProduct>();

            TryInsertProduct(product);
        }


        [TestMethod]
        public void NotBeAbleToInsertAnInvalidProduct()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            dto.Generic = null;
            Bo = ObjectFactory.With(dto).GetInstance<IProduct>();

            try
            {
                Repos.Insert(Bo);
                Assert.Fail("could insert invalid product");
            }
            catch (Exception e)
            {
                Assert.IsNotNull(e);
            }
        }

        [TestMethod]
        public void BeAbleToInsertValidProductWithOneSubstance()
        {

            var dto = ProductTestFixtures.GetProductDtoWithOneSubstance();
            Bo = ObjectFactory.With(dto).GetInstance<IProduct>();
            TryInsertProduct(Bo);
        }

        [TestMethod]
        public void GiveAnInsertedProductAnId()
        {
            GenFormApplication.Initialize();

            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            Bo = ObjectFactory.With(dto).GetInstance<IProduct>();
            TryInsertProduct(Bo);
            Assert.IsTrue(Bo.ProductId != 0, "Product id should not be 0");
        }

        [TestMethod]
        public void BeAbleToDeleteAJustInsertedProduct()
        {
            GenFormApplication.Initialize();

            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            Bo = ObjectFactory.With(dto).GetInstance<IProduct>();
            using (var ctx = GetContext())
            {
                Repos = new ProductRepository(ctx);
                Repos.Insert(Bo);
                Repos.Delete(Bo.ProductId);
            }
        }

        private GenFormDataContext GetContext()
        {
            var connectionString = DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GenForm);
            return ObjectFactory.With<String>(connectionString).GetInstance<GenFormDataContext>();
        }

        private void TryInsertProduct(IProduct product)
        {
            try
            {
                Repos.Insert(product);
            }
            catch (Exception e)
            {
                Assert.Fail("could not insert valid product: " + e);
            }
        }

    }
}
