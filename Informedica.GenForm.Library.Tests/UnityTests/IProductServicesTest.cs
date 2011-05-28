using System.Reflection;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.IoC.Factory;
using Informedica.GenForm.Library.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Informedica.Utilities;
using Informedica.GenForm.Library.DomainModel.Products;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Library.Tests.UnityTests
{
    
    
    /// <summary>
    ///This is a test class for IProductServicesTest and is intended
    ///to contain all IProductServicesTest Unit Tests
    ///</summary>
    [TestClass()]
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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            ProductAssembler.RegisterDependencies();
        }
        
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        internal virtual IProductServices CreateIProductServices()
        {
            return LibraryObjectFactory.GetImplementationFor<IProductServices>();
        }

        /// <summary>
        ///A test for GetEmptyProduct
        ///</summary>
        [TestMethod()]
        public void Product_services_can_return_an_empty_product()
        {
            IProductServices services = CreateIProductServices(); 
            Assert.IsTrue(ObjectExaminer.ObjectHasEmptyProperties(services.GetEmptyProduct()), "services did not return an empty product");
        }

        [Isolated]
        [TestMethod]
        public void Product_services_use_product_repository_to_save_product()
        {
            
        }

        [TestMethod]
        public void Test_helper_method_to_determine_whether_product_is_empty()
        {
            IProduct product = LibraryObjectFactory.GetImplementationFor<IProduct>();
            Assert.IsTrue(ObjectExaminer.ObjectHasEmptyProperties(product), "helper method should return true");

            product.ProductName = "Not empty";
            Assert.IsFalse(ObjectExaminer.ObjectHasEmptyProperties(product), "helper method should return false");
        }

    }
}
