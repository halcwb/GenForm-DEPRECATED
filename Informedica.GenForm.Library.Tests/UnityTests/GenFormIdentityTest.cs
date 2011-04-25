using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Library.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock;
using TypeMock.ArrangeActAssert;


namespace Informedica.GenForm.Library.Tests.UnityTests
{
    
    
    /// <summary>
    ///This is a test class for GenFormIdentityTest and is intended
    ///to contain all GenFormIdentityTest Unit Tests
    ///</summary>
    [TestClass]
    public class GenFormIdentityTest
    {


        private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
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
        public void GetIdentity_calls_GetUser_of_User()
        {
            const String name = "Admin";
            var user = CreateFakeIuser();
            var users = CreateUserListWithUser(user);
            IsolateGetUserName(users, name);

            try
            {
                GenFormIdentity.GetIdentity(name);
                Isolate.Verify.WasCalledWithExactArguments(() => User.GetUser(name));

            }
            catch (VerifyException e)
            {
                Assert.Fail("GenFormIdentity did not call User.GetUser: " + e);
            }
        }

        [Isolated]
        [TestMethod]
        public void GetIdentity_of_nonexistent_user_creates_AnonymousIdentity()
        {
            const String name = "foo";
            var users = CreateEmptyUserList();
            IsolateGetUserName(users, name);

            var result = GenFormIdentity.GetIdentity(name);

            Assert.IsInstanceOfType(result, typeof(IAnonymousIdentity), "Getidentity with nonexisting user did not return AnonymousIdentity");
            Assert.IsFalse(result.IsAuthenticated, "AnonymousIdentity should not be authenticated");
        }

        private static IUser CreateFakeIuser()
        {
            return Isolate.Fake.Instance<IUser>();
        }

        private static IEnumerable<IUser> CreateEmptyUserList()
        {
            return new List<IUser>();
        }

        private static IEnumerable<IUser> CreateUserListWithUser(IUser user)
        {
            return new List<IUser> {user};
        }

        private static void IsolateGetUserName(IEnumerable<IUser> users, String name)
        {
            Isolate.WhenCalled(() => User.GetUser(name)).WillReturn(users);
        }
    }
}
