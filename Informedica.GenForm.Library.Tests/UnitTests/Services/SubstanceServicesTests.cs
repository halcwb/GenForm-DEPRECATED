using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.Tests;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Services
{
    /// <summary>
    /// Summary description for SubstanceServicesTests
    /// </summary>
    [TestClass]
    public class SubstanceServicesTests : TestSessionContext
    {
        private TestContext testContextInstance;

        public SubstanceServicesTests() : base(true)
        {
        }

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
        public void ThatSubstanceCanBeGet()
        {
            var substance = SubstanceServices.WithDto(SubstanceTestFixtures.GetSubstanceWithGroup()).Get();
            Assert.IsInstanceOfType(substance, typeof(Substance));
        }

        [TestMethod]
        public void ThatSubstanceWithSubstanceGroupCanBeGet()
        {
            var substance = SubstanceServices.WithDto(SubstanceTestFixtures.GetSubstanceWithGroup()).Get();
            Assert.AreEqual(substance.SubstanceGroup.Name,
                            SubstanceServices.Substances.Single(x => x.Name == substance.Name)
                            .SubstanceGroup.Name);           
        }

        [TestMethod]
        public void ThatSubstanceWithSubstanceGroupIsBidirectional()
        {
            var substance = SubstanceServices.WithDto(SubstanceTestFixtures.GetSubstanceWithGroup()).Get();
            Assert.AreEqual(substance,
                            SubstanceServices.Substances.Single(x => x.Name == substance.Name)
                            .SubstanceGroup.SubstanceSet.Single(s => s.Name == substance.Name));                       
        }

        [TestMethod]
        public void ThatSubstanceCanBeFound()
        {
            var substance = SubstanceServices.WithDto(SubstanceTestFixtures.GetSubstanceWithGroup()).Get();
            Assert.AreEqual(substance, 
                            SubstanceServices.Substances.Single(x => x.Name == substance.Name));
        }

        [TestMethod] 
        public void ThatSubstanceCanChangeName()
        {
            var substance = SubstanceServices.WithDto(SubstanceTestFixtures.GetSubstanceWithGroup()).Get();
            SubstanceServices.ChangeSubstanceName(substance, "dopamine changed");
            Context.CurrentSession().Transaction.Commit();
            
            Context.CurrentSession().Transaction.Begin();
            substance = SubstanceServices.Substances.First(s => s.Name == "dopamine changed");
            Assert.IsNotNull(substance);
            Assert.AreEqual(substance.Name, "dopamine changed");
        }

        [TestMethod]
        public void ThatSubstanceCanBeDeleted()
        {
            var substance = SubstanceServices.WithDto(SubstanceTestFixtures.GetSubstanceWithGroup()).Get();
            SubstanceServices.Delete(substance);
            Context.CurrentSession().Transaction.Commit();

            Context.CurrentSession().Transaction.Begin();
            substance =
                SubstanceServices.Substances.SingleOrDefault(
                    s => s.Name == SubstanceTestFixtures.GetSubstanceWithGroup().Name);
            Assert.IsNull(substance);
        }

        [TestMethod]
        public void ThatSubstanceCanBeDeletedWithinTransaction()
        {
            var substance = SubstanceServices.WithDto(SubstanceTestFixtures.GetSubstanceWithGroup()).Get();
            SubstanceServices.Delete(substance);

            substance =
                SubstanceServices.Substances.SingleOrDefault(
                    s => s.Name == SubstanceTestFixtures.GetSubstanceWithGroup().Name);
            Assert.IsNull(substance);
        }

        [TestMethod]
        public void ThatSubstanceGroupCanBeDeletedWithoutDeletingSubstance()
        {
            var substance = SubstanceServices.WithDto(SubstanceTestFixtures.GetSubstanceWithGroup()).Get();
            var group = substance.SubstanceGroup;
            var id = group.Id;
            group.ClearAllSubstances();

            Assert.IsNull(substance.SubstanceGroup);
            Context.CurrentSession().Delete(group);
            group = Context.CurrentSession().Get<SubstanceGroup>(id);
            Assert.IsNull(group);
        }

        [TestMethod]
        public void ThatIfSubstanceDeleteIsCalledSubstanceGroupHasNoAssociationWithSubstance()
        {
            var substance = SubstanceServices.WithDto(SubstanceTestFixtures.GetSubstanceWithGroup()).Get();
            
            SubstanceServices.Delete(substance);

        }

        [TestMethod]
        public void ThatSubstanceCanBeDeletedWithoutDeletingSubstanceGroup()
        {
            var substance = SubstanceServices.WithDto(SubstanceTestFixtures.GetSubstanceWithGroup()).Get();
            var id = substance.SubstanceGroup.Id;
            SubstanceServices.Delete(substance);
            var group = Context.CurrentSession().Get<SubstanceGroup>(id);
            Assert.IsNotNull(group);
        }
    }
}
