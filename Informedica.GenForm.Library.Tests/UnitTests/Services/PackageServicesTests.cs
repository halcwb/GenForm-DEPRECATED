using System;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.TestFixtures.Fixtures;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Services
{
    /// <summary>
    /// Summary description for PackageServicesTests
    /// </summary>
    [TestClass]
    public class PackageServicesTests : TestSessionContext
    {
        public PackageServicesTests() : base(true) { }

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
        public void ThatPackageCanBeGet()
        {
            var package = GetPackageWithoutShape();
            Assert.IsInstanceOfType(package, typeof(Package));
        }

        [TestMethod]
        public void ThatPackageHasAnId()
        {
            var package = GetPackageWithoutShape();
            Assert.IsTrue(package.Id != Guid.Empty);
        }

        private static Package GetPackageWithoutShape()
        {
            var package = PackageServices.WithDto(PackageTestFixtures.GetAmpulDto()).Get();
            return package;
        }

        [TestMethod]
        public void ThatAPackageCanBeFound()
        {
            var package = PackageServices.WithDto(PackageTestFixtures.GetAmpulDto()).Get();
            Assert.AreEqual(package,
                            PackageServices.Packages.Single(x => x.Name == package.Name));
        }

        [TestMethod]
        public void ThatPackageCanBeFoundByName()
        {
            var package = PackageServices.WithDto(PackageTestFixtures.GetAmpulDto()).Get();
            Assert.AreEqual(package, PackageServices.GetByName(package.Name));
        }

        [TestMethod]
        public void ThatAPackagaWithAshapeCanBeGet()
        {
            var package = GetPackageWithShape();
            Assert.IsTrue(package.ShapeSet.Any());
            PackageServices.Delete(package);
        }

        private static Package GetPackageWithShape()
        {
            return PackageServices.WithDto(PackageTestFixtures.GetDtoWithOneShape()).Get();
        }

        [TestMethod]
        public void ThatAPackageCanBeDeleted()
        {
            var package = GetPackageWithoutShape();
            PackageServices.Delete(package);
            Assert.IsNull(PackageServices.Packages.SingleOrDefault(p => p.Name == package.Name));
        }

        [TestMethod]
        public void ThatPackageWithShapeCanBeDeleted()
        {
            var package = GetPackageWithShape();
            PackageServices.Delete(package);
            Assert.IsNull(PackageServices.Packages.SingleOrDefault(p => p.Name == package.Name));
        }

        [TestMethod]
        public void ThatPackageWithShapeAfterDeletePackageLeavesShape()
        {
            var package = GetPackageWithShape();
            var shapeName = package.ShapeSet.First().Name;
            PackageServices.Delete(package);
            var shape = ShapeServices.Shapes.SingleOrDefault(x => x.Name == shapeName);
            Assert.IsNotNull(shape);
        }

        [TestMethod]
        public void ThatPackageWithTwoShapesCanBeGet()
        {
            var package = GetPackageWithTwoShapes();
            Assert.AreEqual(2, package.ShapeSet.Count());
        }

        [TestMethod]
        public void ThatPackageWithTwoShapesCanBeDeleted()
        {
            var package = GetPackageWithTwoShapes();
            PackageServices.Delete(package);
            Assert.IsNull(PackageServices.Packages.SingleOrDefault(x => x.Name == package.Name));
        }

        [TestMethod]
        public void ThatPackageWithTwoShapesCanBeDeletedLeavesTwoShapes()
        {
            var package = GetPackageWithTwoShapes();
            PackageServices.Delete(package);
            Assert.AreEqual(2, ShapeServices.Shapes.Count());
        }

        private Package GetPackageWithTwoShapes()
        {
            return PackageServices.WithDto(PackageTestFixtures.GetDtoWithTwoShapes()).Get();
        }
    }
}
