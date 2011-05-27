using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.DataAccess.DataContexts;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Informedica.GenForm.Library.DomainModel.Users;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for UserRepositoryTest and is intended
    ///to contain all UserRepositoryTest Unit Tests
    ///</summary>
    [TestClass]
    public class UserRepositoryTest
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
        public void Get_User_by_system_username_returns_instance_of_IUser_with_that_name()
        {
            var name = "Test";
            IsolateRepositoryFromDataContext(CreateUserListWithName(name));

            var result = CreateUserRepository().GetByName(name).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(IUser), "The correct type was not returned");
            Assert.IsTrue(result.Name == name, "The returned user should have name Admin");
        }

        private IEnumerable<GenFormUser> CreateUserListWithName(String name)
        {
            var adminUser = new GenFormUser { UserName = name };
            return new List<GenFormUser> { adminUser };
        }

        private void IsolateRepositoryFromDataContext(IEnumerable<GenFormUser> users)
        {
            var context = Isolate.Fake.Instance<GenFormDataContext>();
            Isolate.NonPublic.WhenCalled(typeof(UserRepository), "FindUsersByName").WillReturn(users);
            Isolate.WhenCalled(() => DataContextFactory.CreateGenFormDataContext()).WillReturn(context);
        }


        [Isolated]
        [TestMethod]
        public void Get_UserByName_calls_Mapper_MapFromDaoToBo()
        {
            var repository = CreateUserRepository();
            IsolateRepositoryFromDataContext(CreateUserListWithName(String.Empty));

            try
            {
                repository.GetByName(String.Empty);
            }
            catch (VerifyException e)
            {
                Assert.Fail("MapFromDaoToBo was not called on Mapper");
            }
        }

        private static UserRepository CreateUserRepository()
        {
            return new UserRepository();
        }
    }
}
