using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Library.Services.Users;
using Informedica.GenForm.Mvc3.Controllers;
using Informedica.GenForm.PresentationLayer.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for LoginControllerTest and is intended
    ///to contain all LoginControllerTest Unit Tests
    ///</summary>
    [TestClass]
    public class LoginControllerShould
    {
        private const String ValidUser = "Admin";
        private const String ValidPassword = "Admin";
        private const String TempPassword = "temp";
        private const String InvalidUser = "foo";
        private const String InvalidPassword = "bar";

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
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            GenFormApplication.Initialize();
        }
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

        [Isolated]
        [TestMethod]
        public void ReturnFalseForInvalidUserLogin()
        {
            
            var controller = new LoginController();
            var user = GetUser();
            IsolateLoginController(user);
            Isolate.Fake.StaticMethods(typeof(LoginServices));
            Isolate.WhenCalled(() => LoginServices.Login(user)).IgnoreCall();

            var response = controller.Login(InvalidUser, InvalidPassword);

            Assert.IsFalse(GetSuccessValueFromActionResult(response));
        }

        [Isolated]
        [TestMethod]
        public  void ReturnSuccessValueIsTrueWhenValidUserLogin()
        {
            // Setup
            var user = GetUser();
            IsolateLoginController(user);

            Isolate.WhenCalled(() => LoginServices.Login(user)).IgnoreCall();
            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(user)).WillReturn(true);
            var controller = new LoginController();
            Isolate.NonPublic.WhenCalled(controller, "SetLoginCookie").IgnoreCall();

            var response = controller.Login(ValidUser, ValidPassword);

            Assert.IsTrue(GetSuccessValueFromActionResult(response));
        }

        [Isolated]
        [TestMethod]
        public  void ReturnSuccessValueTrueForPasswordChangeForValidUser()
        {
            var controller = GetController();
            var user = GetUser();
            IsolateLoginController(user);

            Isolate.WhenCalled(() => LoginServices.IsLoggedIn(user)).WillReturn(true);
            Assert.IsTrue(LoginServices.IsLoggedIn(GetUser()), "Cannot log in");

            Isolate.WhenCalled(() => LoginServices.ChangePassword(user, TempPassword)).IgnoreCall();
            Isolate.WhenCalled(() => LoginServices.CheckPassword(TempPassword)).WillReturn(true);

            var response = controller.ChangePassword(ValidUser, ValidPassword, TempPassword);
            Isolate.Verify.WasCalledWithExactArguments(() => LoginServices.ChangePassword(user, TempPassword));            

            Assert.IsTrue(GetSuccessValueFromActionResult(response), "Password was not changed");
        }

        [Isolated]
        [TestMethod]
        public void NotChangePasswordWhenNotLoggedIn()
        {
            var controller = GetController();
            var user = GetUser();
            IsolateLoginController(user);

            Isolate.Fake.StaticMethods(typeof(LoginServices));
            Isolate.WhenCalled(() => LoginServices.ChangePassword(user, "newpassword")).IgnoreCall();

            var response = controller.ChangePassword("foo", "oldpassword", "newpassword");
            Isolate.Verify.WasCalledWithExactArguments(() => LoginServices.ChangePassword(user, "newpassword"));

            Assert.IsFalse(GetSuccessValueFromActionResult(response), "Password was not changed");
        }

        [Isolated]
        [TestMethod]
        public void ReturnLoginPresentationWithDisabledLoginButtonForInValidLogin()
        {
            LoginController controller = new LoginController();
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
            LoginController controller = new LoginController();
            Isolate.Fake.StaticMethods(typeof(LoginForm));

            var result = controller.GetLoginPresentation("Admin", "Admin");

            Isolate.Verify.WasCalledWithAnyArguments(() => LoginForm.NewLoginForm("", ""));

            Assert.IsNotNull(result);
            Assert.IsFalse(GetLoginInButtonEnabledValue(result));
        }

        private static LoginController GetController()
        {
            return new LoginController();
        }

        private static ILoginCriteria GetUser()
        {
            return Isolate.Fake.Instance<ILoginCriteria>();
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

        private static void IsolateLoginController(ILoginCriteria user)
        {
            Isolate.Fake.StaticMethods<LoginUser>();
            Isolate.WhenCalled(() => LoginUser.NewLoginUser(InvalidUser, InvalidPassword)).WillReturn(user);
        }

    }
}
