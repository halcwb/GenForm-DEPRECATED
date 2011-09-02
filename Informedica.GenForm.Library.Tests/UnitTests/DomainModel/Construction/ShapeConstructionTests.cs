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
            var shape = ShapeTestFixtures.CreateIvFluidShape();
            Assert.IsTrue(ShapeIsValid(shape));
        }

        [TestMethod]
        public void ThatShapeWithoutNameThrowsException()
        {
            try
            {
                var dto = ShapeTestFixtures.GetIvFluidDto();
                dto.Name = String.Empty;
                Shape.Create(dto);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }

        [TestMethod]
        public void AValidShapeWithPackagesCanBeConstructed()
        {
            var shape = Shape.Create(ShapeTestFixtures.GetValidDtoWithPackages());
            var package1 = Package.Create(ShapeTestFixtures.GetValidDtoWithPackages().Packages.First());
            var package2 = Package.Create(ShapeTestFixtures.GetValidDtoWithPackages().Packages.Last());

            shape.AddPackage(package1);
            shape.AddPackage(package2);
            Assert.IsTrue(ShapeIsValid(shape) && ShapeContainsPackages(shape));
        }

        private static bool ShapeContainsPackages(Shape shape)
        {
            return shape.PackageSet.Count() == 2 &&
                   shape.PackageSet.First().ShapeSet.Contains(shape) &&
                   shape.PackageSet.Last().ShapeSet.Contains(shape);
        }

        private static bool ShapeIsValid(Shape shape)
        {
            return !String.IsNullOrWhiteSpace(shape.Name);
        }
    }
}
