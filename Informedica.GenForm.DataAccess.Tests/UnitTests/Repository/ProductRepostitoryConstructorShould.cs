using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Repository
{
    /// <summary>
    /// Summary description for ProductRepostitoryConstructorShould
    /// </summary>
    [TestClass]
    public class ProductRepostitoryConstructorShould
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
        public void CreateRepositoryWithDefaultDataContext()
        {
            var repos = new ProductRepository();
            Assert.IsInstanceOfType(repos.GetDataContext(), typeof(GenFormDataContext));
        }

        [TestMethod]
        public void BeAbleToCreateRepositoryWithOwnDataContext()
        {
            var context = Isolate.Fake.Instance<GenFormDataContext>();
            var repos = new ProductRepository(context);
            Assert.AreEqual(String.Empty, repos.GetDataContext().Connection.ConnectionString);
        }

        //ToDo: finish this test
        [TestMethod]
        public void UseInjectedDataContextOnlyOnce()
        {
            var context = Isolate.Fake.Instance<GenFormDataContext>();
            var repos = new ProductRepository(context);

            Assert.AreEqual(context, repos.GetDataContext());
            Assert.AreNotEqual(context, repos.GetDataContext());

            repos = ObjectFactory.GetInstance<ProductRepository>();
        }

    }
}
