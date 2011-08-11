using System;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Library.Services.Users;
using Informedica.GenForm.Mvc3.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Mvc3.Tests.IntegrationTests
{
    /// <summary>
    /// Summary description for LoginControllerTest
    /// </summary>
    [TestClass]
    public class LoginControllerTest
    {
        public LoginControllerTest()
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
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
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

        [Isolated]
        [TestMethod]
        public void Login_user_results_in_UserRepository_registration()
        {
            var controller = new LoginController();
            var services = Isolate.Fake.Instance<LoginServices>();
            var user = Isolate.Fake.Instance<ILoginCriteria>();
            Isolate.NonPublic.WhenCalled(typeof(LoginController), "GetLoginServices").WillReturn(services);
            Isolate.WhenCalled(() => services.Login(user)).IgnoreCall();

            controller.Login("Admin", "Admin");
            try
            {
                ObjectFactory.GetInstance<IRepositoryLinqToSql<IUser>>();

            }
            catch (Exception e)
            {
                Assert.Fail("UserRepository could not be resolved: " + e);
            }
        }
    }
}
