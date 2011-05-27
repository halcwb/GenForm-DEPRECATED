using System;
using Informedica.GenForm.Mvc3.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Tests.RegressionTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class LoginAcceptanceTests
    {
        private const String SystemUserName = "Admin";
        private const String SystemUserPassword = "Admin";

        public LoginAcceptanceTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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

        [TestMethod]
        public void System_user_can_login()
        {
            // Setup
            var loginController = CreateLoginController();
            
            // Execute
            var result = loginController.Login(SystemUserName, SystemUserPassword);
            
            // Verify
            Assert.IsTrue(ActionResultParser.GetSuccessValueFromActionResult(result), "System user could not successfully log in");

            // No Teardown
        }

        [TestMethod]
        public void System_user_cannot_login_with_password_bar()
        {
            var loginController = CreateLoginController();

            var result = loginController.Login(SystemUserName, "bar");

            Assert.IsFalse(ActionResultParser.GetSuccessValueFromActionResult(result), "System should not be able with password bar");
        }

        [TestMethod]
        public void User_foo_cannot_login_with_password_bar()
        {
            var loginController = CreateLoginController();

            var result = loginController.Login("foo", "bar");

            Assert.IsFalse(ActionResultParser.GetSuccessValueFromActionResult(result), "User foo cannot login with password bar (if not added as users)");
        }

        private static ILoginController CreateLoginController()
        {
            return new LoginController();
        }

        [TestMethod]
        public void User_without_username_cannot_login()
        {
            var loginController = CreateLoginController();

            var result = loginController.Login("", "bar");

            Assert.IsFalse(ActionResultParser.GetSuccessValueFromActionResult(result), "User without username cannot log in");
        }

        [TestMethod]
        public  void User_without_password_cannot_login()
        {
            var loginController = CreateLoginController();

            var result = loginController.Login("foo", "");

            Assert.IsFalse(ActionResultParser.GetSuccessValueFromActionResult(result), "User without a password cannot login");
        }
    }
}
