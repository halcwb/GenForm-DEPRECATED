using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Informedica.GenForm.Library.DomainModel.Products;
using StructureMap;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for BrandRepositoryTest and is intended
    ///to contain all BrandRepositoryTest Unit Tests
    ///</summary>
    [TestClass]
    public class BrandRepositoryTest
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
        public void Save_brand_calls_data_mapper_to_map_bo_to_dao()
        {
            var repos = CreateBrandRepository();
            var brand = ObjectFactory.GetInstance<IBrand>();

            var mapper = CreateFakeBrandMapper();
            var dao = CreateFakeBrandDao();
            Isolate.WhenCalled(() => mapper.MapFromBoToDao(brand, dao)).IgnoreCall();

            var context = CreateFakeDatabaseContext();
            Isolate.WhenCalled(() => context.SubmitChanges()).IgnoreCall();

            try
            {
                repos.Insert(brand);
                Isolate.Verify.WasCalledWithAnyArguments(() => mapper.MapFromBoToDao(brand, dao));
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(VerifyException)) throw;
                Assert.Fail("brand repository did not call brand mapper to map dao to bo");
            }
        }

        [Isolated]
        [TestMethod]
        public void Brand_repository_submits_brand_to_datacontext()
        {
            var repos = CreateBrandRepository();
            var brand = ObjectFactory.GetInstance<IBrand>();

            var mapper = CreateFakeBrandMapper();
            var dao = CreateFakeBrandDao();
            Isolate.WhenCalled(() => mapper.MapFromBoToDao(brand, dao)).IgnoreCall();

            var context = CreateFakeDatabaseContext();
            Isolate.WhenCalled(() => context.SubmitChanges()).IgnoreCall();

            try
            {

            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(VerifyException)) throw;
                Assert.Fail("brand repository did not call brand mapper to map brand to dao");
            }

        }

        private GenFormDataContext CreateFakeDatabaseContext()
        {
            var context = Isolate.Fake.Instance<GenFormDataContext>();
            ObjectFactory.Inject(context);

            return context;
        }

        private Database.Brand CreateFakeBrandDao()
        {
            return Isolate.Fake.Instance<Database.Brand>();
        }

        private BrandMapper CreateFakeBrandMapper()
        {
            var mapper = Isolate.Fake.Instance<BrandMapper>();
            ObjectFactory.Inject(mapper);
            return mapper;
        }

        private IBrandRepository CreateBrandRepository()
        {
            return ObjectFactory.GetInstance<IBrandRepository>();
        }
    }
}
