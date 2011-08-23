using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests.Fixtures;
using Informedica.GenForm.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    /// <summary>
    /// Summary description for ProductConstructionTests
    /// </summary>
    [TestClass]
    public class ProductConstructionTests
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
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
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
        public void ThatAValidProductCanBeConstructed()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances());
            Assert.IsTrue(ProductChecker.ProductIsValid(product));
        }

        [TestMethod]
        public void ThatAValidProductWithBrandCanBeConstructed()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances());
            Assert.IsTrue(ProductChecker.ProductIsValid(product) && 
                          ProductChecker.ProductHasBrand(product));
        }

        [TestMethod]
        public void ThatAValidProductWithShapeCanBeConstructed()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances());
            Assert.IsTrue(ProductChecker.ProductIsValid(product) && ProductChecker.ProductHasBrand(product) && ProductChecker.ProductHasShape(product));
        }

        [TestMethod]
        public void ThatAValidProductWithPackageCanBeConstructed()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances());
            Assert.IsTrue(ProductChecker.ProductIsValid(product) && 
                          ProductChecker.ProductHasBrand(product) && 
                          ProductChecker.ProductHasShape(product) && 
                          ProductChecker.ProductHasPackage(product));
        }

        [TestMethod]
        public void ThatAValidProductAssociatesShapeWithPackage()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances());
            Assert.IsTrue(ProductChecker.ProductIsValid(product) &&
                          ProductChecker.ProductHasBrand(product) &&
                          ProductChecker.ProductHasShape(product) &&
                          ProductChecker.ProductHasPackage(product) &&
                          ProductChecker.ProductAssociatesShapeWithPackage(product));
        }

        [TestMethod]
        public void ThatAValidProductWithUnitValueCanBeConstructed()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances());
            Assert.IsTrue(ProductChecker.ProductIsValid(product) &&
                          ProductChecker.ProductHasBrand(product) &&
                          ProductChecker.ProductHasShape(product) &&
                          ProductChecker.ProductHasPackage(product) &&
                          ProductChecker.ProductHasUnitValue(product));
        }

        [TestMethod]
        public void ThatAValidProductAssociatesShapeWithUnit()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances());
            Assert.IsTrue(ProductChecker.ProductIsValid(product) &&
                          ProductChecker.ProductHasBrand(product) &&
                          ProductChecker.ProductHasShape(product) &&
                          ProductChecker.ProductHasPackage(product) &&
                          ProductChecker.ProductAssociatesShapeWithPackage(product) &&
                          ProductChecker.ProductHasUnitValue(product) && 
                          ProductChecker.ProductAssociatesShapeWithUnitGroup(product));
        }

        [TestMethod]
        public void ThatAValidProductWithProductSubstanceCanBeConstructed()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithOneSubstance());
            Assert.IsTrue(ProductChecker.ProductIsValid(product) &&
                          ProductChecker.ProductHasBrand(product) &&
                          ProductChecker.ProductHasShape(product) &&
                          ProductChecker.ProductHasPackage(product) &&
                          ProductChecker.ProductAssociatesShapeWithPackage(product) &&
                          ProductChecker.ProductHasUnitValue(product) &&
                          ProductChecker.ProductAssociatesShapeWithUnitGroup(product) &&
                          ProductChecker.ProductHasProductSubstance(product));
        }

        [TestMethod]
        public void ThatAValidProductWithRoutesCanBeConstructed()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithOneSubstanceAndRoutes());
            Assert.IsTrue(ProductChecker.ProductIsValid(product) &&
                          ProductChecker.ProductHasBrand(product) &&
                          ProductChecker.ProductHasShape(product) &&
                          ProductChecker.ProductHasPackage(product) &&
                          ProductChecker.ProductAssociatesShapeWithPackage(product) &&
                          ProductChecker.ProductHasUnitValue(product) &&
                          ProductChecker.ProductAssociatesShapeWithUnitGroup(product) &&
                          ProductChecker.ProductHasProductSubstance(product) && 
                          ProductChecker.ProductHasRoutes(product));
        }
    }
}
