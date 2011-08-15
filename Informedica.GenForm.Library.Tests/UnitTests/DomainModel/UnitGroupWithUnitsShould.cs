using System.Diagnostics;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Exceptions;
using Informedica.GenForm.Library.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    /// <summary>
    /// Summary description for UnitGroupWithUnitsShould
    /// </summary>
    [TestClass]
    public class UnitGroupWithUnitsShould
    {
        private TestContext testContextInstance;
        private UnitGroup _unitGroup;

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
        [TestInitialize]
        public void MyTestInitialize()
        {
            _unitGroup = CreateNewUnitGroup();
        }

        private UnitGroup CreateNewUnitGroup()
        {
            return UnitGroupFactory.CreateUnitGroup(new UnitGroupDto
                    {
                        AllowConversion = true,
                        Name = "massa"
                    });
        }

        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void BeAbleToHaveAUnitAdded()
        {
            var unit = UnitFactory.CreateUnit(new UnitDto
                                                  {
                                                      Abbreviation = "mg",
                                                      AllowConversion = false,
                                                      Divisor = 1,
                                                      IsReference = false,
                                                      Multiplier = 1,
                                                      Name = "milligram",
                                                      UnitGroupName = "algemeen"
                                                  });
            _unitGroup.AddUnit(unit);
            Assert.AreSame(unit, _unitGroup.Units.First());
        }

        [TestMethod]
        public void NotAcceptTheSameUnitTwice()
        {
            try
            {
                _unitGroup.AddUnit(CreateUnit());
                _unitGroup.AddUnit(CreateUnit());
                Assert.Fail(new StackFrame().GetMethod().Name);

            }
            catch (System.Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(CannotAddItemException<Unit>), e.ToString());
            } 
        }

        [TestMethod]
        public void SetUnitWithReferenceToItself()
        {
            var unit = UnitFactory.CreateUnit(new UnitDto
                                                  {
                                                      AllowConversion = false,
                                                      Abbreviation = "mg",
                                                      Divisor = 1,
                                                      IsReference = false,
                                                      Multiplier = 1,
                                                      Name = "milligram",
                                                      UnitGroupName = "algemeen"
                                                  });
            Assert.AreNotEqual(_unitGroup, unit.UnitGroup);

            _unitGroup.AddUnit(unit);

            Assert.AreEqual(_unitGroup.Name, unit.UnitGroup.Name);
            Assert.AreEqual(_unitGroup, unit.UnitGroup);
        }

        [TestMethod]
        public void AcceptTwoDifferentUnits()
        {
            var unit1 = CreateUnit();
            var unit2 = UnitFactory.CreateUnit(new UnitDto
                        {
                            Abbreviation = "mcg",
                            Name = "microgram",
                            Divisor = 1000000,
                            IsReference = true,
                            AllowConversion = false,
                            Multiplier = 0.0000001M,
                            UnitGroupName = "algemeen"
                        });

            if (!_unitGroup.ContainsUnit(unit1)) _unitGroup.AddUnit(unit1);
            if (!_unitGroup.ContainsUnit(unit2)) _unitGroup.AddUnit(unit2);

            Assert.IsTrue(_unitGroup.Units.Count() == 2);
        }

        private Unit CreateUnit()
        {
            return UnitFactory.CreateUnit(new UnitDto
                    {
                        Abbreviation = "mg",
                        Name = "milligram",
                        Divisor = 1000,
                        AllowConversion = true,
                        IsReference = false,
                        Multiplier = 0.001M,
                        UnitGroupName = "massa"
                    });
        }
    }
}
