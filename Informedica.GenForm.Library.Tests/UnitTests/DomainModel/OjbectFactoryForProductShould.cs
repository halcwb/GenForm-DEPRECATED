using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services.Products.dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    /// <summary>
    /// Summary description for ProductClassShould
    /// </summary>
    [TestClass]
    public class OjbectFactoryForProductShould: ProductTestBase
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
        public void CreateAnewProductInstanceFromAproductDto()
        {
            var dto = new ProductDto();
            try
            {
                var product = GetProduct(dto);
                Assert.IsInstanceOfType(product, typeof(IProduct), "could not create an instanc of product");

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void CreateAnInstanceWithProductNameSetToDto()
        {
            var dto = new ProductDto{ProductName = ProductName};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.ProductName, product.ProductName, "new instance should have the same productname");
        }

        [TestMethod]
        public void CreateAnInstanceWithGenericNameSetToDto()
        {
            var dto = new ProductDto{Generic = GenericName};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.Generic, product.GenericName, "new instance should have the same productname");
        }

        [TestMethod]
        public void CreateAnInstanceWithBrandNameSetToDto()
        {
            var dto = new ProductDto{Brand = BrandName};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.Brand, product.BrandName, "new instance should have the same productname");
        }

        [TestMethod]
        public void CreateAnInstanceWithProductCodeSetToDto()
        {
            var dto = new ProductDto{ProductCode = ProductCode};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.ProductName, product.ProductName, "new instance should have the same productname");
        }

        [TestMethod]
        public void CreateAnInstanceWithDisplayNameSetToDto()
        {
            var dto = new ProductDto{DisplayName = DisplayName};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.DisplayName, product.DisplayName, "new instance should have the same productname");
        }

        [TestMethod]
        public void CreateAnInstanceWithShapeNameSetToDto()
        {
            var dto = new ProductDto{Shape = Shape};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.Shape, product.ShapeName, "new instance should have the same productname");
        }

        [TestMethod]
        public void CreateAnInstanceWithPackageNameSetToDto()
        {
            var dto = new ProductDto{Package = Pacakage};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.Package, product.PackageName, "new instance should have the same productname");
        }

        [TestMethod]
        public void CreateAnInstanceWithUnitNameSetToDto()
        {
            var dto = new ProductDto{Unit = Unit};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.Unit, product.UnitName, "new instance should have the same productname");
        }

        [TestMethod]
        public void CreateAnInstanceWithQuantitySetToDto()
        {
            var dto = new ProductDto{Quantity = Quantity};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.Quantity, product.Quantity, "new instance should have the same productname");
        }
    }
}
