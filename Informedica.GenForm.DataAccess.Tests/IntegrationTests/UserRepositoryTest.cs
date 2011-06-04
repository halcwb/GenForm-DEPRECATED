using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.DataAccess.DataContexts;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.DataAccess.Tests.IntegrationTests
{
    /// <summary>
    /// Summary description for UserRepositoryTest
    /// </summary>
    [TestClass]
    public class UserRepositoryTest
    {
        public UserRepositoryTest()
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
        public void GetUserByName_with_system_admin_name_returns_AdminUser()
        {
            var repository = new UserRepository();
            IEnumerable<GenFormUser> users = CreateListWithAdminUser();
            IsolateRepositoryFromDataContext(users);

            var list = repository.Fetch("Admin");

            Assert.IsTrue(list.Count() > 0, "A user with name Admin should exist in the database");
            Assert.IsTrue(list.FirstOrDefault().Name == "Admin", "The User should have name Admin");
        }

        private IEnumerable<GenFormUser> CreateListWithAdminUser()
        {
            var adminUser = new GenFormUser {UserName = "Admin"};
            return new List<GenFormUser> {adminUser};
        }

        private void IsolateRepositoryFromDataContext(IEnumerable<GenFormUser> users)
        {
            var context = Isolate.Fake.Instance<GenFormDataContext>();
            Isolate.NonPublic.WhenCalled(typeof(UserRepository), "FindUsersByName").WillReturn(users);
            Isolate.WhenCalled(() => DataContextFactory.CreateGenFormDataContext()).WillReturn(context);
        }

        [Isolated]
        [TestMethod]
        public void GetUserByName_with_name_Foo_should_return_emptyList()
        {
            var repository = new UserRepository();
            IsolateRepositoryFromDataContext(new List<GenFormUser>());
            var list = repository.Fetch("foo");

            Assert.IsTrue(list.Count() == 0, "A user with name foo should not exist in the database");

        }
    }
}
