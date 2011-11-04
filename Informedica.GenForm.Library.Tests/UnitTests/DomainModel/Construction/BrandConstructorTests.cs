using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.TestFixtures.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    /// <summary>
    /// Summary description for BrandConstructorTests
    /// </summary>
    [TestClass]
    public class BrandConstructorTests
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
        public void ThatBrandWithNameIsCreated()
        {
            var brand = BrandTestFixtures.GetBrandWithNoProducts();
            Assert.IsTrue(BrandIsValid(brand));
        }

        [TestMethod]
        public void ThatBrandHasZeroProducts()
        {
            var brand = BrandTestFixtures.GetBrandWithNoProducts();
            Assert.AreEqual(0, brand.Products.Count());
        }

        [TestMethod]
        public void ThatBrandWithoutNameThrowsException()
        {
            try
            {
                Brand.Create(new BrandDto());
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }   
        }

        private static bool BrandIsValid(Brand brand)
        {
            return (!String.IsNullOrWhiteSpace(brand.Name));
        }
    }
}
