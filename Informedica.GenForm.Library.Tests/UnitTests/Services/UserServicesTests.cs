using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.Services.Users;
using Informedica.GenForm.Tests;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Services
{
    /// <summary>
    /// Summary description for UserServicesTests
    /// </summary>
    [TestClass]
    public class UserServicesTests : TestSessionContext
    {
        public UserServicesTests() : base (true)
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext) { GenFormApplication.Initialize(); }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ThatAuserCanBeGet()
        {
            var user = UserServices.WithDto(UserTestFixtures.GetValidUserDto()).Get();
            Assert.AreEqual(user.Name, UserTestFixtures.GetValidUserDto().Name);
        }

        [TestMethod]
        public void ThatUserCanBeRetrievedById()
        {
            var user = UserServices.WithDto(UserTestFixtures.GetValidUserDto()).Get();
            Assert.AreEqual(user.Name, UserTestFixtures.GetValidUserDto().Name);

            var user2 = UserServices.GetUserById(user.Id);
            Assert.AreSame(user, user2);
        }
    }
}
