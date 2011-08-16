using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for UnitWithGroupShould
    /// </summary>
    [TestClass]
    public class UnitWithGroupShould : TestSessionContext
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
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext) { GenFormApplication.Initialize(); }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run

        #endregion

        [TestMethod]
        public void HavePropertiesSetByTestFixture()
        {
            var unit = Unit.Create(UnitTestFixtures.GetTestUnitMilligram());

            AssertUnitNameIsSet(unit);
        }

        [TestMethod]
        public void HaveAUnitGroup()
        {
            var unit = Unit.Create(UnitTestFixtures.GetTestUnitMilligram());

            AssertUnitGroupName(unit);
        }

        private static void AssertUnitGroupName(IUnit unit)
        {
            Assert.AreEqual(UnitTestFixtures.GetTestUnitMilligram().UnitGroupName, unit.UnitGroup.Name);
        }

        private static void AssertUnitNameIsSet(IUnit unit)
        {
            Assert.AreEqual(UnitTestFixtures.GetTestUnitMilligram().Name, unit.Name);
        }

        [TestMethod]
        public void BeAbleToPersistAUnit()
        {
            PersistUnit(Context.CurrentSession());
        }

        private static void PersistUnit(ISession session)
        {
            var unit = Unit.Create(UnitTestFixtures.GetTestUnitMilligram());

            AssertUnitNameIsSet(unit);
            AssertUnitGroupName(unit);

            session.Save(unit);
        }

    }
}