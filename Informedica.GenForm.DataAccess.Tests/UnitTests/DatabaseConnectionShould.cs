using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Library.DomainModel.Databases;
using Informedica.GenForm.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests
{
    /// <summary>
    /// Summary description for DatabaseConnectionShould
    /// </summary>
    [TestClass]
    public class DatabaseConnectionShould
    {
        private readonly string _validConnectionString = SettingsManager.Instance.GetConnectionString("Test");
        private static IDatabaseConnection _databaseConnection;

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
        public static void MyClassInitialize(TestContext testContext)
        {
            ObjectFactory.Inject<IDatabaseConnection>(new DatabaseConnection());
            ObjectFactory.Inject<IEnvironment>(new Environment());
            ObjectFactory.Inject<SettingReader>(new SettingsSettingReader());

            _databaseConnection = ObjectFactory.GetInstance<IDatabaseConnection>();
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
        public void ReturnFalseWhenConnectionStringCannotConnectToDatabase()
        {
            const string connectionString = @"Data Source=unknown;Initial Catalog=Bogus;Integrated Security=True";
            Assert.IsFalse(_databaseConnection.TestConnection(connectionString), "Using connection: " + connectionString + " test connection should return false");
        }

        [TestMethod] 
        public void ReturnTrueWhenConnectectionStringCanConnectToDatabase()
        {
            var connectionString = _validConnectionString;
            Assert.IsTrue(_databaseConnection.TestConnection(connectionString), "Using connection: " + connectionString + " test connection should return true");
        }

        [TestMethod]
        public void RegisterValidDatabaseSetting()
        {
            IEnvironment setting = GetValidEnvironmentSetting();

            _databaseConnection.RegisterSetting(setting);
            var result = _databaseConnection.GetConnectionString(setting.Name);
            Assert.IsTrue(result == setting.ConnectionString);
        }

        [TestMethod]
        public void ConnectToLocalTestDatabase()
        {
            const string envName = "Test";
            Assert.IsTrue(DatabaseConnection.GetLocalConnectionString(DatabaseConnection.DatabaseName.GenFormTest).Contains(envName));
        }

        private IEnvironment GetValidEnvironmentSetting()
        {
            var setting = ObjectFactory.GetInstance<IEnvironment>();
            setting.Name = "Test";
            setting.ConnectionString =
                _validConnectionString;

            return setting;
        }
    }

}
