using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    /// <summary>
    /// Summary description for UnitConstructionTests
    /// </summary>
    [TestClass]
    public class UnitConstructionTests
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
        public void ThatAValidUnitIsConstructedWithNewUnitGroup()
        {
            var unit = Unit.Create(UnitTestFixtures.GetTestUnitMilligram());
            Assert.IsTrue(UnitIsValid(unit));
        }

        [TestMethod]
        public void ThatAValidUnitIsConstructedWithExistingUnitGroup()
        {
            var group = UnitGroup.Create(UnitGroupTestFixtures.GetDtoVolume());
            var unit = Unit.Create(UnitTestFixtures.GetTestUnitMilligram(), group);
            Assert.IsTrue(UnitIsValid(unit));
        }

        private bool UnitIsValid(Unit unit)
        {
            return !string.IsNullOrWhiteSpace(unit.Name) &&
                   !string.IsNullOrWhiteSpace(unit.Abbreviation) &&
                   unit.UnitGroup != null &&
                   unit.UnitGroup.UnitSet.Contains(unit);
        }
    }
}
