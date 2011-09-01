using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    /// <summary>
    /// Summary description for SubstanceConstructionTests
    /// </summary>
    [TestClass]
    public class SubstanceConstructionTests
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
        public void ThatAValidSubstanceIsCreated()
        {
            var substance = Substance.Create(SubstanceTestFixtures.GetSubstanceWithGroup());
            Assert.IsTrue(SubstanceIsValid(substance));
        }

        [TestMethod]
        public void ThatAValidSubstanceIsCreatedInANewGroup()
        {
            var substance = Substance.Create(SubstanceTestFixtures.GetSubstanceWithGroup());
            Assert.IsTrue(SubstanceGroupIsValid(substance));
        }

        [TestMethod]
        public void ThatAValidSubstanceCanBeCreatedWithAnExistingGroup()
        {
            var substance = Substance.Create(SubstanceTestFixtures.GetSubstanceDtoWithoutGroup(), 
                            SubstanceGroup.Create(SubstanceGroupTestFixtures.GetSubstanceGroupDtoWithoutItems()));
            Assert.IsTrue(SubstanceIsValid(substance));
            Assert.IsTrue(SubstanceGroupIsValid(substance));
        }

        private bool SubstanceGroupIsValid(Substance substance)
        {
            if (substance.SubstanceGroup == null) return true;

            return !string.IsNullOrWhiteSpace(substance.SubstanceGroup.Name) &&
                   substance.SubstanceGroup.SubstanceSet.Contains(substance);
        }

        private bool SubstanceIsValid(Substance substance)
        {
            return !string.IsNullOrWhiteSpace(substance.Name);
        }
    }
}
