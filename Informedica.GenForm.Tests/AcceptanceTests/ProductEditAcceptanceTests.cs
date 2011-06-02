using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services;
using Informedica.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;


namespace Informedica.GenForm.Tests.AcceptanceTests
{
    /// <summary>
    /// Summary description for ProductEditAcceptanceTests
    /// </summary>
    [TestClass]
    public class ProductEditAcceptanceTests
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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            GenFormApplication.Initialize();
        }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void User_can_start_with_a_new_empty_product()
        {
            var services = GetProductServices();
            Assert.IsTrue(ObjectExaminer.ObjectHasEmptyProperties(services.GetEmptyProduct()), "User could not start with empty product");
        }

        private static IProductServices GetProductServices()
        {
            return ObjectFactory.GetInstance<IProductServices>();
        }

        [TestMethod]
        public void User_can_change_fields_of_empty_product()
        {
            const string testName = "Test";
            var product = GetProductServices().GetEmptyProduct();
            product.ProductName = testName;
            Assert.AreEqual(testName, product.ProductName,"User could not change the fields of an empty product");
        }


        [Isolated]
        [TestMethod]
        public void When_user_saves_valid_product_no_error_is_thrown()
        {
            var product = GetValidProduct();

            try
            {
                GetProductServices().SaveProduct(product);

            }
            catch (System.Exception e)
            {
                Assert.Fail("Saver product returned an error " + e.Message);
            }
        }

        [Isolated]
        [TestMethod]
        public void When_user_saves_invalid_product_an_error_is_thrown()
        {
            var product = GetInvalidProduct();

            try
            {
                GetProductServices().SaveProduct(product);
                Assert.Fail("Saving an invalid product should throw an exception");
            }
// ReSharper disable EmptyGeneralCatchClause
            catch(Exception)
// ReSharper restore EmptyGeneralCatchClause
            {
                
            }
        }

        private IProduct GetInvalidProduct()
        {
            var product = GetValidProduct();
            product.PackageName = "";
            return product;
        }

        private IProduct GetValidProduct()
        {
            var product = GetProductServices().GetEmptyProduct();

            product.ProductName = "paracetamol 500 mg tablet";
            product.GenericName = "paracetamol";
            product.BrandName = "Paracetamol";
            product.ShapeName = "tablet";
            product.Quantity = 1;
            product.UnitName = "stuk";
            product.PackageName = "tablet";
            return product;
        }

        [Isolated]
        [TestMethod]
        public void User_cannot_save_product_with_mandatory_fields_not_filled_in()
        {
            var product = GetProductServices().GetEmptyProduct();

            product.ProductName = "Test";

            try
            {
                GetProductServices().SaveProduct(product);
                Assert.Fail("a non valid product should not be saved");

            }
            catch
            {
            }
        }

    }
}
