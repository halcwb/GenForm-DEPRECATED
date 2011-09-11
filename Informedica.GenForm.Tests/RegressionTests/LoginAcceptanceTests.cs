using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Mvc3.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Tests.RegressionTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class LoginAcceptanceTests : TestSessionContext
    {
        private const String SystemUserName = "Admin";
        private const String SystemUserPassword = "Admin";

        public LoginAcceptanceTests() : base(true)
        {
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
        [ClassInitialize]
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
        public void SystemUserCanLogin()
        {
            // Setup
            var loginController = CreateLoginController();
            
            // Execute
            var result = loginController.Login(SystemUserName, SystemUserPassword, "GenFormTest");
            
            // Verify
            Assert.IsTrue(ActionResultParser.GetSuccessValue(result), "System user could not successfully log in");

            // No Teardown
        }

        [TestMethod]
        public void SystemUserCannotLoginWithInvalidPassword()
        {
            var loginController = CreateLoginController();

            var result = loginController.Login(SystemUserName, "bar", "GenFormTest");

            Assert.IsFalse(ActionResultParser.GetSuccessValue(result), "System should not be able with password bar");
        }

        [TestMethod]
        public void InvalidUserCannotLoginWithInvalidPassword()
        {
            var loginController = CreateLoginController();

            var result = loginController.Login("foo", "bar", "GenFormTest");

            Assert.IsFalse(ActionResultParser.GetSuccessValue(result), "User foo cannot login with password bar (if not added as users)");
        }

        [TestMethod]
        public void UserWithoutUserNameCannotLogin()
        {
            var loginController = CreateLoginController();

            var result = loginController.Login("", "bar", "GenFormTest");

            Assert.IsFalse(ActionResultParser.GetSuccessValue(result), "User without username cannot log in");
        }

        [TestMethod]
        public  void UserWithoutPasswordCannotLogin()
        {
            var loginController = CreateLoginController();

            var result = loginController.Login("foo", "", "GenFormTest");

            Assert.IsFalse(ActionResultParser.GetSuccessValue(result), "User without a password cannot login");
        }

        private static LoginController CreateLoginController()
        {
            return new LoginController();
        }

    }
}
