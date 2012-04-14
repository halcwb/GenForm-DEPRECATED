using Informedica.SecureSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.SettingManager
{
    /// <summary>
    /// Summary description for SettingsManagerShould
    /// </summary>
    [TestClass]
    public class SettingsManagerShould
    {
        private const string AppSettingValue = "Test app setting";
        private const string AppSettingName = "testSetting";

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

        [Isolated]
        [TestMethod]
        public void BeCreatedWithASecureSettingsManager()
        {
            var fakeISettingSource = Isolate.Fake.Instance<ISettingSource>();
            try
            {
                new SettingsManager(new SecureSettingSource(fakeISettingSource));

            }
            catch (System.Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void BeAbleToRetrieveAConnectionStringByName()
        {
            const string exp = "Data Source=:memory:;Version=3;New=True;Pooling=True;Max Pool Size=1;";
            SettingsManager.Instance.AddConnectionString("Test", exp);
            var con = SettingsManager.Instance.GetConnectionString("Test").ConnectionString;
            Assert.AreEqual(exp, con);
        }

        [TestMethod]
        public void BeAbleToAddAndRemoveAnConnectionString()
        {
            SettingsManager.Instance.AddConnectionString("test", "test");
            SettingsManager.Instance.RemoveConnectionString("test");

            Assert.IsNotNull(SettingsManager.Instance.GetConnectionString("test"));
        }

        [TestMethod]
        public void BeAbleToSetAnAppSetting()
        {
            SettingsManager.Instance.WriteSecureSetting(AppSettingName, AppSettingValue);

            Assert.AreEqual(AppSettingValue, SettingsManager.Instance.ReadSecureSetting(AppSettingName));
        }

        [TestMethod]
        public void BeAbleToRemoveAnAppSetting()
        {
            if(string.IsNullOrWhiteSpace(SettingsManager.Instance.ReadSecureSetting(AppSettingName))) 
                SettingsManager.Instance.WriteSecureSetting(AppSettingName, AppSettingValue);

            SettingsManager.Instance.RemoveSecureSetting(AppSettingName);
            Assert.IsTrue(string.IsNullOrWhiteSpace(SettingsManager.Instance.ReadSecureSetting(AppSettingName)));
        }
    }
}
