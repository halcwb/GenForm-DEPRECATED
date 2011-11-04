using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.TestFixtures.Fixtures;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Factories
{
    /// <summary>
    /// Summary description for SubstanceFactoryTests
    /// </summary>
    [TestClass]
    public class SubstanceFactoryTests : TestSessionContext
    {
        private TestContext testContextInstance;

        public SubstanceFactoryTests() : base(false) {}

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
        public void ThatASubstanceFactoryCanCreateSubstanceUsingSubstanceDto()
        {
            var dto = SubstanceTestFixtures.GetSubstanceDtoWithoutGroup();
            var fact = new SubstanceFactory(dto);

            Assert.IsInstanceOfType(fact.Get(), typeof(Substance));
        }

        [TestMethod]
        public void ThatSubstanceFactoryCreatesSubstanceWithGroup()
        {
            var dto = SubstanceTestFixtures.GetSubstanceWithGroup();
            var fact = new SubstanceFactory(dto);

            Assert.IsNotNull(fact.Get().SubstanceGroup);
        }

        [TestMethod]
        public void ThatSubstanceCreatedWithSubstanceGroupContainsThatSubstance()
        {
            var dto = SubstanceTestFixtures.GetSubstanceWithGroup();
            var fact = new SubstanceFactory(dto);
            var subst = fact.Get();

            Assert.IsNotNull(subst.SubstanceGroup.Substances.First() == subst);
        }

        [TestMethod]
        public void ThatWhenSubstanceWithGroupIsPersistedSubstanceGroupContainsSubstance()
        {
            var dto = SubstanceTestFixtures.GetSubstanceWithGroup();
            var fact = new SubstanceFactory(dto);
            var subst = fact.Get();
            Assert.IsTrue(subst.SubstanceGroup.Substances.Contains(subst));
            Context.CurrentSession().Transaction.Commit();
            Context.CurrentSession().Transaction.Begin();

            Assert.IsTrue(subst.SubstanceGroup.Substances.Contains(subst));
        }

        [TestMethod]
        public void ThatWhenSubstanceIsRemovedFromGroupAssociationsAreRemoved()
        {
            var dto = SubstanceTestFixtures.GetSubstanceWithGroup();
            var fact = new SubstanceFactory(dto);
            var subst = fact.Get();
            
            Assert.AreEqual(subst, subst.SubstanceGroup.Substances.First());
            subst.RemoveFromSubstanceGroup();
            Assert.IsNull(subst.SubstanceGroup);
        }
    }
}
