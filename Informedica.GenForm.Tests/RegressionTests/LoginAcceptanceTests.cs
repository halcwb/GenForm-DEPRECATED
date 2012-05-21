using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.Services.Users;
using Informedica.GenForm.Mvc3.Controllers;
using Informedica.GenForm.Services.UserLogin;
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

        public LoginAcceptanceTests()
            : base(true)
        {
        }

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
            UserServices.WithDto(GetAdminDto()).Get();
            var loginController = CreateLoginController();
            var dto = new UserLoginDto
                          {
                              UserName = SystemUserName,
                              Password = SystemUserPassword,
                              Environment = "Test"
                          };

            // Execute
            var result = loginController.Login(dto);

            // Verify
            Assert.IsTrue(ActionResultParser.GetSuccessValue(result), "System user could not successfully log in");

            // No Teardown
        }

        private UserDto GetAdminDto()
        {
            return new UserDto
                       {
                           Email = "admin@admin.com",
                           FirstName = "Admin",
                           LastName = "Admin",
                           Name = "Admin",
                           Pager = "123",
                           Password = "Admin"
                       };
        }

        [TestMethod]
        public void SystemUserCannotLoginWithInvalidPassword()
        {
            var loginController = CreateLoginController();
            var dto = new UserLoginDto
            {
                UserName = SystemUserName,
                Password = "bar",
                Environment = "Test"
            };

            var result = loginController.Login(dto);

            Assert.IsFalse(ActionResultParser.GetSuccessValue(result), "System should not be able with password bar");
        }

        [TestMethod]
        public void InvalidUserCannotLoginWithInvalidPassword()
        {
            var loginController = CreateLoginController();
            var dto = new UserLoginDto
            {
                UserName = "foo",
                Password = "bar",
                Environment = "Test"
            };

            var result = loginController.Login(dto);

            Assert.IsFalse(ActionResultParser.GetSuccessValue(result), "User foo cannot login with password bar (if not added as users)");
        }

        [TestMethod]
        public void UserWithoutUserNameCannotLogin()
        {
            var loginController = CreateLoginController();
            var dto = new UserLoginDto
            {
                UserName = string.Empty,
                Password = "bar",
                Environment = "Test"
            };

            var result = loginController.Login(dto);

            Assert.IsFalse(ActionResultParser.GetSuccessValue(result), "User without username cannot log in");
        }

        [TestMethod]
        public void UserWithoutPasswordCannotLogin()
        {
            var loginController = CreateLoginController();
            var dto = new UserLoginDto
            {
                UserName = "foo",
                Password = string.Empty,
                Environment = "Test"
            };

            var result = loginController.Login(dto);

            Assert.IsFalse(ActionResultParser.GetSuccessValue(result), "User without a password cannot login");
        }

        private static LoginController CreateLoginController()
        {
            var loginController = new LoginController();
            loginController.ControllerContext = new ControllerContext
                                                    {
                                                        Controller = loginController,
                                                        RequestContext = new RequestContext(MockHttpContext(), new RouteData())
                                                    };
            return loginController;
        }

        private static HttpContextBase MockHttpContext()
        {
            return new HttpContextWrapper(new HttpContext(new HttpRequest("", "http://localhost/genform/default.aspx", ""), new HttpResponse(TextWriter.Null)));
        }
    }
}
