using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.Tests;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Services
{
    /// <summary>
    /// Summary description for ShapeServicesTests
    /// </summary>
    [TestClass]
    public class ShapeServicesTests : TestSessionContext
    {
        private TestContext testContextInstance;

        public ShapeServicesTests() : base(true) {}

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
        public static void MyClassInitialize(TestContext testContext) { GenFormApplication.Initialize();}
        
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
        public void ThatShapeCanBeGet()
        {
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetIvFluidDto()).Get();
            Assert.IsNotNull(shape);
        }

        [TestMethod]
        public void ThatAddedShapeCanBeFound()
        {
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetIvFluidDto()).Get();
            Assert.AreEqual(shape, ShapeServices.Shapes.Single(
                x => x.Name == shape.Name));
        }

        [TestMethod]
        public void ThatShapeCanBeDeleted()
        {
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetIvFluidDto()).Get();
            ShapeServices.Delete(shape);
            Assert.IsNull(ShapeServices.Shapes.SingleOrDefault(x => x.Name == ShapeTestFixtures.GetIvFluidDto().Name));
        }

        [TestMethod]
        public void ThatShapeCanBeUpdated()
        {
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetIvFluidDto()).Get();
            // ToDo: rewrite
            // shape.Name = shape.Name + "_changed";
            Assert.IsNotNull(ShapeServices.Shapes.SingleOrDefault(x => x.Name == shape.Name));
        }

        [TestMethod]
        public void ThatShapeIsAssociatedWithPackage()
        {
            var package = PackageServices.WithDto(GetPackageDto()).Get();
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetValidDtoWithPackages()).Get();
            Assert.AreEqual(shape.PackageSet.Single(p => p.Name == package.Name), package);

        }

        [TestMethod]
        public void ThatShapePackageRelationshipIsBiDirectional()
        {
            var package = PackageServices.WithDto(GetPackageDto()).Get();
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetValidDtoWithPackages()).Get();
            Assert.AreEqual(shape, package.ShapeSet.First());
        }

        [TestMethod]
        public void ThatShapeWithPackageCanBeDeleted()
        {
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetValidDtoWithPackages()).Get();
            ShapeServices.Delete(shape);
            Assert.IsNull(ShapeServices.Shapes.SingleOrDefault(x => x.Name == ShapeTestFixtures.GetValidDtoWithPackages().Name));
        }

        [TestMethod]
        public void ThatShapeWithAfterDeleteLeavesPackage()
        {
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetValidDtoWithPackages()).Get();
            ShapeServices.Delete(shape);
            Assert.IsNotNull(PackageServices.Packages.SingleOrDefault(x => x.Name == ShapeTestFixtures.GetValidDtoWithPackages().Packages.First().Name));
        }

        [TestMethod]
        public void ThatShapeIsAssociatedWithRoute()
        {
            var route = RouteServices.WithDto(GetRouteDto()).Get();
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetValidDtoWithRoutes()).Get();

            var list = new List<IRoute>(shape.Routes);
            Assert.AreEqual(list.Single(p => p.Name == route.Name), route);
        }

        private RouteDto GetRouteDto()
        {
            return ShapeTestFixtures.GetValidDtoWithRoutes().Routes.First();
        }

        [TestMethod]
        public void ThatShapeRouteRelationshipIsBiDirectional()
        {
            var route = RouteServices.WithDto(GetRouteDto()).Get();
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetValidDtoWithRoutes()).Get();
            Assert.AreEqual(shape, route.ShapeSet.First());
        }

        [TestMethod]
        public void ThatShapeWithRoutesCanBeDeletedLeavingRoutes()
        {
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetValidDtoWithRoutes()).Get();
            ShapeServices.Delete(shape);
            Assert.IsNotNull(RouteServices.Routes.SingleOrDefault(x => x.Name == ShapeTestFixtures.GetValidDtoWithRoutes().Routes.First().Name));
        }

        [TestMethod]
        public void ThatShapeIsAssociatedWithUnitGroup()
        {
            var unitGroup = UnitGroupServices.WithDto(GetUnitGroupDto()).Get();
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetValidDtoWithUnitGroups()).Get();
            Assert.AreEqual(shape.UnitGroupSet.Single(p => p.Name == unitGroup.Name), unitGroup);
        }

        [TestMethod]
        public void ThatShapeWithUnitGroupsCanBeDeletedLeavingUnitGroups()
        {
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetValidDtoWithUnitGroups()).Get();
            ShapeServices.Delete(shape);
            Assert.IsNotNull(UnitGroupServices.UnitGroups.SingleOrDefault(x => x.Name == ShapeTestFixtures.GetValidDtoWithUnitGroups().UnitGroups.First().Name));
        }

        private UnitGroupDto GetUnitGroupDto()
        {
            return ShapeTestFixtures.GetValidDtoWithUnitGroups().UnitGroups.First();
        }

        [TestMethod]
        public void ThatShapeUnitGroupRelationshipIsBiDirectional()
        {
            var unitGroup = UnitGroupServices.WithDto(GetUnitGroupDto()).Get();
            var shape = ShapeServices.WithDto(ShapeTestFixtures.GetValidDtoWithUnitGroups()).Get();
            Assert.AreEqual(shape, unitGroup.Shapes.First());
        }

        private PackageDto GetPackageDto()
        {
            return ShapeTestFixtures.GetValidDtoWithPackages().Packages.First();
        }
    }
}
