﻿using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for UnitWithGroupShould
    /// </summary>
    [TestClass]
    public class UnitWithGroupShould
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
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void HavePropertiesSetByTestFixture()
        {
            var unit = DomainFactory.Create<IUnit, UnitDto>(UnitTestFixtures.GetTestUnitMilligram());

            AssertUnitNameIsSet(unit);
        }

        [TestMethod]
        public void HaveAUnitGroup()
        {
            var unit = DomainFactory.Create<IUnit, UnitDto>(UnitTestFixtures.GetTestUnitMilligram());

            AssertUnitGroupName(unit);
        }

        private static void AssertUnitGroupName(IUnit unit)
        {
            Assert.AreEqual(UnitTestFixtures.GetTestUnitMilligram().UnitGroupName, unit.UnitGroup.UnitGroupName);
        }

        private static void AssertUnitNameIsSet(IUnit unit)
        {
            Assert.AreEqual(UnitTestFixtures.GetTestUnitMilligram().Name, unit.Name);
        }

        [TestMethod]
        public void BeAbleToPersistAUnit()
        {
            using (var session = GenFormApplication.Instance.SessionFactoryFromInstance.OpenSession())
            {
                PersistUnit(session);
            }
        }

        private static void PersistUnit(ISession session)
        {
            var unit =
                DomainFactory.Create<IUnit, UnitDto>(UnitTestFixtures.GetTestUnitMilligram());

            using (var trans = session.BeginTransaction())
            {
                AssertUnitNameIsSet(unit);
                AssertUnitGroupName(unit);

                session.Save(unit);

                trans.Commit();
            }

        }

    }
}
