using System;
using Informedica.GenForm.Library.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Informedica.GenForm.Library.Security;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Library.Tests
{
    
    
    /// <summary>
    ///This is a test class for ILoginServicesTest and is intended
    ///to contain all ILoginServicesTest Unit Tests
    ///</summary>
    [TestClass]
    public class ILoginServicesTest
    {


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



        [Isolated]
        [TestMethod]
        public void System_user_should_be_loggedout_after_being_logged_in()
        {
            var target = CreateILoginServices(); 
            var user = CreateSystemLoginUser();

            SetUpSystemUserLogout(user);

            target.Logout(user);

            Assert.IsFalse(target.IsLoggedIn(user), "User should be logged out, after logout");
        }

        private static void SetUpSystemUserLogout(ILoginUser user)
        {
            var identity = Isolate.Fake.Instance<IGenFormIdentity>();
            Isolate.WhenCalled(() => GenFormIdentity.GetIdentity(user)).WillReturn(identity);
            Isolate.WhenCalled(() => GenFormPrincipal.Logout()).IgnoreCall();
            Isolate.WhenCalled(() => GenFormPrincipal.IsLoggedIn()).WillReturn(false);
        }


        [Isolated]
        [TestMethod]
        public void IsLogged_returns_true_after_system_user_login()
        {
            var target = CreateILoginServices();
            var user = CreateSystemLoginUser();
            SetUpSystemUserLogin(user);

            target.Login(user);

            Assert.IsTrue(target.IsLoggedIn(user));
        }

        private static void SetUpSystemUserLogin(ILoginUser user)
        {
            Isolate.WhenCalled(() => GenFormPrincipal.Login(user)).IgnoreCall();
            Isolate.WhenCalled(() => GenFormPrincipal.IsLoggedIn()).WillReturn(true);
        }

        private static ILoginUser CreateSystemLoginUser()
        {
            return LoginUser.NewLoginUser("Admin", "Admin");
        }

        [Isolated]
        [TestMethod]
        public void When_system_user_logs_in_LoggedIn_returns_true()
        {
            var target = CreateILoginServices();
            var user = CreateSystemLoginUser();
            SetUpSystemUserLogin(user);

            Assert.IsTrue(target.IsLoggedIn(user), "System User should be logged in");
        }


        internal virtual ILoginServices CreateILoginServices()
        {
            return LoginServices.NewLoginServices();
        }

        /// <summary>
        ///A test for ChangePassword
        ///</summary>
        [TestMethod]
        public void System_user_can_change_password()
        {
            var target = CreateILoginServices();
            var user = CreateSystemLoginUser();
            SetUpSystemUserLogin(user);

            const string newPassword = "NewPassword";
            var oldPassword = user.Password;

            var principal = Isolate.Fake.Instance<IGenFormPrincipal>();
            Isolate.NonPublic.Property.WhenGetCalled(typeof(LoginServices), "Principal").WillReturn(principal);
            Isolate.WhenCalled(() => principal.ChangePassword(oldPassword, newPassword)).IgnoreCall();
            Isolate.WhenCalled(() => principal.CheckPassword(newPassword)).WillReturn(true);

            target.ChangePassword(user, newPassword);
            
        }
    }
}
