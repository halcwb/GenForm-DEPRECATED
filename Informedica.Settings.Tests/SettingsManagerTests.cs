using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.Settings.Tests
{
    
    
    /// <summary>
    ///This is a test class for SettingsManagerTest and is intended
    ///to contain all SettingsManagerTest Unit Tests
    ///</summary>
    [TestClass]
    public class SettingsManagerTests
    {
        private const string EnvironmentConnectionString = @"Data Source=HAL-WIN7\\INFORMEDICA;Initial Catalog=GenFormTest;Integrated Security=True";


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            ObjectFactory.Inject(typeof(SettingReader), new ConfigurationManagerSettingReader());
            ObjectFactory.Inject(typeof(SettingWriter), new ConfigurationManagerSettingWriter());
        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod]
        public void ThatSettingsManagerCanRegisterASetting()
        {
            var environment = "Test";
            var connectionString = "TestConnection";

            SettingsManager.Instance.WriteSecureSetting(environment, connectionString);
        }

        [TestMethod]
        public void ThatSettingsManagerCanRegisterEnvironmen()
        {
            SettingsManager.Instance.WriteSecureSetting("GenFormTest", EnvironmentConnectionString);

            Assert.AreEqual(EnvironmentConnectionString, SettingsManager.Instance.ReadSecureSetting("GenFormTest"));
        }

        [TestMethod]
        public void ThatSessionManagerCanReturnExportPath()
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(SettingsManager.Instance.GetExporthPath()));
        }

        [TestMethod]
        public void ThatSessionManagerCanReturnLogPath()
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(SettingsManager.Instance.GetLogPath()));
        }
    }
}
