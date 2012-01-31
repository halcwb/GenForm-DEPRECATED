using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Library.Services.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Library.Tests.UnitTests.Services
{
    
    
    /// <summary>
    ///This is a test class for ILoginServicesTest and is intended
    ///to contain all ILoginServicesTest Unit Tests
    ///</summary>
    [TestClass]
    public class LoginServicesShould
    {
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
        public void BeAbleToLogoutSystemUserAfterLogin()
        {
            var user = CreateSystemLoginCriteria();

            IsolateSystemUserLogoutReturnsLoggedInFalse(user);

            LoginServices.Logout(user);

            Assert.IsFalse(LoginServices.IsLoggedIn(user), "User should be logged out, after logout");
        }

        private static void IsolateSystemUserLogoutReturnsLoggedInFalse(ILoginCriteria criteria)
        {
            var identity = Isolate.Fake.Instance<IGenFormIdentity>();
            Isolate.WhenCalled(() => GenFormIdentity.GetIdentity(criteria)).WillReturn(identity);
// ReSharper disable ConvertClosureToMethodGroup
            Isolate.WhenCalled(() => GenFormPrincipal.Logout()).IgnoreCall();
// ReSharper restore ConvertClosureToMethodGroup
            Isolate.WhenCalled(() => GenFormPrincipal.GetPrincipal().IsLoggedIn()).WillReturn(false);
        }


        [Isolated]
        [TestMethod]
        public void ReturnTrueAfterSuccessfullLogin()
        {
            var user = CreateSystemLoginCriteria();
            IsolateSystemUserLoginReturnsTrue(user);

            LoginServices.Login(user);

            Assert.IsTrue(LoginServices.IsLoggedIn(user));
        }

        [Isolated]
        [TestMethod]
        public void ReturnTrueAfterSystemUserLogin()
        {
            var criteria = CreateSystemLoginCriteria();
            IsolateSystemUserLoginReturnsTrue(criteria);

            Assert.IsTrue(LoginServices.IsLoggedIn(criteria), "System User should be logged in");
        }


        [Isolated]
        [TestMethod]
        public void SystemUserCanChangePassword()
        {
            var user = CreateSystemLoginCriteria();
            IsolateSystemUserLoginReturnsTrue(user);
            
            const string newPassword = "NewPassword";
            var oldPassword = user.Password;

            var principal = Isolate.Fake.Instance<IGenFormPrincipal>();
            Isolate.NonPublic.Property.WhenGetCalled(typeof(LoginServices), "Principal").WillReturn(principal);
            Isolate.WhenCalled(() => principal.ChangePassword(oldPassword, newPassword)).IgnoreCall();
            Isolate.WhenCalled(() => principal.CheckPassword(newPassword)).WillReturn(true);

            LoginServices.ChangePassword(user, newPassword);
            
        }

        private static void IsolateSystemUserLoginReturnsTrue(ILoginCriteria criteria)
        {
            Isolate.WhenCalled(() => GenFormPrincipal.Login(criteria)).IgnoreCall();
            Isolate.WhenCalled(() => GenFormPrincipal.GetPrincipal().IsLoggedIn()).WillReturn(true);
        }

        private static ILoginCriteria CreateSystemLoginCriteria()
        {
            return LoginUser.NewLoginUser("Admin", "Admin");
        }

    }
}
