using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for SubstanceShould
    /// </summary>
    [TestClass]
    public class SubstanceShould
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
            using (var session = GenFormApplication.Instance.SessionFactoryFromInstance.OpenSession())
            {
                PersistSubstance(session, CreateTestSubstance());
            }
        }

        [TestMethod]
        public void BeAbleToCreateASubstanceDirectlyFromSessionFactory()
        {
            using (var session = SessionFactoryCreator.CreateSessionFactory().OpenSession())
            {
                PersistSubstance(session, CreateTestSubstance());
            }
        }

        [TestMethod]
        public void BePersistedWithSubstanceGroup()
        {

            using (var session = GenFormApplication.Instance.SessionFactoryFromInstance.OpenSession())
            {
                var subst = DomainFactory.Create<ISubstance, SubstanceDto>(new SubstanceDto
                {
                    SubstanceGroupName = "analgetica",
                    Name = "paracetamol"
                });

                PersistSubstance(session, subst);
            }
        }

        [TestMethod]
        public void ThrowAnErrorWhenSubsanceGroupNameIsEmptyString()
        {
            var subst = DomainFactory.Create<ISubstance, SubstanceDto>(new SubstanceDto
                                                                           {
                                                                               SubstanceGroupName = "",
                                                                               Name = "paracetamol"
                                                                           });

            using (var session = GenFormApplication.Instance.SessionFactoryFromInstance.OpenSession())
            {
                PersistSubstance(session, subst);
            }
        }

        private static void PersistSubstance(ISession session, ISubstance subst)
        {
            using (var trans = session.BeginTransaction())
            {
                session.SaveOrUpdate(subst);

                trans.Rollback();
            }
        }

        private static ISubstance CreateTestSubstance()
        {
            return DomainFactory.Create<ISubstance, SubstanceDto>(new SubstanceDto { Name = "paracetamol" });
        }

    }
}
