using System;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    /// <summary>
    /// Summary description for NewShapeShould
    /// </summary>
    [TestClass]
    public class NewShapeShould
    {   
        private TestContext testContextInstance;
        private Shape _newShape;

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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            GenFormApplication.Initialize();

        }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void BeAbleToAssociateWithRoute()
        {
            var route = AssociateShapeWithRoute(CreateRoute());
            Assert.AreEqual(route, _newShape.Routes.First());
        }

        private Route AssociateShapeWithRoute(Route route)
        {
            _newShape = Shape.Create(new ShapeDto { Name = "infusievloeistof" });
            _newShape.AddRoute(route);
            return route;
        }

        private static Route CreateRoute()
        {
            return Route.Create(new RouteDto{ Abbreviation = "iv", Name = "intraveneus"});
        }

        [TestMethod]
        public void NotAcceptTheSameRouteTwice()
        {
            var route = AssociateShapeWithRoute(CreateRoute());
            try
            {
                AssociateShapeWithRoute(route);
            }
            catch ( Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(CannotAddItemException<Shape>));
            }
            Assert.IsFalse(_newShape.Routes.Count() == 2);
        }

        [TestMethod]
        public void NotAcceptDifferentRouteObjectsWithSameDataTwice()
        {
            AssociateShapeWithRoute(CreateRoute());
            try
            {
                AssociateShapeWithRoute(CreateRoute());

            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(CannotAddItemException<Route>));
            }
            Assert.IsFalse(_newShape.Routes.Count() == 2);
        }

        [TestMethod]
        public void BeAbleToAssociateWithPackage()
        {
            var package = AssociateShapeWithPackage(CreatePackage());
            Assert.IsTrue(_newShape.Packages.First().Name == package.Name);
        }

        private Package AssociateShapeWithPackage(Package package)
        {
            _newShape = Shape.Create(new ShapeDto { Name = "infusievloeistof" });
            _newShape.AddPackage(package);
            return package;
        }

        private Package CreatePackage()
        {
            return Package.Create(new PackageDto
                                   {
                                       Abbreviation = "ampul",
                                       Name = "ampul"
                                   });
        }

        [TestMethod]
        public void BeAbleToAssociateWithUnit()
        {
            var unitGroup = AssociateShapeWithUnit(CreateUnitGroup());

            Assert.IsTrue(_newShape.UnitGroups.Contains(unitGroup));
            Assert.IsTrue(_newShape.UnitGroups.Count() == 1);
        }

        [TestMethod]
        public void WillNotAddSameUnitGroupTwice()
        {
            var group1 = AssociateShapeWithUnit(CreateUnitGroup());
            try
            {
                AssociateShapeWithUnit(CreateUnitGroup());

            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(CannotAddItemException<Unit>));
            }
            Assert.IsTrue(_newShape.UnitGroups.Count() == 1);
            Assert.IsTrue(_newShape.UnitGroups.Contains(group1, new UnitGroupComparer()));
        }

        private UnitGroup AssociateShapeWithUnit(UnitGroup unitGroup)
        {
            _newShape = Shape.Create(new ShapeDto { Name = "infusievloeistof" });
            _newShape.AddUnitGroup(unitGroup);
            return unitGroup;
        }

        private static UnitGroup CreateUnitGroup()
        {
            return UnitGroup.Create(GetUnitGroupDto());
        }

        private static UnitGroupDto GetUnitGroupDto()
        {
            return new UnitGroupDto
                       {
                           AllowConversion = true,
                           Name = "massa"
                       };
        }

        [TestMethod]
        public void HaveUnitWithShapeAssociatedWithThatUnit()
        {
            var unit = AssociateShapeWithUnit(CreateUnitGroup());
            Assert.IsTrue(unit.Shapes.Contains(_newShape, new ShapeComparer()));
        }
    }
}
