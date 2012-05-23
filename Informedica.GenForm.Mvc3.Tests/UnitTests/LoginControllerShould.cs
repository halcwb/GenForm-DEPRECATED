using System.Web;
using System;
using Informedica.GenForm.Mvc3.Controllers;
using Informedica.GenForm.Presentation.Security;
using Informedica.GenForm.Services.Environments;
using Informedica.GenForm.Services.UserLogin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using TypeMock.ArrangeActAssert;
using LoginServices = Informedica.GenForm.Services.UserLogin.LoginServices;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for LoginControllerTest and is intended
    ///to contain all LoginControllerTest Unit Tests
    ///</summary>
    [TestClass]
    public class LoginControllerShould
    {
        private LoginController _controller;
        private UserLoginDto _user;
        private HttpResponseBase _response;
        private const String ValidUser = "Admin";
        private const String ValidPassword = "Admin";
        private const String TempPassword = "temp";
        private const String InvalidUser = "foo";
        private const String InvalidPassword = "bar";

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //    GenFormApplication.Initialize();
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _controller = new LoginController();
            Isolate.WhenCalled(() => EnvironmentServices.SetEnvironment("Test")).IgnoreCall();

            _user = Isolate.Fake.Instance<UserLoginDto>();
            Isolate.WhenCalled(() => LoginServices.Login(_user)).IgnoreCall();

            _response = Isolate.Fake.Instance<HttpResponseBase>();
            Isolate.WhenCalled(() =>_controller.Response).WillReturn(_response);
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [Isolated]
        [TestMethod]
        public void ReturnFalseForInvalidUserLogin()
        {

            _user = new UserLoginDto
                        {
                            UserName = InvalidUser,
                            Password = InvalidPassword,
                            Environment = "Test"
                        };
            var userName = _user.UserName;
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(userName)).WillReturn(false);

            var response = _controller.Login(_user);
            Assert.IsFalse(GetSuccessValueFromActionResult(response));
        }

        [Isolated]
        [TestMethod]
        public void RequestLoginFromLoginServicesUsingLoginDto()
        {
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(string.Empty)).WillReturn(true);

            _controller.Login(_user);

            Isolate.Verify.WasCalledWithExactArguments(() => LoginServices.Login(_user));
        }

        [Isolated]
        [TestMethod]
        public void AskLoginServicesIfUserIsLoggedIn()
        {
            var name = _user.UserName;
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(name)).WillReturn(true);

            _controller.Login(_user);

            Isolate.Verify.WasCalledWithExactArguments(() => LoginServices.IsLoggedIn(name));
        }

        [Isolated]
        [TestMethod]
        public  void ReturnSuccessValueIsTrueWhenValidUserLogin()
        {
            // Setup            
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(ValidUser)).WillReturn(true);
            Isolate.WhenCalled(() => LoginServices.GetLoggedIn()).WillReturn(ValidUser);
            Isolate.WhenCalled(() => _controller.Response).ReturnRecursiveFake();

            var response = _controller.Login(_user);
            
            Assert.IsTrue(GetSuccessValueFromActionResult(response));
        }

        [Isolated]
        [TestMethod]
        public void AppendALoginCookieWhenSuccessfulLogin()
        {
            var cookie = Isolate.Fake.Instance<HttpCookie>();
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(ValidUser)).WillReturn(true);

            Isolate.WhenCalled(() => _controller.Response).WillReturn(_response);

            _controller.Login(_user);

            Isolate.Verify.WasCalledWithAnyArguments(() => _response.AppendCookie(cookie));
        }

        [Isolated]
        [TestMethod]
        public  void ReturnSuccessValueTrueForPasswordChangeForValidUser()
        {
            Isolate.WhenCalled(() => LoginServices.ChangePassword(_user, TempPassword)).IgnoreCall();
            Isolate.WhenCalled(() => LoginServices.CheckPassword(TempPassword)).WillReturn(true);

            var response = _controller.ChangePassword(ValidUser, ValidPassword, TempPassword);

            Isolate.Verify.WasCalledWithAnyArguments(() => LoginServices.ChangePassword(_user, TempPassword));            
            Assert.IsTrue(GetSuccessValueFromActionResult(response), "Password was not changed");
        }

        [Isolated]
        [TestMethod]
        public void NotChangePasswordWhenNotLoggedIn()
        {
            Isolate.Fake.StaticMethods(typeof(LoginServices));
            Isolate.WhenCalled(() => LoginServices.ChangePassword(_user, "newpassword")).IgnoreCall();

            var response = _controller.ChangePassword("foo", "oldpassword", "newpassword");
            Isolate.Verify.WasCalledWithAnyArguments(() => LoginServices.ChangePassword(_user, "newpassword"));

            Assert.IsFalse(GetSuccessValueFromActionResult(response), "Password was not changed");
        }

        [Isolated]
        [TestMethod]
        public void ReturnLoginPresentationWithDisabledLoginButtonForInValidLogin()
        {
            var controller = new LoginController();
            Isolate.Fake.StaticMethods(typeof(LoginForm));

            var result = controller.GetLoginPresentation("", "");

            Isolate.Verify.WasCalledWithAnyArguments(() => LoginForm.NewLoginForm("", ""));

            Assert.IsNotNull(result);
            Assert.IsFalse(GetLoginInButtonEnabledValue(result));
        }

        [Isolated]
        [TestMethod]
        public void ReturnLoginPresentationForValidUserWithLoginButtonEnabled()
        {
            Isolate.Fake.StaticMethods(typeof(LoginForm));

            var result = _controller.GetLoginPresentation("Admin", "Admin");

            Isolate.Verify.WasCalledWithAnyArguments(() => LoginForm.NewLoginForm("", ""));

            Assert.IsNotNull(result);
            Assert.IsFalse(GetLoginInButtonEnabledValue(result));
        }

        private static UserLoginDto GetUser()
        {
            return new UserLoginDto();
        }

        private static bool GetSuccessValueFromActionResult(ActionResult response)
        {
            return ActionResultParser.GetSuccessValue(response);
        }

        private static bool GetLoginInButtonEnabledValue(ActionResult result)
        {
            var form = (ILoginForm)((JsonResult)(result)).Data.GetType().GetProperty("data").GetValue(((JsonResult)(result)).Data, null);
            return form.Login.Enabled;
        }

    }
}
