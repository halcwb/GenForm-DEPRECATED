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
        private HttpContextBase _context;
        private const String ValidUser = "Admin";
        private const String ValidPassword = "Admin";
        private const String TempPassword = "temp";
        private const string Testgenform = "TestGenForm";

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

            _context = Isolate.Fake.Instance<HttpContextBase>();
            Isolate.WhenCalled(() => _controller.HttpContext).WillReturn(_context);
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
        public void StoreTheEnvironmentNameInTheHttpContextSessionCollection()
        {
            _user = new UserLoginDto
                        {
                            UserName = "Admin",
                            Password = "Admin",
                            Environment = Testgenform
                        };

            var userName = _user.UserName;
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(userName)).WillReturn(true);

            SetEnvironmentOnController();
            _controller.Login(_user);
            Isolate.Verify.WasCalledWithAnyArguments(() => _context.Session.Add(LoginController.EnvironmentSetting, Testgenform));
        }

        [Isolated]
        [TestMethod]
        public void ReturnActionResultWithSuccessIsTrueAfterSetEnvironmentActionMethodCall()
        {
            SetEnvironmentOnController();
            Assert.IsTrue(ActionResultParser.GetSuccessValue(_controller.SetEnvironment(Testgenform)));
        }

        [Isolated]
        [TestMethod]
        public void ReturnALoginExceptionMessageWhenEnvironmentHasNotBeenSetBeforeLogin()
        {
            var userName = _user.UserName;
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(userName)).WillReturn(false);

            var result = _controller.Login(_user);
            Assert.AreEqual(LoginController.NoEnvironmentMessage, ActionResultParser.GetPropertyValue<string>(result, "message"));
        }

        [Isolated]
        [TestMethod]
        public void ReturnFalseForInvalidUserLogin()
        {
            var userName = _user.UserName;
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(userName)).WillReturn(false);

            SetEnvironmentOnController();
            var result = _controller.Login(_user);
            Assert.IsFalse(GetSuccessValueFromActionResult(result));
        }

        [Isolated]
        [TestMethod]
        public void RequestLoginFromLoginServicesUsingLoginDto()
        {
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(string.Empty)).WillReturn(true);

            SetEnvironmentOnController();
            _controller.Login(_user);

            Isolate.Verify.WasCalledWithExactArguments(() => LoginServices.Login(_user));
        }

        [Isolated]
        [TestMethod]
        public void AskLoginServicesIfUserIsLoggedIn()
        {
            var name = _user.UserName;
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(name)).WillReturn(true);

            SetEnvironmentOnController();
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

            SetEnvironmentOnController();
            var result = _controller.Login(_user);
            
            Assert.IsTrue(GetSuccessValueFromActionResult(result));
        }

        [Isolated]
        [TestMethod]
        public void AppendALoginCookieToResponseWhenSuccessfulLogin()
        {
            var cookie = Isolate.Fake.Instance<HttpCookie>();
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(ValidUser)).WillReturn(true);

            Isolate.WhenCalled(() => _controller.Response).WillReturn(_response);

            SetEnvironmentOnController();
            _controller.Login(_user);

            Isolate.Verify.WasCalledWithAnyArguments(() => _response.AppendCookie(cookie));
        }

        [Isolated]
        [TestMethod]
        public  void ReturnSuccessValueTrueForPasswordChangeForValidUser()
        {
            Isolate.WhenCalled(() => LoginServices.ChangePassword(_user, TempPassword)).IgnoreCall();
            Isolate.WhenCalled(() => LoginServices.CheckPassword(TempPassword)).WillReturn(true);

            SetEnvironmentOnController();
            var result = _controller.ChangePassword(ValidUser, ValidPassword, TempPassword);

            Isolate.Verify.WasCalledWithAnyArguments(() => LoginServices.ChangePassword(_user, TempPassword));            
            Assert.IsTrue(GetSuccessValueFromActionResult(result), "Password was not changed");
        }

        [Isolated]
        [TestMethod]
        public void NotChangePasswordWhenNotLoggedIn()
        {
            Isolate.Fake.StaticMethods(typeof(LoginServices));
            Isolate.WhenCalled(() => LoginServices.ChangePassword(_user, "newpassword")).IgnoreCall();

            SetEnvironmentOnController();
            var result = _controller.ChangePassword("foo", "oldpassword", "newpassword");
            Isolate.Verify.WasCalledWithAnyArguments(() => LoginServices.ChangePassword(_user, "newpassword"));

            Assert.IsFalse(GetSuccessValueFromActionResult(result), "Password was not changed");
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

        [Isolated]
        [TestMethod]
        public void ProvideTheHttpSessionStateBaseToEnvironmentServicesWhenSetEnvironmentIsCalled()
        {
            _controller.SetEnvironment(Testgenform);

            var session = Isolate.Fake.Instance<HttpSessionStateBase>();
            Isolate.Verify.WasCalledWithAnyArguments(() => EnvironmentServices.SetHttpSessionCache(session));
        }

        private void SetEnvironmentOnController()
        {
            Isolate.WhenCalled(() => _context.Session[LoginController.EnvironmentSetting]).WillReturn(Testgenform);
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
