using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.DataAccess.Tests.TestBase;
using Informedica.GenForm.Library.DomainModel.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Substance = Informedica.GenForm.Database.Substance;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mapper
{
    /// <summary>
    /// Summary description for GenericMapperShould
    /// </summary>
    [TestClass]
    public class GenericMapperShould: DataMapperTestBase<GenericMapper,IGeneric,Substance>
    {
        private const string GenericName = "dopamine";
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
        public void MapGenericToDao()
        {
            Bo.GenericName = GenericName;
            Mapper.MapFromBoToDao(Bo, Dao);
            AssertIsMapped();
        }

        [TestMethod]
        public void MapDaoToGeneric()
        {
            Dao.SubstanceName = GenericName;
            Dao.IsGeneric = true;
            Mapper.MapFromDaoToBo(Dao, Bo);
            AssertIsMapped();
        }

        #region Overrides of DataMapperTestBase<GenericMapper,IGeneric,Substance>

        protected override bool IsMapped(IGeneric bo, Substance dao)
        {
            return bo.GenericName == dao.SubstanceName;
        }

        #endregion
    }
}
