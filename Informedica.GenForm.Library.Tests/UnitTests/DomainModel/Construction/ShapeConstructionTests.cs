using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    /// <summary>
    /// Summary description for ShapeConstructionTests
    /// </summary>
    [TestClass]
    public class ShapeConstructionTests
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
        public void AValidShapeCanBeConstucted()
        {
            var shape = Shape.Create(ShapeTestFixtures.GetValidDto());
            Assert.IsTrue(ShapeIsValid(shape));
        }

        [TestMethod]
        public void AValidShapeWithPackagesCanBeConstructed()
        {
            var shape = Shape.Create(ShapeTestFixtures.GetValidDtoWithPackages());
            Assert.IsTrue(ShapeIsValid(shape) && ShapeContainsPackages(shape));
        }

        private bool ShapeContainsPackages(Shape shape)
        {
            return shape.Packages.Count() == 2 &&
                   shape.Packages.First().Shapes.Contains(shape) &&
                   shape.Packages.Last().Shapes.Contains(shape);
        }

        [TestMethod]
        public void AValidShapeWithRoutesCanBeConstructed()
        {
            var shape = Shape.Create(ShapeTestFixtures.GetValidDtoWithRoutes());
            Assert.IsTrue(ShapeIsValid(shape) && ShapeContainsPackages(shape) && ShapeContainsRoutes(shape));
        }

        private bool ShapeContainsRoutes(Shape shape)
        {
            return shape.Routes.Count() == 2 &&
                   shape.Routes.First().Shapes.Contains(shape) &&
                   shape.Routes.Last().Shapes.Contains(shape);
        }

        [TestMethod]
        public void AValidShapeWithUnitsCanBeConstructed()
        {
            var shape = Shape.Create(ShapeTestFixtures.GetValidDtoWithUnits());
            Assert.IsTrue(ShapeIsValid(shape) && ShapeContainsPackages(shape) && ShapeContainsRoutes(shape) && ShapeContainsUnits(shape));
        }

        private bool ShapeContainsUnits(Shape shape)
        {
            return shape.Units.Count() == 2 &&
                   shape.Units.First().Shapes.Contains(shape) &&
                   shape.Units.Last().Shapes.Contains(shape);
        }

        private bool ShapeIsValid(Shape shape)
        {
            return !String.IsNullOrWhiteSpace(shape.Name);
        }
    }
}
