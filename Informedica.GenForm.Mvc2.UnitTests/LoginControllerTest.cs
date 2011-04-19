using System;
using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Library.Services;
using Informedica.GenForm.Mvc2.Controllers;
using Informedica.GenForm.PresentationLayer.Security;
using Informedica.GenForm.ServiceProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Mvc2.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for LoginControllerTest and is intended
    ///to contain all LoginControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LoginControllerTest
    {
        private const String ValidUser = "Admin";
        private const String ValidPassword = "Admin";
        private const String TempPassword = "temp";
        private const String InvalidUser = "foo";
        private const String InvalidPassword = "bar";
        private const String SuccessProperty = "success";

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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
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

        private static bool GetSuccessValueFromActionResult(ActionResult response)
        {
            return (bool)((JsonResult)(response)).Data.GetType().GetProperty(SuccessProperty).GetValue(((JsonResult)(response)).Data, null);
        }

        [Isolated]
        [TestMethod]
        public void TestUserFooCannotLogin()
        {
            var controller = new LoginController();

            var response = controller.Login(InvalidUser, InvalidPassword);

            Assert.IsFalse(GetSuccessValueFromActionResult(response));
        }

        [Isolated]
        [TestMethod]
        public  void SystemUserCanLogin()
        {
            // Setup
            var user = GetUser();
            var loginServices = GetLoginServices();
            Isolate.WhenCalled(() => loginServices.Login(user)).IgnoreCall();
            Isolate.WhenCalled(() => loginServices.IsLoggedIn(user)).WillReturn(true);
            var controller = new LoginController();

            var response = controller.Login(ValidUser, ValidPassword);

            Assert.IsTrue(GetSuccessValueFromActionResult(response));
        }

        private static ILoginUser GetUser()
        {
            return GenFormServiceProvider.Instance.Resolve<ILoginUser>();
        }

        private static ILoginServices GetLoginServices()
        {
            return GenFormServiceProvider.Instance.Resolve<ILoginServices>();
        }

        [Isolated]
        [TestMethod]
        public  void LoggedInUserCanChangePassword()
        {
            ILoginController controller = new LoginController();
            var loginServices = GenFormServiceProvider.Instance.Resolve<ILoginServices>();
            Assert.IsTrue(loginServices.IsLoggedIn(GetUser()), "Cannot log in");

            Isolate.WhenCalled(() => loginServices.ChangePassword(GetUser(), TempPassword)).WillReturn(false);

            var response = controller.ChangePassword(ValidUser, ValidPassword, TempPassword);
            Isolate.Verify.WasCalledWithExactArguments(() => loginServices.ChangePassword(GetUser(), TempPassword));            

            Assert.IsTrue(GetSuccessValueFromActionResult(response), "Password was not changed");
        }

        [Isolated]
        [TestMethod]
        public void NotLoggedInUserCanNotChangePassword()
        {
            ILoginController controller = new LoginController();
            var loginServices = GenFormServiceProvider.Instance.Resolve<ILoginServices>();

            Isolate.WhenCalled(() => loginServices.ChangePassword(GetUser(), "newpassword")).WillReturn(true);

            var response = controller.ChangePassword("foo", "oldpassword", "newpassword");
            Isolate.Verify.WasCalledWithExactArguments(() => loginServices.ChangePassword(GetUser(), "newpassword"));

            Assert.IsFalse(GetSuccessValueFromActionResult(response));
        }

        [Isolated]
        [TestMethod]
        public void GetLoginFormModelWithDisabledLoginButtonUsingInvalidData()
        {
            ILoginController controller = new LoginController();
            Isolate.Fake.StaticMethods(typeof(LoginForm));

            var result = controller.GetLoginFormModel("", "");

            Isolate.Verify.WasCalledWithAnyArguments(() => LoginForm.NewLoginForm("", ""));

            Assert.IsNotNull(result);
            Assert.IsFalse(GetLoginInButtonEnabledValue(result));
        }

        [Isolated]
        [TestMethod]
        public void GetLoginFormModelWithEnabledLoginButtonUsingValidData()
        {
            ILoginController controller = new LoginController();
            Isolate.Fake.StaticMethods(typeof(LoginForm));

            var result = controller.GetLoginFormModel("Admin", "Admin");

            Isolate.Verify.WasCalledWithAnyArguments(() => LoginForm.NewLoginForm("", ""));

            Assert.IsNotNull(result);
            Assert.IsFalse(GetLoginInButtonEnabledValue(result));
        }

        private static bool GetLoginInButtonEnabledValue(ActionResult result)
        {
            var form = (ILoginForm)((JsonResult)(result)).Data.GetType().GetProperty("data").GetValue(((JsonResult)(result)).Data, null);
            return form.Login.Enabled;
        }

    }
}
