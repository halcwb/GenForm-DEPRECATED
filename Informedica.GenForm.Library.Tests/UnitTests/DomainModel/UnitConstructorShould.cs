using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    /// <summary>
    /// Summary description for UnitTestConstructorShould
    /// </summary>
    [TestClass]
    public class UnitConstructorShould
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
        public void AddUnitToUnitGroup()
        {
            Unit unit = CreateUnit();
            Assert.IsNotNull(unit.UnitGroup);
            Assert.AreEqual("massa", unit.UnitGroup.UnitGroupName);
        }

        private Unit CreateUnit()
        {
            return new Unit(new UnitDto
                                {
                                    Abbreviation = "mg",
                                    AllowConversion = true,
                                    Divisor = 1000,
                                    IsReference = true,
                                    Multiplier = (decimal)0.001,
                                    Name = "milligram",
                                    UnitGroupName = "massa"
                                });
        }

        [TestMethod]
        public void UseExistingUnitGroupToAddUnitTo()
        {
            var unit1 = CreateUnit();
            var unit2 = new Unit(new UnitDto
                                     {
                                         Abbreviation = "mcg",
                                         AllowConversion = true,
                                         Divisor = 1000000,
                                         IsReference = true,
                                         Multiplier = (decimal)0.0000001,
                                         Name = "microgram",
                                         UnitGroupName = "massa"
                                     });

            Assert.AreEqual(unit1.UnitGroup, unit2.UnitGroup);
        }
    }
}
