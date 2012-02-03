using TypeMock.ArrangeActAssert;
using Informedica.SecureSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Settings.Tests
{
    /// <summary>
    /// Summary description for SettingsManagerShould
    /// </summary>
    [TestClass]
    public class SettingsManagerShould
    {
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
                new SettingsManager(new SecureSettingsManager(fakeISettingSource));

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
            var con = SettingsManager.Instance.GetConnectionString("Test");
            Assert.AreEqual(exp, con);
        }
    }
}
