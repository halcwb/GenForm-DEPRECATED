using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    /// <summary>
    /// Summary description for UnitFactoryCreates
    /// </summary>
    [TestClass]
    public class UnitFactoryCreates
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
        public void ANewUnitUsingAUnitDto()
        {
            var unit = CreateTestUnit();
            Assert.IsInstanceOfType(unit, typeof(Unit));
        }

        private static Unit CreateTestUnit()
        {
            var dto = UnitTestFixtures.GetTestUnitMilligram();
            var unit = UnitFactory.CreateUnit(dto);
            return unit;
        }

        [TestMethod]
        public void AlwaysAUnitWithAUnitGroup()
        {
            var unit = CreateTestUnit();
            Assert.IsNotNull(unit.UnitGroup);
        }

        [TestMethod]
        public void AUnitWithUnitGroupWithNameOfUnitDtoUnitGroupName()
        {
            var unit = CreateTestUnit();
            Assert.AreEqual(UnitTestFixtures.GetTestUnitMilligram().UnitGroupName, unit.UnitGroup.Name);
        }

        [TestMethod]
        public void UnitWithUnitGroupThatContainsThatUnit()
        {
            var unit = CreateTestUnit();
            Assert.AreSame(unit, unit.UnitGroup.Units.First());
        }
    }
}
