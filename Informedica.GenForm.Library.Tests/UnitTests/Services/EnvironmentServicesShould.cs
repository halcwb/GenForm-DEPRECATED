using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Library.DomainModel.Databases;
using Informedica.GenForm.Library.Services.Databases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.Library.Tests.UnitTests.Services
{
    /// <summary>
    /// Summary description for DatabaseServicesShould
    /// </summary>
    [TestClass]
    public class EnvironmentServicesShould
    {
        private TestContext testContextInstance;
        private static IEnvironment _environment;


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
        public static void MyClassInitialize(TestContext testContext)
        {
            ObjectFactory.Inject<IEnvironment>(new Environment());
            ObjectFactory.Inject<IDatabaseConnection>(new DatabaseConnection());

            _environment = ObjectFactory.GetInstance<IEnvironment>();
        }

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
        public void ReturnFalseWhenCheckingIfBogusDatabaseExists()
        {
            SetUpInvalidDatabaseSetting();

            Assert.IsFalse(EnvironmentServices.TestDatabaseConnection(_environment), "Bogus database should not exist");
        }

        private static void SetUpInvalidDatabaseSetting()
        {
            _environment.Name = "Bogus";
            _environment.ConnectionString = @"C:\Bogus";
        }

        [TestMethod]
        public void ReturnTrueWhenCheckingIfProductionDatabaseExists()
        {
            SetUpValidDatabaseSetting();

            Assert.IsTrue(EnvironmentServices.TestDatabaseConnection(_environment), "Production database should exists");
        }

        private static void SetUpValidDatabaseSetting()
        {
            _environment.Name = "ProductieDatabase";
            _environment.ConnectionString =
                @"Data Source=HAL-WIN7\INFORMEDICA;Initial Catalog=GenForm;Integrated Security=True";
        }


        [TestMethod]
        public void RegisterValidDatabaseSetting()
        {
            SetUpValidDatabaseSetting();
            EnvironmentServices.RegisterDatabaseSetting(_environment);

            Assert.IsTrue(EnvironmentServices.TestDatabaseConnection(_environment), "A valid database setting should be registered");
        }
    }
}
