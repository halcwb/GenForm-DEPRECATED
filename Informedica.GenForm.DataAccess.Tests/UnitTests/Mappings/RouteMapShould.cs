using System.Collections.Generic;
using FluentNHibernate.Testing;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for RouteMapShould
    /// </summary>
    [TestClass]
    public class RouteMapShould : MappingTests
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

        [TestMethod]
        public void CorrectlyMapARoute()
        {
            new PersistenceSpecification<Route>(_context.CurrentSession())
                .CheckProperty(r => r.Name, "intraveneus")
                .CheckProperty(r => r.Abbreviation, "iv")
                .VerifyTheMappings();
        }

        [TestMethod]
        public void BeAssociatedWithShapes()
        {
            new PersistenceSpecification<Route>(_context.CurrentSession())
                .CheckProperty(r => r.Name, "intraveneus")
                .CheckProperty(r => r.Abbreviation, "iv")
                .CheckList(r => r.Shapes, GetShapesList())
                .VerifyTheMappings();
        }

        private IEnumerable<Shape> GetShapesList()
        {
            return new List<Shape>
                       {
                           new Shape(new ShapeDto {Name = "infusievloeistof"})
                       };
        }
    }
}
