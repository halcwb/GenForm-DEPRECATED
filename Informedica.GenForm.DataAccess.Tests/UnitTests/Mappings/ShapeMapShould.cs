using System.Collections.Generic;
using FluentNHibernate.Testing;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for ShapeMapShould
    /// </summary>
    [TestClass]
    public class ShapeMapShould : MappingTests
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
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext) { DatabaseCleaner.CleanDataBase(); }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        
        #endregion

        //ToDo: Add tests with lists with multiple entries

        [TestMethod]
        public void CorrectlyMapAShape()
        {
            new PersistenceSpecification<Shape>(_context.CurrentSession())
                .CheckProperty(s => s.Name, "infusievloeistof")
                .VerifyTheMappings(); 
        }

        [TestMethod]
        public void AssociateShapeWithOnePackage()
        {
            new PersistenceSpecification<Shape>(_context.CurrentSession())
                .CheckProperty(s => s.Name, "infusievloeistof")
                .CheckList(s => s.Packages, GetPackageList())
                .VerifyTheMappings();
        }


        [TestMethod]
        public void AssociateShapeWithUnit()
        {
                new PersistenceSpecification<Shape>(_context.CurrentSession())
                    .CheckProperty(s => s.Name, "infusievloeistof")
                    .CheckList(s => s.Units, GetUnitList())
                    .VerifyTheMappings();
            
        }

        [TestMethod]
        public void AssociateShapeWithOneRoute()
        {
            new PersistenceSpecification<Shape>(_context.CurrentSession())
                .CheckProperty(s => s.Name, "infusievloeistof")
                .CheckList(s => s.Routes, GetRouteList())
                .VerifyTheMappings();
        }

        private IEnumerable<Route> GetRouteList()
        {
            return new List<Route>
                       {
                           new Route(new RouteDto {Abbreviation = "iv", Name = "intraveneus"})
                       };
        }

        private IEnumerable<Unit> GetUnitList()
        {
            return new List<Unit>
                       {
                           UnitFactory.CreateUnit(new UnitDto{Abbreviation = "ml", AllowConversion = true, Divisor = 10, IsReference = false, Multiplier = 1000, Name = "milliliter", UnitGroupName = "algemeen"}),
                           UnitFactory.CreateUnit(new UnitDto{Abbreviation = "l", AllowConversion = true, Divisor = 1, IsReference = true, Multiplier = 1, Name = "liter", UnitGroupName = "volume"}),
                       };
        }

        private IEnumerable<Package> GetPackageList()
        {
            return new List<Package>
                       {
                           new Package(new PackageDto {Name = "ampul"})
                       };
        }
    }
}
