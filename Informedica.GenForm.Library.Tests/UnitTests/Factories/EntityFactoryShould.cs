using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.TestFixtures.Fixtures;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Library.Tests.UnitTests.Factories
{
    /// <summary>
    /// Summary description for EntityFactoryShould
    /// </summary>
    [TestClass]
    public class EntityFactoryShould : TestSessionContext
    {
        public EntityFactoryShould() : base(false)
        {
            //
            // TODO: Add constructor logic here
            //
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
        public void FirstTryToFindAnEntityInTheRepository()
        {
            var factory = new PackageFactory(PackageTestFixtures.GetDtoWithTwoShapes());
            var package = Isolate.Fake.Instance<Package>();
            Isolate.NonPublic.WhenCalled(factory, "Find").WillReturn(package);
            factory.Get();

            try
            {
                Isolate.Verify.NonPublic.WasCalled(factory, "Find");
            }
            catch (Exception e)
            {
                if(e.GetType() != typeof(VerifyException)) throw;
                Assert.Fail();
            }
        }

        [TestMethod]
        public void NotCreateEntityIfFound()
        {
            var factory = new PackageFactory(PackageTestFixtures.GetDtoWithTwoShapes());
            var package = Isolate.Fake.Instance<Package>();
            Isolate.NonPublic.WhenCalled(factory, "Find").WillReturn(package);
            factory.Get();

            try
            {
                Isolate.Verify.NonPublic.WasCalled(factory, "Create");
                Assert.Fail();
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(VerifyException)) throw;
            }
            
        }

        [TestMethod]
        public void NotAddFoundEntityToRepository()
        {
            var factory = new PackageFactory(PackageTestFixtures.GetDtoWithTwoShapes());
            var package = Isolate.Fake.Instance<Package>();
            var repos = Isolate.Fake.Instance<IRepository<Package>>();

            Isolate.NonPublic.WhenCalled(factory, "Find").WillReturn(package);
            Isolate.WhenCalled(() => repos.Add(package)).IgnoreCall();
            factory.Get();

            Isolate.Verify.WasNotCalled(() => repos.Add(package));
        }
    }
}
