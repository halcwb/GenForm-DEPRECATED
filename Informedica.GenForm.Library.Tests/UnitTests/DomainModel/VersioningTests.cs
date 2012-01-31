using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.TestFixtures.Fixtures;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    /// <summary>
    /// Summary description for VersioningTests
    /// </summary>
    [TestClass]
    public class VersioningTests : TestSessionContext
    {
        public VersioningTests() : base(true)
        {
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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
        public void ThatVersionDoesNotChangedAfterOnlyAReadAction()
        {
            var services = BrandServices.WithDto(BrandTestFixtures.GetDto()).Get();
            Assert.AreEqual(1, services.Version);
            Context.CurrentSession().Transaction.Commit();
            Context.CurrentSession().Transaction.Begin();
            var brand = BrandServices.Brands.Single(x => x.Name == BrandTestFixtures.GetDto().Name);
            Assert.AreEqual(1, brand.Version);
        }


    }
}
