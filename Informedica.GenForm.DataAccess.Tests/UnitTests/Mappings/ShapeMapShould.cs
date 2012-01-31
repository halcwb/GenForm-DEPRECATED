using System.Collections.Generic;
using FluentNHibernate.Testing;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for ShapeMapShould
    /// </summary>
    [TestClass]
    public class ShapeMapShould : TestSessionContext
    {
        public ShapeMapShould() : base(false) {}

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
        
        #endregion

        //ToDo: Add tests with lists with multiple entries

        [TestMethod]
        public void CorrectlyMapAShape()
        {
            new PersistenceSpecification<Shape>(Context.CurrentSession())
                .CheckProperty(s => s.Name, "infusievloeistof")
                .VerifyTheMappings(); 
        }

        [TestMethod]
        public void AssociateShapeWithOnePackage()
        {
            var comparer = new ShapeComparer();
            new PersistenceSpecification<Shape>(Context.CurrentSession(), comparer)
                .CheckProperty(s => s.Name, "infusievloeistof")
                .CheckList(s => s.PackageSet, GetPackageList())
                .VerifyTheMappings();
        }

        // ToDo: solve problem with VerifyMappings for sets
        public void AssociateShapeWithTwoPackages()
        {
            new PersistenceSpecification<Shape>(Context.CurrentSession())
                .CheckProperty(s => s.Name, "infusievloeistof")
                .CheckList(s => s.PackageSet, GetPackageListTwo())
                .VerifyTheMappings();
        }

        private IEnumerable<Package> GetPackageListTwo()
        {
            return new List<Package>
                       {
                           Package.Create(new PackageDto {Name = "ampul"}),
                           Package.Create(new PackageDto {Name = "zak"})
                       };
        }

        [TestMethod]
        public void AssociateShapeWithOneUnitGroup()
        {
            var comparer = new UnitGroupComparer();
            new PersistenceSpecification<Shape>(Context.CurrentSession(), comparer)
                .CheckProperty(s => s.Name, "infusievloeistof")
                .CheckList(s => s.UnitGroupSet, GetUnitGroupList())
                .VerifyTheMappings();
        }

        [TestMethod]
        public void AssociateShapeWithOneRoute()
        {
            var comparer = new ShapeComparer();
            new PersistenceSpecification<Shape>(Context.CurrentSession(), comparer)
                .CheckProperty(s => s.Name, "infusievloeistof")
                .CheckList(s => s.RouteSet, GetRouteList())
                .VerifyTheMappings();
        }

        // ToDo: solve problem with VerifyMappings for sets
        public void AssociateShapeWithTwoRoutes()
        {
            var comparer = new ShapeComparer();
            new PersistenceSpecification<Shape>(Context.CurrentSession(), comparer)
                .CheckProperty(s => s.Name, "infusievloeistof")
                .CheckList(s => s.RouteSet, GetRouteListWithTwo())
                .VerifyTheMappings();
        }

        private IEnumerable<Route> GetRouteListWithTwo()
        {
            return new List<Route>
                       {
                           Route.Create(new RouteDto {Abbreviation = "iv", Name = "intraveneus"}),
                           Route.Create(new RouteDto {Abbreviation = "or", Name = "oraal"})
                       };
        }

        private IEnumerable<Route> GetRouteList()
        {
            return new List<Route>
                       {
                           Route.Create(new RouteDto {Abbreviation = "iv", Name = "intraveneus"})
                       };
        }

        private IEnumerable<UnitGroup> GetUnitGroupList()
        {
            return new List<UnitGroup>
                       {
                           UnitGroup.Create(new UnitGroupDto{ AllowConversion = true, Name = "volume"})
                       };
        }

        private IEnumerable<Package> GetPackageList()
        {
            return new List<Package>
                       {
                           Package.Create(new PackageDto {Name = "ampul"})
                       };
        }
    }
}
