using System.Data;
using System.Linq;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.DataAccess.Tests.TestBase;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Brand = Informedica.GenForm.Database.Brand;
using Package = Informedica.GenForm.Database.Package;
using Product = Informedica.GenForm.Database.Product;
using Shape = Informedica.GenForm.Database.Shape;
using Substance = Informedica.GenForm.Database.Substance;
using Unit = Informedica.GenForm.Database.Unit;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mapper
{
    /// <summary>
    /// Summary description for ProductMapperShould
    /// </summary>
    [TestClass]
    public class ProductMapperShould: DataMapperTestBase<ProductMapper,IProduct,Product>
    {

        private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
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
        public void MapProductToDao()
        {
            FillProduct();
            Mapper.MapFromBoToDao(Bo, Dao);
            AssertIsMapped();
        }

        [TestMethod]
        public void MapDaoToProduct()
        {
            FillDao();
            Mapper.MapFromDaoToBo(Dao, Bo);
            AssertIsMapped();
        }

        [TestMethod]
        public void MapProductWithOneSubstanceToDao()
        {
            FillProductWithOneSubstance();
            Mapper.MapFromBoToDao(Bo, Dao);
            AssertIsMapped();
        }

        [TestMethod]
        public void ReferenceSameDaoSubstanceForGenericAndProductSubstance()
        {
            FillProductWithOneSubstance();
            Mapper.MapFromBoToDao(Bo, Dao);
            Assert.AreSame(Dao.Substance, Dao.ProductSubstance.First().Substance, "substance should reference same substance");
        }

        [TestMethod]
        public void ResultInMappedDaoThatCanBeSubmittedToDataContext()
        {
            SubmitProduct();
        }

        private void SubmitProduct()
        {
            using (var ctx = new GenFormDataContext(DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GenForm)))
            {
                ctx.Connection.Open();
                ctx.Transaction = ctx.Connection.BeginTransaction(IsolationLevel.Serializable);

                FillProduct();
                Dao = new Product();
                Mapper.MapFromBoToDao(Bo, Dao);
                ctx.Product.InsertOnSubmit(Dao);
                try
                {
                    ctx.SubmitChanges();
                }
                catch (System.Exception e)
                {
                    Assert.Fail(e.ToString());
                }
                finally
                {
                    ctx.Transaction.Rollback();
                    ctx.Connection.Close();
                }
            }
        }

        [TestMethod]
        public void BeAbleToBeUsedInDifferentDataContexts()
        {
            SubmitProduct();   
        }

        private void FillProductWithOneSubstance()
        {
            var dto = ProductTestFixtures.GetProductDtoWithOneSubstance();

            Bo = GetBoWithDto(dto);
        }

        private void FillDao()
        {
            Dao.Brand = new Brand {BrandName = ProductTestFixtures.Brand};
            Dao.DisplayName = ProductTestFixtures.DisplayName;
            Dao.Package = new Package { PackageName = ProductTestFixtures.Package };
            Dao.ProductCode = ProductTestFixtures.ProductCode;
            Dao.ProductName = ProductTestFixtures.ProductName;
            Dao.ProductQuantity = ProductTestFixtures.ProductQuantity;
            Dao.Shape = new Shape { ShapeName = ProductTestFixtures.Shape };
            Dao.Substance = new Substance { SubstanceName = ProductTestFixtures.Generic, IsGeneric = true };
            Dao.Unit = new Unit { UnitName = ProductTestFixtures.ProductUnit };
        }

        private void FillProduct()
        {
            var dto = GetDtoWithoutSubstance();
            Bo = GetBoWithDto(dto);
        }

        private ProductDto GetDtoWithoutSubstance()
        {
            return ProductTestFixtures.GetProductDtoWithNoSubstances();
        }

        #region Overrides of DataMapperTestBase<ProductMapper,IProduct,Product>

        protected override bool IsMapped(IProduct bo, Product dao)
        {
            var isMapped = Bo.BrandName == (dao.Brand == null ? string.Empty: dao.Brand.BrandName);
            isMapped = isMapped && Bo.GenericName == dao.Substance.SubstanceName && (dao.Substance.IsGeneric ?? false);
            isMapped = isMapped && Bo.PackageName == dao.Package.PackageName;
            isMapped = isMapped && Bo.ProductCode == dao.ProductCode;
            isMapped = isMapped && Bo.ProductName == dao.ProductName;
            isMapped = isMapped && Bo.DisplayName == dao.DisplayName;
            isMapped = isMapped && Bo.Quantity == dao.ProductQuantity;
            isMapped = isMapped && Bo.ShapeName == dao.Shape.ShapeName;
            isMapped = isMapped && Bo.UnitName == dao.Unit.UnitName;

            foreach (var substance in Bo.Substances)
            {
                var substance1 = substance;
                var daoSubst =
                    dao.ProductSubstance.SingleOrDefault(s => substance1.Substance == s.Substance.SubstanceName &&
                                                              substance1.SortOrder == s.SubstanceOrdering &&
                                                              substance1.Quantity == s.SubstanceQuantity &&
                                                              substance1.Unit == s.Unit.UnitName);
                isMapped = isMapped && daoSubst != null;
            }

            return isMapped;
        }

        #endregion
    }
}
