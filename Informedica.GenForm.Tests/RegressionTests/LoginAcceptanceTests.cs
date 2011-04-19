using System;
using Informedica.GenForm.Mvc2.Controllers;
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
        public void LoginSystemUserReturnsLoggedIn()
        {
            // Setup
            ILoginController loginController = new LoginController();
            
            // Execute
            var result = loginController.Login(SystemUserName, SystemUserPassword);
            
            // Verify
            Assert.IsTrue(ActionResultParser.GetSuccessValueFromActionResult(result), "System user successfully logged in");

            // No Teardown
        }
    }
}
