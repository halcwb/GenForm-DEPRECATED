using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    /// <summary>
    /// Summary description for UnitGroupWithUnitsShould
    /// </summary>
    [TestClass]
    public class UnitGroupConstructionTests
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
        //[TestInitialize]
        //public void MyTestInitialize()
        //{
        //    _unitGroup = CreateNewUnitGroup();
        //}

        //private UnitGroup CreateNewUnitGroup()
        //{
        //    return UnitGroupFactory.CreateUnitGroup(new UnitGroupDto
        //            {
        //                AllowConversion = true,
        //                Name = "massa"
        //            });
        //}

        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ThatAValidUnitGroupCanBeCreated()
        {
            var group = UnitGroup.Create(UnitGroupTestFixtures.ValidDto());
            Assert.IsTrue(UnitGroupIsValid(group));
        }

        private bool UnitGroupIsValid(UnitGroup group)
        {
            return !String.IsNullOrWhiteSpace(group.Name);
        }
    }
}
