using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Informedica.GenForm.Library.DomainModel.Products;
using TypeMock.ArrangeActAssert;
using Brand = Informedica.GenForm.Database.Brand;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests
{   
    /// <summary>
    ///This is a test class for BrandRepositoryTest and is intended
    ///to contain all BrandRepositoryTest Unit Tests
    ///</summary>
    [TestClass]
    public class BrandRepositoryShould: RepositoryTestBase<IBrandRepository, IBrand, Brand> 
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
        public void CallBrandDataMapperToMapBrand()
        {
            try
            {
                Repos.Insert(Bo);
                Isolate.Verify.WasCalledWithAnyArguments(() => Mapper.MapFromBoToDao(Bo, Dao));
            }
            catch (Exception e)
            {
                AssertVerify(e, "brand repository did not call brand mapper to map dao to bo");
            }
        }

        [Isolated]
        [TestMethod]
        public void SubmitInsertedBrandToDatacontext()
        {
            try
            {
                Repos.Insert(Bo);
                Isolate.Verify.WasCalledWithAnyArguments(() => Context.SubmitChanges());
            }
            catch (Exception e)
            {
                AssertVerify(e, "brand repository did not call submitchanges on context");
            }

        }
    }
}
