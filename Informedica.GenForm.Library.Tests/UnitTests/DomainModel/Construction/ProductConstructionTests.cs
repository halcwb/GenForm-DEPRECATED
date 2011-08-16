using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests.Fixtures;
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
            Assert.IsTrue(ProductIsValid(product));
        }

        private bool ProductIsValid(Product product)
        {
            return !String.IsNullOrWhiteSpace(product.Name) &&
                   !String.IsNullOrWhiteSpace(product.GenericName) &&
                   !String.IsNullOrWhiteSpace(product.DisplayName) &&
                   !String.IsNullOrWhiteSpace(product.ProductCode);
        }

        [TestMethod]
        public void ThatAValidProductWithBrandCanBeConstructed()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances());
            Assert.IsTrue(ProductIsValid(product) && ProductHasBrand(product));
        }

        private bool ProductHasBrand(Product product)
        {
            return product.Brand != null &&
                   !String.IsNullOrWhiteSpace(product.Brand.Name) &&
                   product.Brand.Products.Contains(product);
        }


        [TestMethod]
        public void ThatAValidProductWithShapeCanBeConstructed()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances());
            Assert.IsTrue(ProductIsValid(product) && ProductHasBrand(product) && ProductHasShape(product));
        }

        private bool ProductHasShape(Product product)
        {
            return product.Shape != null &&
                   !String.IsNullOrWhiteSpace(product.Shape.Name) &&
                   product.Shape.Products.Contains(product);
        }

        [TestMethod]
        public void ThatAValidProductWithPackageCanBeConstructed()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances());
            Assert.IsTrue(ProductIsValid(product) && 
                          ProductHasBrand(product) && 
                          ProductHasShape(product) && 
                          ProductHasPackage(product));
        }

        private bool ProductHasPackage(Product product)
        {
            return product.Package != null &&
                   !String.IsNullOrWhiteSpace(product.Package.Name) &&
                   product.Package.Products.Contains(product);
        }

        [TestMethod]
        public void ThatAValidProductAssociatesShapeWithPackage()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances());
            Assert.IsTrue(ProductIsValid(product) &&
                          ProductHasBrand(product) &&
                          ProductHasShape(product) &&
                          ProductHasPackage(product) &&
                          ProductAssociatesShapeWithPackage(product));
        }

        private bool ProductAssociatesShapeWithPackage(Product product)
        {
            return product.Shape.Packages.Contains(product.Package);
        }

        [TestMethod]
        public void ThatAValidProductWithUnitValueCanBeConstructed()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances());
            Assert.IsTrue(ProductIsValid(product) &&
                          ProductHasBrand(product) &&
                          ProductHasShape(product) &&
                          ProductHasPackage(product) &&
                          ProductHasUnitValue(product));
        }

        private bool ProductHasUnitValue(Product product)
        {
            return product.Quantity != null &&
                   product.Quantity.Value > 0 && 
                   product.Quantity.Unit != null &&
                   !String.IsNullOrWhiteSpace(product.Quantity.Unit.Name);
        }

        [TestMethod]
        public void ThatAValidProductAssociatesShapeWithUnit()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances());
            Assert.IsTrue(ProductIsValid(product) &&
                          ProductHasBrand(product) &&
                          ProductHasShape(product) &&
                          ProductHasPackage(product) &&
                          ProductAssociatesShapeWithPackage(product) &&
                          ProductHasUnitValue(product) && 
                          ProductAssociatesShapeWithUnit(product));
        }

        private bool ProductAssociatesShapeWithUnit(Product product)
        {
            return product.Shape.Units.Contains(product.Quantity.Unit);
        }

        [TestMethod]
        public void ThatAValidProductWithProductSubstanceCanBeConstructed()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithOneSubstance());
            Assert.IsTrue(ProductIsValid(product) &&
                          ProductHasBrand(product) &&
                          ProductHasShape(product) &&
                          ProductHasPackage(product) &&
                          ProductAssociatesShapeWithPackage(product) &&
                          ProductHasUnitValue(product) &&
                          ProductAssociatesShapeWithUnit(product) &&
                          ProductHasProductSubstance(product));
        }

        private bool ProductHasProductSubstance(Product product)
        {
            return product.Substances.Count() > 0 &&
                   product.Substances.First().SortOrder > 0 &&
                   product.Substances.First().Substance != null &&
                   product.Substances.First().Substance.Products.Contains(product) &&
                   product.Substances.First().Quantity != null &&
                   product.Substances.First().Quantity.Value > 0 &&
                   !String.IsNullOrWhiteSpace(product.Substances.First().Quantity.Unit.Name);
        }

        [TestMethod]
        public void ThatAValidProductWithRoutesCanBeConstructed()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithOneSubstanceAndRoutes());
            Assert.IsTrue(ProductIsValid(product) &&
                          ProductHasBrand(product) &&
                          ProductHasShape(product) &&
                          ProductHasPackage(product) &&
                          ProductAssociatesShapeWithPackage(product) &&
                          ProductHasUnitValue(product) &&
                          ProductAssociatesShapeWithUnit(product) &&
                          ProductHasProductSubstance(product) && 
                          ProductHasRoutes(product));
        }

        private bool ProductHasRoutes(Product product)
        {
            return product.Routes.Count() > 0 && 
                   !String.IsNullOrWhiteSpace(product.Routes.First().Name) &&
                   !String.IsNullOrWhiteSpace(product.Routes.First().Abbreviation) &&
                   product.Routes.First().Products.Contains(product) &&
                   product.Routes.Last().Products.Contains(product);
        }
    }
}
