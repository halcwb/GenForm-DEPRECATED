using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.TestFixtures.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    /// <summary>
    /// Summary description for UnitConstructionTests
    /// </summary>
    [TestClass]
    public class UnitConstructionTests
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
        public void ThatAValidUnitIsConstructedWithNewUnitGroup()
        {
            var unit = UnitTestFixtures.CreateUnitMilligram();
            Assert.IsTrue(UnitIsValid(unit));
        }

        [TestMethod]
        public void ThatUnitWithNoNameCannotBeConstructed()
        {
            var group = UnitGroupTestFixtures.CreateUnitGroupMass();
            var dto = UnitTestFixtures.GetTestUnitMilligram();
            dto.Name = string.Empty;

            AssertCreateFails(group, dto);
        }


        [TestMethod]
        public void ThatUnitWithNoAbbreviationCannotBeConstructed()
        {
            var group = UnitGroupTestFixtures.CreateUnitGroupMass();
            var dto = UnitTestFixtures.GetTestUnitMilligram();
            dto.Abbreviation = string.Empty;

            AssertCreateFails(group, dto);
        }


        [TestMethod]
        public void ThatUnitWithNoMultiplierCannotBeConstructed()
        {
            var group = UnitGroupTestFixtures.CreateUnitGroupMass();
            var dto = UnitTestFixtures.GetTestUnitMilligram();
            dto.Multiplier = 0;

            AssertCreateFails(group, dto);
        }


        [TestMethod]
        public void ThatUnitWithEmptyGroupCannotBeConstructed()
        {
            var dto = UnitTestFixtures.GetTestUnitMilligram();

            AssertCreateFails(null, dto);
        }

        private static void AssertCreateFails(UnitGroup group, UnitDto dto)
        {
            try
            {
                Unit.Create(dto, group);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof (AssertFailedException));
            }
        }


        private static bool UnitIsValid(Unit unit)
        {
            return !string.IsNullOrWhiteSpace(unit.Name) &&
                   !string.IsNullOrWhiteSpace(unit.Abbreviation) &&
                   unit.UnitGroup != null &&
                   unit.UnitGroup.UnitSet.Contains(unit);
        }
    }
}
