using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Factories
{
    /// <summary>
    /// Summary description for FactoryManagerTests
    /// </summary>
    [TestClass]
    public class FactoryManagerTests
    {
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
        public void ThatAUnitFactoryCanBeReturned()
        {
            var factory = (UnitFactory)FactoryManager.Get<Unit, UnitDto>(new UnitDto());
            Assert.IsInstanceOfType(factory, typeof(UnitFactory));
        }

        [TestMethod]
        public void ThatABrandFactoryCanBeReturned()
        {
            var factory = (BrandFactory)FactoryManager.Get<Brand, BrandDto>(new BrandDto());
            Assert.IsInstanceOfType(factory, typeof(BrandFactory));
        }
    }
}

