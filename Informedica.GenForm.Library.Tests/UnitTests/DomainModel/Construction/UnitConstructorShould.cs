using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    /// <summary>
    /// Summary description for UnitTestConstructorShould
    /// </summary>
    [TestClass]
    public class UnitConstructorShould
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
        public void AddUnitToUnitGroup()
        {
            Unit unit = CreateUnit();
            Assert.IsNotNull(unit.UnitGroup);
            Assert.AreEqual("mass", unit.UnitGroup.Name);
        }

        private Unit CreateUnit()
        {
            var group = UnitGroup.Create(new UnitGroupDto
                                             {
                                                 AllowConversion = true,
                                                 Name = "mass"
                                             });
            return Unit.Create(new UnitDto
                    {
                        Abbreviation = "mg",
                        AllowConversion = true,
                        Divisor = 1000,
                        IsReference = true,
                        Multiplier = (decimal)0.001,
                        Name = "milligram",
                        UnitGroupName = "massa"
                    }, group);
        }

        [TestMethod]
        public void UseExistingUnitGroupToAddUnitTo()
        {
            var unit1 = CreateUnit();
            var unit2 = Unit.Create(new UnitDto
                        {
                            Abbreviation = "mcg",
                            Divisor = 1000000,
                            IsReference = true,
                            Multiplier = (decimal)0.0000001,
                            Name = "microgram",
                        }, (UnitGroup) unit1.UnitGroup);

            Assert.AreEqual(unit1.UnitGroup, unit2.UnitGroup);
        }
    }
}
