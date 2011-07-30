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
        public void BeAbleToLogoutSystemUserAfterLogin()
        {
            var target = CreateILoginServices(); 
            var user = CreateSystemLoginCriteria();

            IsolateSystemUserLogoutReturnsLoggedInFalse(user);

            target.Logout(user);

            Assert.IsFalse(target.IsLoggedIn(user), "User should be logged out, after logout");
        }

        private static void IsolateSystemUserLogoutReturnsLoggedInFalse(ILoginCriteria criteria)
        {
            var identity = Isolate.Fake.Instance<IGenFormIdentity>();
            Isolate.WhenCalled(() => GenFormIdentity.GetIdentity(criteria)).WillReturn(identity);
            Isolate.WhenCalled(() => GenFormPrincipal.Logout()).IgnoreCall();
            Isolate.WhenCalled(() => GenFormPrincipal.GetPrincipal().IsLoggedIn()).WillReturn(false);
        }


        [Isolated]
        [TestMethod]
        public void ReturnTrueAfterSuccessfullLogin()
        {
            var target = CreateILoginServices();
            var user = CreateSystemLoginCriteria();
            IsolateSystemUserLoginReturnsTrue(user);

            target.Login(user);

            Assert.IsTrue(target.IsLoggedIn(user));
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

        [Isolated]
        [TestMethod]
        public void ReturnTrueAfterSystemUserLogin()
        {
            var target = CreateILoginServices();
            var criteria = CreateSystemLoginCriteria();
            IsolateSystemUserLoginReturnsTrue(criteria);

            Assert.IsTrue(target.IsLoggedIn(criteria), "System User should be logged in");
        }


        private static ILoginServices CreateILoginServices()
        {
            return LoginServices.NewLoginServices();
        }

        [Isolated]
        [TestMethod]
        public void System_user_can_change_password()
        {
            var target = CreateILoginServices();
            var user = CreateSystemLoginCriteria();
            IsolateSystemUserLoginReturnsTrue(user);
            
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
