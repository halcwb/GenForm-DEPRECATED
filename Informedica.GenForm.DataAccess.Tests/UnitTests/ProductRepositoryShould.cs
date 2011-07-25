using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Tests.TestBase;
using Informedica.GenForm.Library.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Informedica.GenForm.Library.DomainModel.Products;
using TypeMock.ArrangeActAssert;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests
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


    }
}
