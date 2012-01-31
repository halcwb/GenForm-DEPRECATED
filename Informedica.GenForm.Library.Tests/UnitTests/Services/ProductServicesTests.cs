using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.TestFixtures.Fixtures;
using Informedica.GenForm.Tests;
using Informedica.GenForm.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Services
{
    /// <summary>
    /// Summary description for ProductServicesTests
    /// </summary>
    [TestClass]
    public class ProductServicesTests : TestSessionContext
    {
        public ProductServicesTests() : base(true) {}

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext) { GenFormApplication.Initialize(); }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ThatAProductCanBeGet()
        {
            var product = GetProduct();
            Assert.IsInstanceOfType(product, typeof(Product));
        }

        private static IProduct GetProduct()
        {
            var product = ProductServices.WithDto(ProductTestFixtures.GetProductDtoWithTwoSubstancesAndRoute()).Get();
            return product;
        }

        [TestMethod]
        public void ThatProductHasShape()
        {
            var product = GetProduct();
            
            Assert.IsTrue(ProductChecker.ProductHasShape(product));
        }

        [TestMethod]
        public void ThatProductHasBrand()
        {
            var product = GetProduct();
            Assert.IsTrue(ProductChecker.ProductHasBrand(product));
        }

        [TestMethod]
        public void ThatProductHasPackage()
        {
            var product = GetProduct();
            Assert.IsTrue(ProductChecker.ProductHasPackage(product));
        }

        [TestMethod]
        public void ThatProductHasUnitValue()
        {
            var product = GetProduct();
            Assert.IsTrue(ProductChecker.ProductHasUnitValue(product));
        }

        [TestMethod]
        public void ThatProductAssociatesShapeAndPackage()
        {
            var product = GetProduct();
            Assert.IsTrue(ProductChecker.ProductAssociatesShapeWithPackage(product));
        }

        [TestMethod]
        public void ThatProductAssociatesShapeAndUnit()
        {
            var product = GetProduct();
            Assert.IsTrue(ProductChecker.ProductAssociatesShapeWithUnitGroup(product));
        }

        [TestMethod]
        public void ThatProductWithSubstancesCanBeGet()
        {
            var product = GetProduct();
            Assert.IsTrue(ProductChecker.ProductIsValid(product) && ProductChecker.ProductHasProductSubstance(product));
        }

        [TestMethod]
        public void ThatProductWithRoutesCanBeGet()
        {
            var product = GetProduct();
            Assert.IsTrue(ProductChecker.ProductIsValid(product) && ProductChecker.ProductHasRoutes(product));
        }

        [TestMethod]
        public void ThatProductWithSubstancesAndRoutesCanBeGet()
        {
            var product = GetProduct();
            Assert.IsTrue(ProductChecker.ProductIsValid(product) && ProductChecker.CheckAll(product));
        }

        [TestMethod]
        public void ThatAProductCanBeDeleted()
        {
            var product = GetProduct();
            ProductServices.Delete(product);
            Assert.IsNull(ProductServices.Products.SingleOrDefault(x => x.Name == product.Name));
        }

        [TestMethod]
        public void ThatAProductWithOnlyRoutesCanBeDeleted()
        {
            var product = GetProduct();
            product.RemoveRoute(product.Routes.First());
            ProductServices.Delete(product);
            product = ProductServices.Products.SingleOrDefault(x => x.Name == ProductTestFixtures.ProductName);
            Assert.IsNull(product);
        }

        [TestMethod]
        public void ThatAProductWithOneSubstanceCanBeDeleted()
        {
            var product = GetProduct();
            Assert.IsTrue(product.Substances.Any(), "substances were not added");
            ProductServices.Delete(product);

            product = ProductServices.Products.SingleOrDefault(x => x.Name == ProductTestFixtures.ProductName);
            Assert.IsNull(product, "product was not deleted");
        }

        [TestMethod]
        public void ThatProductWithSubstancesAndRoutesCanBeDeleted()
        {
            var product = GetProduct();
            Assert.IsTrue(product.Substances.Any(), "substances were not added");

            ProductServices.Delete(product);
            product = ProductServices.Products.SingleOrDefault(x => x.Name == ProductTestFixtures.ProductName);
            Assert.IsTrue(product == null, "product was not deleted");
        }
    }
}
