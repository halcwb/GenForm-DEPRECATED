using FluentNHibernate.Testing;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for UserMapShould
    /// </summary>
    [TestClass]
    public class UserMapShould : TestSessionContext
    {
        public UserMapShould() : base(false)
        {
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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
        public void MapUser()
        {
            new PersistenceSpecification<User>(Context.CurrentSession())
                .CheckProperty(b => b.Name, "Foo")
                .CheckProperty(u => u.Email, "Foo@gmail.com")
                .CheckProperty(u => u.FirstName, "Foo")
                .CheckProperty(u => u.LastName, "Bar")
                .CheckProperty(u => u.Password, "secret")
                .VerifyTheMappings();
        }
    }
}
