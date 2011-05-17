using System;
using System.Threading;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DataAccess;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Library.ServiceProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Library.Tests.UnityTests
{
    
    
    /// <summary>
    ///This is a test class for GenFormPrincipalTest and is intended
    ///to contain all GenFormPrincipalTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GenFormPrincipalTest
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            var repository = (IRepository<IUser>) new UserRepository();
            DalServiceProvider.Instance.RegisterInstanceOfType(repository);
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
        public void Login_system_user_results_in_PrincipalIdentity()
        {
            ILoginCriteria user = CreateSystemUser();
            IsolateGetIdentity();
            GenFormPrincipal.Login(user);

            Assert.IsNotNull(GenFormPrincipal.GetPrincipal().Identity, "Principal identity should be set");
        }

        private void IsolateGetIdentity()
        {
            var identity = Isolate.Fake.Instance<GenFormIdentity>();
            Isolate.NonPublic.WhenCalled(typeof(GenFormPrincipal), "GetIdentity").WillReturn(identity);
        }

        private static ILoginCriteria CreateSystemUser()
        {
            return LoginUser.NewLoginUser("Admin", "Admin");
        }

        [Isolated]
        [TestMethod]
        public void Login_with_system_user_and_password_bar_should_return_false()
        {
            var user = CreateSystemUser();
            user.Password = "bar";
            GenFormPrincipal.Login(user);

            Assert.IsFalse(GenFormPrincipal.GetPrincipal().IsLoggedIn(), "System user should not be able to login with password bar");

        }

        [Isolated]
        [TestMethod]
        public void Login_calls_SetPrincipal_with_identity()
        {
            var user = CreateSystemUser();
            var identity = CreateFakeGenFormIdentity();
            Isolate.WhenCalled(() => GenFormIdentity.GetIdentity(user)).WillReturn(identity);
            Isolate.NonPublic.WhenCalled(typeof(GenFormPrincipal), "SetPrincipal").CallOriginal();

            try
            {
                GenFormPrincipal.Login(user);
                Isolate.Verify.NonPublic.WasCalled(typeof(GenFormPrincipal), "SetPrincipal").WithArguments(identity);
            }
            catch (VerifyException e)
            {
                Assert.Fail("SetPrincipal was not called using a fake identity: " + e);
            }
        }

        private static IGenFormIdentity CreateFakeGenFormIdentity()
        {
            return Isolate.Fake.Instance<IGenFormIdentity>();
        }

        [Isolated]
        [TestMethod]
        public void IsLoggedIn_returns_false_for_anonymousidentity()
        {
            var principal = GenFormPrincipal.GetPrincipal();

            Assert.IsFalse(principal.IsLoggedIn(), "Principal that is not logged in should return false");
        }

        [Isolated]
        [TestMethod]
        public void GetGenFormPrincipal_should_not_return_null_when_threadprincipal_is_set_to_null()
        {
            Thread.CurrentPrincipal = null;

            Assert.IsNotNull(GenFormPrincipal.GetPrincipal(), "GetPrincipal should never return null");
        }
    }
}
