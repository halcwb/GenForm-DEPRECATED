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
    public class DatabaseServicesShould
    {
        private TestContext testContextInstance;
        private static IDatabaseServices _databaseServices;
        private static IDatabaseSetting _databaseSetting;


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
        public static void MyClassInitialize(TestContext testContext)
        {
            ObjectFactory.Inject<IDatabaseServices>(new DatabaseServices());
            ObjectFactory.Inject<IDatabaseSetting>(new DatabaseSetting());
            ObjectFactory.Inject<IDatabaseConnection>(new DatabaseConnection());

            _databaseServices = ObjectFactory.GetInstance<IDatabaseServices>();
            _databaseSetting = ObjectFactory.GetInstance<IDatabaseSetting>();
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

            Assert.IsFalse(_databaseServices.TestDatabaseConnection(_databaseSetting), "Bogus database should not exist");
        }

        private static void SetUpInvalidDatabaseSetting()
        {
            _databaseSetting.Name = "Bogus";
            _databaseSetting.ConnectionString = @"C:\Bogus";
            _databaseSetting.Machine = "HAL-WIN7";
        }

        [TestMethod]
        public void ReturnTrueWhenCheckingIfProductionDatabaseExists()
        {
            SetUpValidDatabaseSetting();

            Assert.IsTrue(_databaseServices.TestDatabaseConnection(_databaseSetting), "Production database should exists");
        }

        private static void SetUpValidDatabaseSetting()
        {
            _databaseSetting.Name = "ProductieDatabase";
            _databaseSetting.ConnectionString =
                @"Data Source=HAL-WIN7\INFORMEDICA;Initial Catalog=GenForm;Integrated Security=True";
            _databaseSetting.Machine = "HAL-WIN7";
        }

        [TestMethod]
        public void RegisterValidDatabaseSetting()
        {
            SetUpValidDatabaseSetting();
            _databaseServices.RegisterDatabaseSetting(_databaseSetting);

            Assert.IsTrue(_databaseServices.TestDatabaseConnection(_databaseSetting), "A valid database setting should be registered");
        }
    }
}
