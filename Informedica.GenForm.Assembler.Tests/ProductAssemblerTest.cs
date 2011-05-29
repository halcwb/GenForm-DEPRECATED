using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.Assembler.Tests
{
    
    
    /// <summary>
    ///This is a test class for ProductAssemblerTest and is intended
    ///to contain all ProductAssemblerTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProductAssemblerTest
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


        [TestMethod]
        public void An_implementation_for_product_can_be_get()
        {
            Assert.IsInstanceOfType(ObjectFactory.GetInstance<IProduct>(), typeof (IProduct), "no implementation for product was found");
        }

        [TestMethod]
        public void An_implementation_for_product_services_can_be_get()
        {
            Assert.IsInstanceOfType(ObjectFactory.GetInstance<IProductServices>(), typeof(IProductServices), "no implementation for product services was found");
        }

        [TestMethod]
        public void An_implementation_for_product_repository_can_be_get()
        {
            Assert.IsInstanceOfType(ObjectFactory.GetInstance<IProductRepository>(), typeof(IProductRepository), "no implementation for product repository was found");
        }
    }
}
