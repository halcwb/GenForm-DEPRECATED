using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    /// <summary>
    /// Summary description for PackageConstructionTests
    /// </summary>
    [TestClass]
    public class PackageConstructionTests
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
        public void ThatAValidPackageCanBeConstructed()
        {
            var package = Package.Create(PackageTestFixtures.GetValidDto());
            Assert.IsTrue(PackageIsValid(package));
        }

        private bool PackageIsValid(Package package)
        {
            return !String.IsNullOrWhiteSpace(package.Name) &&
                   !String.IsNullOrWhiteSpace(package.Abbreviation);
        }
    }

    public static class PackageTestFixtures
    {
        public static PackageDto GetValidDto()
        {
            return new PackageDto
                       {
                           Abbreviation = "ampul",
                           Name = "ampul"
                       };
        }
    }
}
