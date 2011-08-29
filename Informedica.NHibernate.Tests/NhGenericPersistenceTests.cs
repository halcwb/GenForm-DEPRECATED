using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.NHibernate.Tests
{
    /// <summary>
    /// Summary description for NhGenericPersistenceTests
    /// </summary>
    [TestClass]
    public class NhGenericPersistenceTests : TestSessionContext
    {
        public NhGenericPersistenceTests(): base(true)
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        public void CollectionCanFindAnItemItContains()
        {
            var subst = Substance.Create(SubstanceTestFixtures.GetSubstanceWithGroup());
            var group = subst.SubstanceGroup;
            Assert.IsTrue(group.Substances.Contains(subst));

            Context.CurrentSession().SaveOrUpdate(subst);

            Assert.IsTrue(group.Substances.Contains(subst));
        }

        [TestMethod]
        public void CollectionCanHaveAnItemRemoved()
        {
            var subst = Substance.Create(SubstanceTestFixtures.GetSubstanceWithGroup());
            var group = subst.SubstanceGroup;
            Assert.IsTrue(group.Substances.Contains(subst));

            Context.CurrentSession().SaveOrUpdate(subst);

            subst.RemoveFromSubstanceGroup();
            Assert.IsFalse(group.Substances.Contains(subst));            
        }

        [TestMethod]
        public void LoadedItemByIdIsSameAsSameItemInSet()
        {
            var subst = Substance.Create(SubstanceTestFixtures.GetSubstanceWithGroup());
            var group = subst.SubstanceGroup;

            Context.CurrentSession().SaveOrUpdate(subst);

            var loadedSubst = Context.CurrentSession().Load<Substance>(subst.Id);
            Assert.IsTrue(group.ContainsSubstance(loadedSubst));
        }

        [TestMethod]
        public void LoadedItemHasSameHashCodeAsInitialItem()
        {
            var subst = Substance.Create(SubstanceTestFixtures.GetSubstanceWithGroup());

            Context.CurrentSession().SaveOrUpdate(subst);

            var loadedSubst = Context.CurrentSession().Load<Substance>(subst.Id);
            Assert.AreEqual(subst.GetHashCode(), loadedSubst.GetHashCode());
        }

        [TestMethod]
        public void HashCodeFromLoadedItemIsHashOfGuid()
        {
            var subst = Substance.Create(SubstanceTestFixtures.GetSubstanceWithGroup());

            Context.CurrentSession().SaveOrUpdate(subst);

            var loadedSubst = Context.CurrentSession().Load<Substance>(subst.Id);
            Assert.AreEqual(loadedSubst.Id.GetHashCode(), loadedSubst.GetHashCode());
        }
    }
}
