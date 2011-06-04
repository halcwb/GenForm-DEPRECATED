using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.ServiceProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Library.Tests
{
    
    
    /// <summary>
    ///This is a test class for UserTest and is intended
    ///to contain all UserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserTest
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
        public void GetUser_by_name_Admin_returns_AdminUser()
        {
            const string name = "Admin";

            ArrangeFakeRepository(name);

            Assert.IsTrue(User.GetUser(name).FirstOrDefault().Name == name);
        }

        private static void ArrangeFakeRepository(string name)
        {
            var repos = Isolate.Fake.Instance<IRepository<IUser>>();
            DalServiceProvider.Instance.RegisterInstanceOfType(repos);
            var user = new List<IUser>() {Isolate.Fake.Instance<IUser>()};
            Isolate.WhenCalled(() => user.FirstOrDefault().Name).WillReturn(name);
            Isolate.WhenCalled(() => repos.Fetch(name)).WillReturn(user);
        }
    }
}
