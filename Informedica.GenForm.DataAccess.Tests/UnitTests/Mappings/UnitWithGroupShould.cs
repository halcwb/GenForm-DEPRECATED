using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.TestFixtures.Fixtures;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Linq;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for UnitWithGroupShould
    /// </summary>
    [TestClass]
    public class UnitWithGroupShould : TestSessionContext
    {
        private TestContext testContextInstance;

        public UnitWithGroupShould() : base(false) {}

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
            var unit = CreateUnit();

            AssertUnitNameIsSet(unit);
        }

        private static Unit CreateUnit()
        {
            var group =
                UnitGroup.Create(new UnitGroupDto
                                     {
                                         Name = UnitTestFixtures.GetTestUnitMilligram().UnitGroupName,
                                         AllowConversion = UnitTestFixtures.GetTestUnitMilligram().AllowConversion
                                     });
            var unit = Unit.Create(UnitTestFixtures.GetTestUnitMilligram(), group);
            return unit;
        }

        [TestMethod]
        public void HaveAUnitGroup()
        {
            var unit = CreateUnit();

            AssertUnitGroupName(unit);
        }

        [TestMethod]
        public void BeAbleToPersistAUnit()
        {
            PersistUnit(Context.CurrentSession());
        }

        [TestMethod]
        public void BeAbleToFindUnitInUnitGroup()
        {
            PersistUnit(Context.CurrentSession());
            var unit =
                Context.CurrentSession().Query<Unit>().Single(
                    x => x.Name == UnitTestFixtures.GetTestUnitMilligram().Name);

            Assert.IsTrue(unit.UnitGroup.Units.Contains(unit));
        }

        [TestMethod]
        public void NotAcceptTheSameUnitTwice()
        {
            PersistUnit(Context.CurrentSession());
            var unit =
                Context.CurrentSession().Query<Unit>().Single(
                    x => x.Name == UnitTestFixtures.GetTestUnitMilligram().Name);
            var group = unit.UnitGroup;
            try
            {
                group.AddUnit(unit);
            }
            catch (System.Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }


        [TestMethod]
        public void NotAcceptTheDifferentUnitWithSameUnitNameTwice()
        {
            PersistUnit(Context.CurrentSession());
            var unit =
                Context.CurrentSession().Query<Unit>().Single(
                    x => x.Name == UnitTestFixtures.GetTestUnitMilligram().Name);
            var group = unit.UnitGroup;
            
            try
            {
                group.AddUnit(CreateUnit());
            }
            catch (System.Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }

        private static void PersistUnit(ISession session)
        {
            var unit = CreateUnit();

            AssertUnitNameIsSet(unit);
            AssertUnitGroupName(unit);

            session.Save(unit);
        }

        private static void AssertUnitGroupName(IUnit unit)
        {
            Assert.AreEqual(UnitTestFixtures.GetTestUnitMilligram().UnitGroupName, unit.UnitGroup.Name);
        }

        private static void AssertUnitNameIsSet(IUnit unit)
        {
            Assert.AreEqual(UnitTestFixtures.GetTestUnitMilligram().Name, unit.Name);
        }
    }
}