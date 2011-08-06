using System;
using Informedica.GenForm.DataAccess.Tests.TestBase;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;
using Substance = Informedica.GenForm.Database.Substance;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Repository
{
    /// <summary>
    /// Summary description for SubstanceRepositoryShould
    /// </summary>
    [TestClass]
    public class SubstanceRepositoryShould: RepositoryTestBase<IRepository<ISubstance>, ISubstance,Substance>
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
        public void CallSubstanceMappertoMapSubstanceToDao()
        {
            try
            {
                using (Repos.Rollback)
                {
                    Repos.Insert(Bo);

                } 
                Isolate.Verify.WasCalledWithAnyArguments(() => Mapper.MapFromBoToDao(Bo, Dao));
            }
            catch (Exception e)
            {
                AssertVerify(e, "substane repository did not call substance mapper to map dao to substance");
            }

        }

        [TestMethod]
        public void CallSubmitChangesOnContext()
        {
            try
            {
                Bo.SubstanceName = "dopamine";
                using (Repos.Rollback)
                {
                    Repos.Insert(Bo);
                } Isolate.Verify.WasCalledWithAnyArguments(() => Context.SubmitChanges());
            }
            catch (Exception e)
            {
                AssertVerify(e, "Substance repository did not submit changes");
                throw;
            }
        }
    }
}
