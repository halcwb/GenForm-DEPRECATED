using System;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.TestFixtures.Fixtures;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Services
{
    /// <summary>
    /// Summary description for BrandServicesTests
    /// </summary>
    [TestClass]
    public class BrandServicesTests : TestSessionContext
    {
        public BrandServicesTests() : base(true){}

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
        public void ThatABrandCanBeGet()
        {
            var brand = BrandServices.WithDto(BrandTestFixtures.GetDto()).Get();
            Assert.IsNotNull(brand);
        }

        [TestMethod]
        public void ThatBrandHasAnId()
        {
            var brand = BrandServices.WithDto(BrandTestFixtures.GetDto()).Get();
            Assert.IsTrue(brand.Id != Guid.Empty);
        }
        
        [TestMethod]
        public void ThatBrandCanBeFoundWhenCreated()
        {
            var brand = BrandServices.WithDto(BrandTestFixtures.GetDto()).Get();
            Assert.AreEqual(brand, 
                            BrandServices.Brands.Single(b => b.Name == brand.Name));
        }

        [TestMethod]
        public void ThatBrandCanBeUpdated()
        {
            var brand = BrandServices.WithDto(BrandTestFixtures.GetDto()).Get();
            BrandServices.ChangeBrandName(brand, "changedName");
            Context.CurrentSession().Transaction.Commit();

            Context.CurrentSession().Transaction.Begin();
            brand = BrandServices.Brands.FirstOrDefault(b => b.Name == BrandTestFixtures.GetDto().Name);
            Assert.IsNull(brand);
        }

        [TestMethod]
        public void ThatBrandCanBeDeleted()
        {
            var brand = BrandServices.WithDto(BrandTestFixtures.GetDto()).Get();
            Assert.AreEqual(brand,
                            BrandServices.Brands.Single(b => b.Name == brand.Name));
            BrandServices.Delete(brand);
            brand = BrandServices.Brands.FirstOrDefault(b => b.Name == BrandTestFixtures.GetDto().Name);
            Assert.IsNull(brand);
        }

        [TestMethod]
        public void ThatBrandWithNoProductsHasEmptyProductList()
        {
            var brand = BrandServices.WithDto(BrandTestFixtures.GetDto()).Get();
            Assert.AreEqual(0, brand.Products.Count());
        }

    }
}
