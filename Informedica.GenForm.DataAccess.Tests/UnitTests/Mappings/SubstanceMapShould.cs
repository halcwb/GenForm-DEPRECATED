using System.Collections;
using FluentNHibernate.Testing;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for SubstanceMapShould
    /// </summary>
    [TestClass]
    public class SubstanceMapShould : TestSessionContext
    {
        private TestContext testContextInstance;

        public SubstanceMapShould() : base(false) {}

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
        public void CorrectlyMapSubstanceWithoutGroup()
        {
            new PersistenceSpecification<Substance>(Context.CurrentSession())
                    .CheckProperty(s => s.Name, "paracetamol")
                    .VerifyTheMappings();
        }

        [TestMethod]
        public void CorrectlyMapSubstanceWithGroup()
        {
            var group = SubstanceGroup.Create(GetSubstanceGroupDto());
            var comparer = (IEqualityComparer) new SubstanceComparer();

            new PersistenceSpecification<Substance>(Context.CurrentSession(), comparer)
                    .CheckProperty(s => s.Name, "paracetamol")
                    .CheckReference(s => s.SubstanceGroup, group, ((s, y) => s.AddToSubstanceGroup(y)))
                    .VerifyTheMappings();
        }

        private SubstanceGroupDto GetSubstanceGroupDto()
        {
            return new SubstanceGroupDto{ Name = "analgetica" };
        }
    }
}
