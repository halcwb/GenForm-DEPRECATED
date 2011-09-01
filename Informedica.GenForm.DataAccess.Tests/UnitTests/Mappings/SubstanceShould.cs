using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for SubstanceShould
    /// </summary>
    [TestClass]
    public class SubstanceShould : TestSessionContext
    {
        private TestContext testContextInstance;

        public SubstanceShould() : base(false) {}

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
        public void BeAbleToCreateASessionToPersistSubstance()
        {
            PersistSubstance(Context.CurrentSession(), CreateTestSubstance());
        }

        [TestMethod]
        public void BeAbleToCreateASubstanceDirectlyFromSessionFactory()
        {
            PersistSubstance(Context.CurrentSession(), CreateTestSubstance());
        }

        [TestMethod]
        public void BePersistedWithSubstanceGroup()
        {
                var subst = Substance.Create(new SubstanceDto
                {
                    SubstanceGroupName = "analgetica",
                    Name = "paracetamol"
                });

                Assert.AreEqual("analgetica", subst.SubstanceGroup.Name);
                PersistSubstance(Context.CurrentSession(), subst);
        }

        [TestMethod]
        public void ThrowAnErrorWhenSubsanceGroupNameIsEmptyString()
        {
            var subst = Substance.Create(new SubstanceDto
                {
                    SubstanceGroupName = "",
                    Name = "paracetamol"
                });

            PersistSubstance(Context.CurrentSession(), subst);
        }

        private static void PersistSubstance(ISession session, ISubstance subst)
        {
            session.SaveOrUpdate(subst);
        }

        private static ISubstance CreateTestSubstance()
        {
            return Substance.Create(new SubstanceDto { Name = "paracetamol" });
        }

    }
}
