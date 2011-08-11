using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Assembler.Contexts;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Repository
{
    /// <summary>
    /// Summary description for UnitGroupRepositoryTests
    /// </summary>
    [TestClass]
    public class UnitGroupRepositoryTests
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

        [TestMethod]
        public void ThatTheSameUnitGroupCannotBeAddedTwice()
        {
            var group1 = new UnitGroup(new UnitGroupDto
            {
                AllowConversion = true,
                UnitGroupName = "massa"
            });
            var group2 = new UnitGroup(new UnitGroupDto
            {
                AllowConversion = true,
                UnitGroupName = "massa"
            });
            using (GetContext())
            {
                try
                {
                    new UnitGroupRepository(GenFormApplication.SessionFactory) { group1, group2 };
                    Assert.Fail("repository should not accept same item twice");
                }
                catch (Exception)
                {
                    Assert.IsTrue(true);
                }
            }
        }



        private IDisposable GetContext()
        {
            return ObjectFactory.GetInstance<SessionContext>();
        }
    }
}
