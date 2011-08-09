using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Testing;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for ShapeMapShould
    /// </summary>
    [TestClass]
    public class ShapeMapShould
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
        public void CorrectlyMapAShape()
        {
            using (var session = GenFormApplication.Instance.SessionFactory.OpenSession())
            {
                new PersistenceSpecification<Shape>(session)
                    .CheckProperty(s => s.Name, "infusievloeistof")
                    .VerifyTheMappings();
            }
        }

        [TestMethod]
        public void AssociateShapeWithPackages()
        {
            using (var session = GenFormApplication.Instance.SessionFactory.OpenSession())
            {
                new PersistenceSpecification<Shape>(session)
                    .CheckProperty(s => s.Name, "infusievloeistof")
                    .CheckList(s => s.Packages, GetPackageList())
                    .VerifyTheMappings();
            }
        }


        [TestMethod]
        public void AssociateShapeWithUnit()
        {
            using (var session = GenFormApplication.Instance.SessionFactory.OpenSession())
            {
                new PersistenceSpecification<Shape>(session)
                    .CheckProperty(s => s.Name, "infusievloeistof")
                    .CheckList(s => s.Units, GetUnitList())
                    .VerifyTheMappings();
            }
            
        }

        [TestMethod]
        public void AssociateShapeWithRoute()
        {
            using (var session = GenFormApplication.Instance.SessionFactory.OpenSession())
            {
                new PersistenceSpecification<Shape>(session)
                    .CheckProperty(s => s.Name, "infusievloeistof")
                    .CheckList(s => s.Routes, GetRouteList())
                    .VerifyTheMappings();
            }
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
                           new Unit(new UnitDto{Abbreviation = "ml", AllowConversion = true, Divisor = 10, IsReference = false, Multiplier = 1000, Name = "milliliter", UnitGroupName = "volume"}),
                           new Unit(new UnitDto{Abbreviation = "l", AllowConversion = true, Divisor = 1, IsReference = true, Multiplier = 1, Name = "liter", UnitGroupName = "volume"}),
                       };
        }

        private IEnumerable<Package> GetPackageList()
        {
            return new List<Package>
                       {
                           new Package(new PackageDto {Name = "ampul"}),
                           new Package(new PackageDto {Name = "zak"})
                       };
        }
    }
}
