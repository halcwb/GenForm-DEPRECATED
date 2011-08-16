using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
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
            var dto = new ProductDto{Name = ProductName};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.Name, product.Name, "new instance should have the same productname");
        }

        [TestMethod]
        public void CreateAnInstanceWithGenericNameSetToDto()
        {
            var dto = new ProductDto{GenericName = GenericName};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.GenericName, product.GenericName, "new instance should have the same productname");
        }

        [TestMethod]
        public void CreateAnInstanceWithBrandNameSetToDto()
        {
            var dto = new ProductDto{BrandName = BrandName};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.BrandName, product.Brand.Name, "new instance should have the same productname");
        }

        [TestMethod]
        public void CreateAnInstanceWithProductCodeSetToDto()
        {
            var dto = new ProductDto{ProductCode = ProductCode};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.Name, product.Name, "new instance should have the same productname");
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
            var dto = new ProductDto{ShapeName = Shape};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.ShapeName, product.Shape.Name, "new instance should have the same productname");
        }

        [TestMethod]
        public void CreateAnInstanceWithPackageNameSetToDto()
        {
            var dto = new ProductDto{PackageName = Pacakage};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.PackageName, product.Package.Name, "new instance should have the same productname");
        }

        [TestMethod]
        public void CreateAnInstanceWithUnitNameSetToDto()
        {
            var dto = new ProductDto{UnitName = Unit};
            var product = GetProduct(dto);

            Assert.AreEqual(dto.UnitName, product.Quantity.Unit.Name, "new instance should have the same productname");
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
