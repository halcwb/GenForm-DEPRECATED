using System;
using Informedica.GenForm.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;
using Informedica.GenForm.DataAccess.Repositories;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Repository
{
    /// <summary>
    /// Summary description for RepositoryShould
    /// </summary>
    [TestClass]
    public class RepositoryShould
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
        public void CallDeleteWithContextAndSelectorWhenDeleteByName()
        {
            var fakeContext = Isolate.Fake.Instance<GenFormDataContext>();
            Func<Test,Boolean> fakeSelector = (x => x.name == "Test");
            var repos = new Repository<ITest, Test>(fakeContext);

            Isolate.WhenCalled(() => repos.Delete(fakeContext, fakeSelector)).WithExactArguments().IgnoreCall();
            Isolate.NonPublic.WhenCalled(typeof(Repository<ITest,Test>), "GetNameSelector").WillReturn(fakeSelector);

            repos.Delete("Test");
            Isolate.Verify.WasCalledWithAnyArguments(() => repos.Delete(fakeContext, fakeSelector));
        }
    }

    public class Test
    {
        public string name { get; set; }
    }

    public interface ITest
    {
    }
}
