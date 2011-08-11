using System;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Assembler.Contexts;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    /// <summary>
    /// Summary description for DomainFactoryCanCreate
    /// </summary>
    [TestClass]
    public class DomainFactoryCanCreate
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
        public void Substance()
        {
            var subst = DomainFactory.Create<ISubstance, SubstanceDto>(new SubstanceDto { Id = Guid.NewGuid(), Name = "test"});

            Assert.IsNotNull(subst);
        }


        [TestMethod]
        public void UnitGroupWithAValidId()
        {
            using (GetContext())
            {
                RegisterUnitGroupRepository();
                var group = GetUnitgroup();
                Assert.IsFalse(group.Id.Equals(Guid.Empty));
            }
        }

        private UnitGroup GetUnitgroup()
        {
            var group = GetRepository().SingleOrDefault(x => x.UnitGroupName == "massa") ??
                        DomainFactory.CreateOrGetById<UnitGroup, UnitGroupDto>(
                new UnitGroupDto {AllowConversion = true, UnitGroupName = "massa"});
            return group;
        }

        [TestMethod]
        public void UnitGroupIsAvailableInRepositoryWhenCreated()
        {
            using (GetContext())
            {
                CreateOrGetUnitGroup();
            }
        }

        private UnitGroup CreateOrGetUnitGroup()
        {
            RegisterUnitGroupRepository();
            var group = GetUnitgroup();
            AssertUnitGroupInRepository(group);
            return group;
        }

        private static void RegisterUnitGroupRepository()
        {
            ObjectFactory.Inject((IRepository<UnitGroup, Guid, UnitGroupDto>)
                                 new UnitGroupRepository(GenFormApplication.SessionFactory));
        }

        private void AssertUnitGroupInRepository(UnitGroup group1)
        {
            var group2 =
                GetRepository().Single(
                    x => x.UnitGroupName == group1.UnitGroupName);
            Assert.AreEqual(group1.Id, group2.Id);
        }

        private UnitGroupRepository GetRepository()
        {
            return (UnitGroupRepository) ObjectFactory.GetInstance<IRepository<UnitGroup, Guid, UnitGroupDto>>();
        }

        [TestMethod]
        public void UnitGroupButIsUnavailableInRepositoryOutsideContextBounds()
        {
            UnitGroup group;
            using (GetContext())
            {
                group = CreateOrGetUnitGroup();
            }
            try
            {
                AssertUnitGroupInRepository(group);
                Assert.Fail("Outside scope of Context repository cannot retrieve unitgroup");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        private IDisposable GetContext()
        {
            return ObjectFactory.GetInstance<SessionContext>();
        }

    }
}
