using System;
using System.Linq;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.DataAccess.Tests.TestBase;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services.Products.dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Brand = Informedica.GenForm.Database.Brand;
using Package = Informedica.GenForm.Database.Package;
using Product = Informedica.GenForm.Database.Product;
using Shape = Informedica.GenForm.Database.Shape;
using Substance = Informedica.GenForm.Database.Substance;
using Unit = Informedica.GenForm.Database.Unit;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests
{
    /// <summary>
    /// Summary description for ProductMapperShould
    /// </summary>
    [TestClass]
    public class ProductMapperShould: DataMapperTestBase<ProductMapper,IProduct,Product>
    {
        private const string ProductName = "dopamine (Dynatra) 200 mg in 5 mL infusievloeistof per ampul";
        private const string DisplayName = "dopamine (Dynatra) 200 mg in 5 mL infusievloeistof per ampul";
        private const string Generic = "dopamine";
        private const string Shape = "infusievloeistof";
        private const string Package = "ampul";
        private const Decimal ProductQuantity = 5;
        private const Decimal SubstanceQuantity = 5;
        private const string ProductUnit = "mL";
        private const string Code = "1";
        private const string Brand = "Dynatra";
        private const Int32 SortOrder = 1;
        private const String Substance = "dopamine";
        private const String SubstanceUnit = "mg";

        private TestContext _testContextInstance;
        private const string ProductCode = "1";

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
            Mapper.MapFromDaoToBo(Dao, (IMappable<IProduct>)Bo);
            AssertIsMapped();
        }

        [TestMethod]
        public void MapProductWithSubstancesToDao()
        {
            FillProductWithSubstances();
            Mapper.MapFromBoToDao(Bo, Dao);
            AssertIsMapped();
        }

        private void FillProductWithSubstances()
        {
            FillProduct();
            FillProductSubstance(Bo.AddSubstance());
        }

        private static void FillProductSubstance(IProductSubstance substance)
        {
            substance.Quantity = SubstanceQuantity;
            substance.SortOrder = SortOrder;
            substance.Substance = Substance;
            substance.Unit = SubstanceUnit;
            return;
        }

        private void FillDao()
        {
            Dao.Brand = new Brand {BrandName = Brand};
            Dao.DisplayName = DisplayName;
            Dao.Package = new Package {PackageName = Package};
            Dao.ProductCode = Code;
            Dao.ProductName = ProductName;
            Dao.ProductQuantity = ProductQuantity;
            Dao.Shape = new Shape {ShapeName = Shape};
            Dao.Substance = new Substance {SubstanceName = Generic, IsGeneric = true};
            Dao.Unit = new Unit {UnitName = ProductUnit};
        }

        private void FillProduct()
        {
            var dto = new ProductDto
                          {
                              ProductCode = ProductCode,
                              ProductName = ProductName,
                              DisplayName = DisplayName,
                              Generic = Generic,
                              Shape = Shape,
                              Unit = ProductUnit,
                              Package = Package,
                              Quantity = ProductQuantity,
                              Brand = Brand,
                              Id = 1
                          };
            Bo = GetBoWithDto(dto);
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
