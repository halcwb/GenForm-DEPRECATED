using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.TestFixtures.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    /// <summary>
    /// Summary description for ProductConstructionTests
    /// </summary>
    [TestClass]
    public class ProductConstructionTests
    {
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
        public void ThatProductCanBeCreatedUsingFluentConstructor()
        {
            var product = Product.Create(ProductTestFixtures.GetProductDtoWithNoSubstances())
                .Shape(ShapeTestFixtures.CreateIvFluidShape())
                .Package(PackageTestFixtures.CreatePackageAmpul())
                .Quantity(UnitTestFixtures.CreateUnitMililiter(), 5M)
                .Substance(1, SubstanceTestFixtures.CreateSubstanceWithoutGroup(), 200,
                           UnitTestFixtures.CreateUnitMilligram())
                .Route(RouteTestFixtures.CreateRouteIv());
            
            Assert.IsInstanceOfType(product, typeof(Product));
        }

        [TestMethod]
        public void ThatProductCreateFailsWhenNoDisplayName()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            dto.DisplayName = string.Empty;
            var shape = ShapeTestFixtures.CreateIvFluidShape();
            var package = PackageTestFixtures.CreatePackageAmpul();
            const decimal prodQuantity = 5M;
            var unit = UnitTestFixtures.CreateUnitMililiter();
            var subst = SubstanceTestFixtures.CreateSubstanceWithoutGroup();
            const int order = 1;
            const decimal quantity = 200;
            var substUnit = UnitTestFixtures.CreateUnitMilligram();
            var route = RouteTestFixtures.CreateRouteIv();

            AssertCreateFails(quantity, subst, substUnit, route, order, shape, dto, package, prodQuantity, unit);            
        }

        [TestMethod]
        public void ThatProductCreateFailsWhenNoGenericName()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            dto.GenericName = string.Empty;
            var shape = ShapeTestFixtures.CreateIvFluidShape();
            var package = PackageTestFixtures.CreatePackageAmpul();
            const decimal prodQuantity = 5M;
            var unit = UnitTestFixtures.CreateUnitMililiter();
            var subst = SubstanceTestFixtures.CreateSubstanceWithoutGroup();
            const int order = 1;
            const decimal quantity = 200;
            var substUnit = UnitTestFixtures.CreateUnitMilligram();
            var route = RouteTestFixtures.CreateRouteIv();

            AssertCreateFails(quantity, subst, substUnit, route, order, shape, dto, package, prodQuantity, unit);
        }

        [TestMethod]
        public void ThatProductCreateFailsWhenNoShape()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            var package = PackageTestFixtures.CreatePackageAmpul();
            const decimal prodQuantity = 5M;
            var unit = UnitTestFixtures.CreateUnitMililiter();
            var subst = SubstanceTestFixtures.CreateSubstanceWithoutGroup();
            const int order = 1;
            const decimal quantity = 200;
            var substUnit = UnitTestFixtures.CreateUnitMilligram();
            var route = RouteTestFixtures.CreateRouteIv();

            AssertCreateFails(quantity, subst, substUnit, route, order, null, dto, package, prodQuantity, unit);
        }

        [TestMethod]
        public void ThatProductCreateFailsWhenNoPackage()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            var shape = ShapeTestFixtures.CreateIvFluidShape();
            const decimal prodQuantity = 5M;
            var unit = UnitTestFixtures.CreateUnitMililiter();
            var subst = SubstanceTestFixtures.CreateSubstanceWithoutGroup();
            const int order = 1;
            const int quantity = 200;
            var substUnit = UnitTestFixtures.CreateUnitMilligram();
            var route = RouteTestFixtures.CreateRouteIv();

            AssertCreateFails(quantity, subst, substUnit, route, order, shape, dto, null, prodQuantity, unit);
        }

        [TestMethod]
        public void ThatProductCreateFailsWhenNoProductQuantity()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            var shape = ShapeTestFixtures.CreateIvFluidShape();
            var package = PackageTestFixtures.CreatePackageAmpul();
            const decimal prodQuantity = 0;
            var unit = UnitTestFixtures.CreateUnitMililiter();
            var subst = SubstanceTestFixtures.CreateSubstanceWithoutGroup();
            const int order = 1;
            const decimal quantity = 200;
            var substUnit = UnitTestFixtures.CreateUnitMilligram();
            var route = RouteTestFixtures.CreateRouteIv();

            AssertCreateFails(quantity, subst, substUnit, route, order, shape, dto, package, prodQuantity, unit);
        }

        [TestMethod]
        public void ThatProductCreateFailsWhenNoProductUnit()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            var shape = ShapeTestFixtures.CreateIvFluidShape();
            var package = PackageTestFixtures.CreatePackageAmpul();
            const decimal prodQuantity = 5M;
            var subst = SubstanceTestFixtures.CreateSubstanceWithoutGroup();
            const int order = 1;
            const decimal quantity = 200;
            var substUnit = UnitTestFixtures.CreateUnitMilligram();
            var route = RouteTestFixtures.CreateRouteIv();

            AssertCreateFails(quantity, subst, substUnit, route, order, shape, dto, package, prodQuantity, null);
        }

        [TestMethod]
        public void ThatProductCreateFailsWhenNoSubstance()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            var shape = ShapeTestFixtures.CreateIvFluidShape();
            var package = PackageTestFixtures.CreatePackageAmpul();
            const decimal prodQuantity = 5M;
            var unit = UnitTestFixtures.CreateUnitMililiter();
            const int order = 1;
            const decimal quantity = 200;
            var substUnit = UnitTestFixtures.CreateUnitMilligram();
            var route = RouteTestFixtures.CreateRouteIv();

            AssertCreateFails(quantity, null, substUnit, route, order, shape, dto, package, prodQuantity, unit);
        }

        [TestMethod]
        public void ThatProductCreateFailsWhenNoSortOrder()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            var shape = ShapeTestFixtures.CreateIvFluidShape();
            var package = PackageTestFixtures.CreatePackageAmpul();
            const decimal prodQuantity = 5M;
            var unit = UnitTestFixtures.CreateUnitMililiter();
            var subst = SubstanceTestFixtures.CreateSubstanceWithoutGroup();
            const int order = 0;
            const decimal quantity = 200;
            var substUnit = UnitTestFixtures.CreateUnitMilligram();
            var route = RouteTestFixtures.CreateRouteIv();

            AssertCreateFails(quantity, subst, substUnit, route, order, shape, dto, package, prodQuantity, unit);
        }

        [TestMethod]
        public void ThatProductCreateFailsWhenSubstanceHasNoQuantity()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            var shape = ShapeTestFixtures.CreateIvFluidShape();
            var package = PackageTestFixtures.CreatePackageAmpul();
            const decimal prodQuantity = 5M;
            var unit = UnitTestFixtures.CreateUnitMililiter();
            var subst = SubstanceTestFixtures.CreateSubstanceWithoutGroup();
            const int order = 1;
            const decimal quantity = 0;
            var substUnit = UnitTestFixtures.CreateUnitMilligram();
            var route = RouteTestFixtures.CreateRouteIv();

            AssertCreateFails(quantity, subst, substUnit, route, order, shape, dto, package, prodQuantity, unit);
        }

        [TestMethod]
        public void ThatProductCreateFailsWhenSubstanceHasNoSubstanceUnit()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            var shape = ShapeTestFixtures.CreateIvFluidShape();
            var package = PackageTestFixtures.CreatePackageAmpul();
            const decimal prodQuantity = 5M;
            var unit = UnitTestFixtures.CreateUnitMililiter();
            var subst = SubstanceTestFixtures.CreateSubstanceWithoutGroup();
            const int order = 1;
            const decimal quantity = 0;
            var substUnit = UnitTestFixtures.CreateUnitMilligram();
            var route = RouteTestFixtures.CreateRouteIv();

            AssertCreateFails(quantity, subst, substUnit, route, order, shape, dto, package, prodQuantity, unit);
        }

        [TestMethod]
        public void ThatProductCreateFailsWhenSubstanceHasNoUnit()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            var shape = ShapeTestFixtures.CreateIvFluidShape();
            var package = PackageTestFixtures.CreatePackageAmpul();
            const decimal prodQuantity = 5M;
            var unit = UnitTestFixtures.CreateUnitMililiter();
            var subst = SubstanceTestFixtures.CreateSubstanceWithoutGroup();
            const int order = 1;
            const decimal quantity = 200;
            var route = RouteTestFixtures.CreateRouteIv();

            AssertCreateFails(quantity, subst, null, route, order, shape, dto, package, prodQuantity, unit);
        }

        [TestMethod]
        public void ThatProductCreateFailsWhenProductHasNoRoute()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            var shape = ShapeTestFixtures.CreateIvFluidShape();
            var package = PackageTestFixtures.CreatePackageAmpul();
            const decimal prodQuantity = 5M;
            var unit = UnitTestFixtures.CreateUnitMililiter();
            var subst = SubstanceTestFixtures.CreateSubstanceWithoutGroup();
            const int order = 1;
            const decimal quantity = 200;
            var substUnit = UnitTestFixtures.CreateUnitMilligram();

            AssertCreateFails(quantity, subst, substUnit, null, order, shape, dto, package, prodQuantity, unit);
        }

        private static void AssertCreateFails(decimal quantity, Substance subst, Unit substUnit, Route route, int order,
                                              Shape shape, ProductDto dto, Package package, decimal prodQuantity, Unit unit)
        {
            try
            {
                Product.Create(dto)
                    .Shape(shape)
                    .Package(package)
                    .Quantity(unit, prodQuantity)
                    .Substance(order, subst, quantity, substUnit)
                    .Route(route);

                Assert.Fail();
            }
            catch (System.Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof (AssertFailedException));
            }
        }
    }
}
