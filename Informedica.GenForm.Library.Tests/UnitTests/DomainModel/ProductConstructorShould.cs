using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    /// <summary>
    /// Summary description for ProductConstructorShould
    /// </summary>
    [TestClass]
    public class ProductConstructorShould
    {
        private TestContext testContextInstance;
        private ProductDto _dto;
        private Product _product;

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
        public void ConstructAnewProductUsingAnEmptyDto()
        {
            try
            {
                var product = new Product(new ProductDto());
                Assert.IsNotNull(product);
                
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void PopulateProductUsingDtoWhithoutSubstances()
        {
            SetupDtoWithoutSubstances();

            Assert.IsTrue(ProductIsPopulated(), "product was not populated");
        }

        private void SetupDtoWithoutSubstances()
        {
            _dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            _product = new Product(_dto);
        }

        [TestMethod]
        public void NotLetProductChangeWhenDtoChanges()
        {
            SetupDtoWithoutSubstances();
            _dto.PackageName = "different";
            Assert.IsFalse(ProductIsPopulated(), "product was changed by change in dto!");
        }

        [TestMethod]
        public void PopulateProductWithOneSubstance()
        {
            _dto = ProductTestFixtures.GetProductDtoWithOneSubstance();
            _product = new Product(_dto);

            Assert.IsTrue(ProductIsPopulated(), "product substance was not added");
        }

        private Boolean ProductIsPopulated()
        {
            var isPopulated = _product.Brand.Name == _dto.BrandName;
            isPopulated = isPopulated && _product.DisplayName == _dto.DisplayName;
            isPopulated = isPopulated && _product.GenericName == _dto.GenericName;
            isPopulated = isPopulated && _product.Package.Name == _dto.PackageName;
            isPopulated = isPopulated && _product.ProductCode == _dto.ProductCode;
            isPopulated = isPopulated && _product.Name == _dto.ProductName;
            isPopulated = isPopulated && _product.Quantity.Value == _dto.Quantity;
            isPopulated = isPopulated && _product.Shape.Name == _dto.ShapeName;
            isPopulated = isPopulated && _product.Quantity.Unit.Name == _dto.UnitName;

            foreach (var substance in _dto.Substances)
            {
                var substance1 = substance;
                isPopulated = isPopulated &&
                              _product.Substances.SingleOrDefault(s => 
                                  substance1.Substance == s.Substance &&
                                  substance1.SortOrder == s.SortOrder &&
                                  substance1.Quantity == s.Quantity &&
                                  substance1.Unit == s.Unit) != null;
            }

            return isPopulated;
        }
    }
}
