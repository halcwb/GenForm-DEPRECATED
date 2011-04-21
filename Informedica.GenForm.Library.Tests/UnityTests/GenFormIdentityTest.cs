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
    [TestClass()]
    public class GenFormIdentityTest
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
        [TestMethod()]
        public void GetIdentity_calls_GetUser_of_User()
        {
            const String name = "Admin";
            var user = Isolate.Fake.Instance<IUser>();
            var users = (IEnumerable<IUser>) new List<IUser> { user };
            Isolate.WhenCalled(() => User.GetUser(name)).WillReturn(users);

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
    }
}
