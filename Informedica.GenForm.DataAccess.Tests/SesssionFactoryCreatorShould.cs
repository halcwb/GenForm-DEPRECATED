using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Databases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Tests
{
    /// <summary>
    /// Summary description for SesssionFactoryCreatorShould
    /// </summary>
    [TestClass]
    public class SesssionFactoryCreatorShould
    {
        private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
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
        //[ClassCleanup()]
        //public static void MyClassCleanup() {}
        
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
        public void BeAbleToCreateASessionFactory()
        {
            var factory = SessionFactoryCreator.CreateSessionFactory();
            Assert.IsNotNull(factory);
        }

        [TestMethod]
        public void BeRetreivedByGenFormApplication()
        {
            var factory = GenFormApplication.SessionFactory;
            Assert.IsInstanceOfType(factory, typeof(ISessionFactory));
        }

        [TestMethod]
        public void BeAbleToCreateASqlLiteDatabase()
        {
            ObjectFactory.Inject(typeof (IDatabaseConfig), new SqlLiteConfig());

            var fact = GenFormApplication.SessionFactory;
            Assert.IsNotNull(fact);
        }

    }
}
