using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.Settings.Tests
{
    
    
    /// <summary>
    ///This is a test class for SettingsManagerTest and is intended
    ///to contain all SettingsManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SettingsManagerTests
    {
        private const string EnvironmentPath = @"C:\Users\halcwb\Documents\Visual Studio 2010\Projects\GenForm\Informedica.GenForm.Mvc3\";
        private const string EnvironmentConnectionString = @"Data Source=HAL-WIN7\\INFORMEDICA;Initial Catalog=GenFormTest;Integrated Security=True";


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
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


        /// <summary>
        ///A test for Initialize
        ///</summary>
        [TestMethod()]
        public void ThatSettingsManagerInitializesExistingFilePath()
        {
            SettingsManager.Instance.Initialize();
        }

        [TestMethod]
        public void ThatSettingsManagerCanRegisterASetting()
        {
            var environment = "Test";
            var connectionString = "TestConnection";

            SettingsManager.Instance.WriteSecureSetting(environment, connectionString);
        }

        [TestMethod]
        public void ThatSettingsManagerCanBeInitializedUsingExistingPath()
        {
            SettingsManager.Instance.Initialize(EnvironmentPath);
        }

        [TestMethod]
        public void ThatSettingsManagerCanRegisterEnvironmentInPath()
        {
            SettingsManager.Instance.Initialize(EnvironmentPath);
            SettingsManager.Instance.WriteSecureSetting("GenFormTest", EnvironmentConnectionString);

            Assert.AreEqual(EnvironmentConnectionString, SettingsManager.Instance.ReadSecureSetting("GenFormTest"));
        }
    }
}
