using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.TestFixtures.Fixtures;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Repository
{
    /// <summary>
    /// Summary description for UserRepositoryTests
    /// </summary>
    [TestClass]
    public class UserRepositoryTests : TestSessionContext
    {
        public UserRepositoryTests() : base(true)
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
        public void ThatAUserCanBeAdded()
        {
            var repos = GetRepository();
            var count = repos.Count;
            repos.Add(CreateUser());
            Assert.AreEqual(count + 1, repos.Count);
        }

        [TestMethod]
        public void ThatUserCanBeRemoved()
        {
            var repos = GetRepository();
            var count = repos.Count;
            var user = CreateUser();
            repos.Add(user);
            repos.Remove(user);
            Assert.AreEqual(count, repos.Count);
        }

        [TestMethod]
        public void ThatUserCanBeRetrievedById()
        {
            var repos = GetRepository();
            var user = CreateUser();
            repos.Add(user);
            var id = user.Id;

            user = repos.GetById(id);
            Assert.AreEqual(id, user.Id);
        }


        [TestMethod]
        public void ThatUserCanBeRetrievedByName()
        {
            var repos = GetRepository();
            var user = CreateUser();
            repos.Add(user);
            var name = user.Name;

            user = repos.GetByName(name);
            Assert.AreEqual(name, user.Name);
        }


        [TestMethod]
        public void ThatUserCanBeRetrievedByNameIsCapitalSensitive()
        {
            var repos = GetRepository();
            var user = CreateUser();
            user.Name = "foobar";
            repos.Add(user);
            const string name = "FooBar";

            var user2 = repos.GetByName(name.ToLower());
            Assert.AreEqual(name.ToLower(), user2.Name.ToLower());
        }

        private static User CreateUser()
        {
            return UserTestFixtures.CreateFooBarUser();
        }
        private static UserRepository GetRepository()
        {
            var repos = ObjectFactory.GetInstance<UserRepository>();
            return repos;
        }

    }
}
