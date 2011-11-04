using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.TestFixtures.Fixtures;
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
            var substance = SubstanceTestFixtures.CreateSubstanceWithoutGroup();
            Assert.IsTrue(SubstanceIsValid(substance));
        }

        [TestMethod]
        public void ThatSubstanceCannotBeCreatedWithoutName()
        {
            try
            {
                Substance.Create(new SubstanceDto());
                Assert.Fail();
            }
            catch (System.Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }

        [TestMethod]
        public void ThatAValidSubstanceIsCreatedInANewGroup()
        {
            var substance = SubstanceTestFixtures.CreateSubstanceWitGroup();
            Assert.IsNotNull(substance.SubstanceGroup);
        }


        [TestMethod]
        public void ThatSubstanceGroupContainsThatSubstance()
        {
            var substance = SubstanceTestFixtures.CreateSubstanceWitGroup();
            Assert.IsTrue(substance.SubstanceGroup.Substances.Contains(substance));
        }

        private static bool SubstanceIsValid(Substance substance)
        {
            return !string.IsNullOrWhiteSpace(substance.Name);
        }
    }
}
