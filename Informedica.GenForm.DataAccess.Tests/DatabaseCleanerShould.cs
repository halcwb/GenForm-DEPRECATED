using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Assembler.Contexts;
using Informedica.GenForm.Library.DomainModel.Databases;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Linq;
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

namespace Informedica.GenForm.DataAccess.Tests
{
    /// <summary>
    /// Summary description for DatabaseCleanerShould
    /// </summary>
    [TestClass]
    public class DatabaseCleanerShould
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
        [ClassInitialize()]
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
        public void StartWithAnEmptyDatabase()
        {
            using (var context = new SessionContext())
            {
                var session = context.CurrentSession();
                session.Transaction.Begin();
                Assert.AreEqual(0, session.Query<EmptyDatabase>().Count());
                session.Transaction.Commit();
            }
            
        }

        [TestMethod]
        public void BeAbleToClearTheEmptyDatabase()
        {
            using(var context = new SessionContext())
            {
                var session = context.CurrentSession();
                session.Transaction.Begin();
                var emptyDb = new EmptyDatabase { IsEmpty = false };
                session.Save(emptyDb);
                session.Transaction.Commit();

                Assert.AreEqual(1, session.Query<EmptyDatabase>().Count());

                session.Transaction.Begin();
                DatabaseCleaner.CleanDataBase();
                session.Transaction.Commit();

                Assert.AreEqual(0, session.Query<EmptyDatabase>().Count());
            }
        }
    }
}