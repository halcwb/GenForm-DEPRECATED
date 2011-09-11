using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Library.Tests.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for GenFormPrincipalTest and is intended
    ///to contain all GenFormPrincipalTest Unit Tests
    ///</summary>
    [TestClass]
    public class GenFormPrincipalTest : TestSessionContext
    {
        private TestContext testContextInstance;

        public GenFormPrincipalTest() : base(true)
        {
        }

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
        //[TestInitialize]
        //public void MyTestInitialize()
        //{
        //    GenFormApplication.Initialize();
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
        public void LoginSystemUserResultsInPricipalIdentityBeingSet()
        {
            ILoginCriteria user = CreateSystemUser();
            IsolateGetIdentity();
            GenFormPrincipal.Login(user);

            Assert.IsNotNull(GenFormPrincipal.GetPrincipal().Identity, "Principal identity should be set");
        }

        [Isolated]
        [TestMethod]
        public void LoginWithAsSystemUserWithWrongPasswordShouldReturnFalse()
        {
            var user = (LoginUser)CreateSystemUser();
            user.Password = "bar";
            GenFormPrincipal.Login(user);

            Assert.IsFalse(GenFormPrincipal.GetPrincipal().IsLoggedIn(), "System user should not be able to login with password bar");

        }

        [Isolated]
        [TestMethod]
        public void LoginCallsSetPrincipalWithIdentity()
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

        [Isolated]
        [TestMethod]
        public void IsLoggedInReturnsFalseForAnonymousUser()
        {
            var principal = GenFormPrincipal.GetPrincipal();

            Assert.IsFalse(principal.IsLoggedIn(), "Principal that is not logged in should return false");
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

        private static IGenFormIdentity CreateFakeGenFormIdentity()
        {
            return Isolate.Fake.Instance<IGenFormIdentity>();
        }

    }
}
