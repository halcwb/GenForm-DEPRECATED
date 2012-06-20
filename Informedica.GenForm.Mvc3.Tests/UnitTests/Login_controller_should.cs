using System.Web;
using System;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Mvc3.Controllers;
using Informedica.GenForm.Presentation.Security;
using Informedica.GenForm.Services;
using Informedica.GenForm.Services.Environments;
using Informedica.GenForm.Services.UserLogin;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using StructureMap;
using TypeMock.ArrangeActAssert;
using LoginServices = Informedica.GenForm.Services.UserLogin.LoginServices;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for LoginControllerTest and is intended
    ///to contain all LoginControllerTest Unit Tests
    ///</summary>
    [TestClass]
    public class Login_controller_should
    {
        private LoginController _controller;
        private UserLoginDto _user;
        private HttpResponseBase _response;
        private HttpContextBase _context;
        private HttpSessionStateBase _sessionState;
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
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            ObjectFactory.Configure(x => x.Scan(scan =>
            {
                scan.AssemblyContainingType<IDatabaseServices>();
                scan.WithDefaultConventions();
            }));

            ObjectFactory.Configure(x => x.For<ISessionStateCache>().Use<HttpSessionStateCache>());

        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize]
        public void MyTestInitialize()
        {
            _sessionState = Isolate.Fake.Instance<HttpSessionStateBase>();
            ObjectFactory.Configure(x => x.For<HttpSessionStateBase>().Use(_sessionState));
            _controller = ObjectFactory.GetInstance<LoginController>();

            Isolate.WhenCalled(() => EnvironmentServices.SetEnvironment("Test")).IgnoreCall();
            Isolate.WhenCalled(() => SessionStateManager.InitializeDatabase(_sessionState)).IgnoreCall();

            _user = new UserLoginDto
            {
                UserName = "Admin",
                Password = "Admin",
                Environment = Testgenform
            };
            Isolate.WhenCalled(() => LoginServices.Login(_user)).IgnoreCall();

            _response = Isolate.Fake.Instance<HttpResponseBase>();
            Isolate.WhenCalled(() =>_controller.Response).WillReturn(_response);

            _context = Isolate.Fake.Instance<HttpContextBase>();
            Isolate.WhenCalled(() => _controller.HttpContext).WillReturn(_context);
            Isolate.WhenCalled(() => _context.Session).WillReturn(_sessionState);
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
        public void have_a_IDatabaseServices_to_initialize_the_database()
        {
            ObjectFactory.Configure(x => x.For<LoginController>().Use(_controller));
            var controller = ObjectFactory.GetInstance<LoginController>();

            Assert.IsNotNull(controller.DatabaseServices);
        }

        [Isolated]
        [TestMethod]
        public void store_the_environment_name_in_the_HttpContextSession_collection()
        {
            var userName = _user.UserName;
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(userName)).WillReturn(true);

            SetEnvironmentOnController();
            _controller.Login(_user);
            Isolate.Verify.WasCalledWithAnyArguments(() => _sessionState.Add(LoginController.EnvironmentSetting, Testgenform));
        }

        [Isolated]
        [TestMethod]
        public void return_an_action_result_with_success_is_true_after_set_environment()
        {
            SetEnvironmentOnController();
            Assert.IsTrue(ActionResultParser.GetSuccessValue(_controller.SetEnvironment(Testgenform)));
        }

        [Isolated]
        [TestMethod]
        public void return_an_action_result_with_an_exception_message_when_environment_has_not_been_set_before_login()
        {
            var userName = _user.UserName;
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(userName)).WillReturn(false);

            var result = _controller.Login(_user);
            Assert.AreEqual(LoginController.NoEnvironmentMessage, ActionResultParser.GetPropertyValue<string>(result, "message"));
        }

        [Isolated]
        [TestMethod]
        public void return_success_is_false_when_invalid_login()
        {
            var userName = _user.UserName;
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(userName)).WillReturn(false);

            SetEnvironmentOnController();
            var result = _controller.Login(_user);
            Assert.IsFalse(GetSuccessValueFromActionResult(result));
        }

        [Isolated]
        [TestMethod]
        public void request_login_from_login_services_using_LoginDto()
        {
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(string.Empty)).WillReturn(true);

            SetEnvironmentOnController();
            _controller.Login(_user);

            Isolate.Verify.WasCalledWithExactArguments(() => LoginServices.Login(_user));
        }

        [Isolated]
        [TestMethod]
        public void ask_login_services_if_user_is_logged_in()
        {
            var name = _user.UserName;
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(name)).WillReturn(true);

            SetEnvironmentOnController();
            _controller.Login(_user);

            Isolate.Verify.WasCalledWithExactArguments(() => LoginServices.IsLoggedIn(name));
        }

        [Isolated]
        [TestMethod]
        public  void return_success_is_true_when_valid_user_login()
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
        public void append_a_cookie_to_response_when_successfull_login()
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
        public  void return_success_is_true_when_password_change_for_valid_user()
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
        public void not_change_the_password_when_not_logged_in()
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
        public void return_a_login_presentation_with_disabled_login_button_for_invalid_login()
        {
            var controller = ObjectFactory.GetInstance<LoginController>();
            Isolate.Fake.StaticMethods(typeof(LoginForm));

            var result = controller.GetLoginPresentation("", "");

            Isolate.Verify.WasCalledWithAnyArguments(() => LoginForm.NewLoginForm("", ""));

            Assert.IsNotNull(result);
            Assert.IsFalse(GetLoginInButtonEnabledValue(result));
        }

        [Isolated]
        [TestMethod]
        public void return_login_presentation_for_valid_user_with_login_button_enabled()
        {
            Isolate.Fake.StaticMethods(typeof(LoginForm));

            var result = _controller.GetLoginPresentation("Admin", "Admin");

            Isolate.Verify.WasCalledWithAnyArguments(() => LoginForm.NewLoginForm("", ""));

            Assert.IsNotNull(result);
            Assert.IsFalse(GetLoginInButtonEnabledValue(result));
        }

        [Isolated]
        [TestMethod]
        public void provide_the_HttpSessionState_to_environment_services_when_set_environment_is_called()
        {
            _controller.SetEnvironment(Testgenform);

            Isolate.Verify.WasCalledWithAnyArguments(() => EnvironmentServices.SetHttpSessionCache(_sessionState));
        }


        [Isolated]
        [TestMethod]
        public void call_environment_services_to_set_HttpSessionCache_when_logging_in()
        {
            _controller.Login(_user);

            Isolate.Verify.WasCalledWithAnyArguments(() => EnvironmentServices.SetHttpSessionCache(_sessionState));
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
