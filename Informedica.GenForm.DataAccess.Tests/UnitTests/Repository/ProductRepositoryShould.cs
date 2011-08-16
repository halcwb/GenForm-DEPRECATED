using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.DataAccess.Tests.TestBase;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Repository
{   
    /// <summary>
    ///This is a test class for ProductRepositoryTest and is intended
    ///to contain all ProductRepositoryTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProductRepositoryShould: RepositoryTestBase<IRepositoryLinqToSql<IProduct>, IProduct>
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
            dto.GenericName = null;
            Bo = ObjectFactory.With(dto).GetInstance<IProduct>();

            try
            {
                using (Repos.Rollback)
                {
                    Repos.Insert(Bo);

                } 
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
        public void BeAbleToDeleteAJustInsertedProductWithNoSubstances()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            DeleteProduct(dto);
        }

        [TestMethod]
        public void BeAbleToDeleteJustInsertedProductWithOneSubstance()
        {
            var dto = ProductTestFixtures.GetProductDtoWithOneSubstance();
            DeleteProduct(dto);
        }

        private void DeleteProduct(ProductDto dto)
        {
            GenFormApplication.Initialize();

            Bo = ObjectFactory.With(dto).GetInstance<IProduct>();
            using (var ctx = GetContext())
            {
                ctx.Connection.Open();
                ctx.Transaction = ctx.Connection.BeginTransaction();
                var repos = new RepositoryLinqToSql<IProduct>();
                try
                {
                    repos.Insert(ctx, Bo);
                }
                catch (Exception e)
                {
                    ctx.Transaction.Rollback();
                    Assert.Fail(e.ToString());
                } 
                ctx.Transaction.Rollback();
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
                using (Repos.Rollback)
                {
                    Repos.Insert(product);
                }
            }
            catch (Exception e)
            {
                Assert.Fail("could not insert valid product: " + e);
            }
        }

    }
}
