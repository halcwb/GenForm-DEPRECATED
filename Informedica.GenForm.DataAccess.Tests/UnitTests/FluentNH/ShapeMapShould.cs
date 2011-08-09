using FluentNHibernate.Testing;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.FluentNH
{
    /// <summary>
    /// Summary description for ShapeMapShould
    /// </summary>
    [TestClass]
    public class ShapeMapShould
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
        public void CorrectlyMapAShape()
        {
            using (var session = GenFormApplication.Instance.SessionFactory.OpenSession())
            {
                new PersistenceSpecification<Shape>(session)
                    .CheckProperty(s => s.Name, "infusievloeistof")
                    //                    .CheckList(b => b.Products, CreateProductList());
                    .VerifyTheMappings();
            }
        }
    }
}
