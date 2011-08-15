using System.Collections.Generic;
using System.Diagnostics;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Testing;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for PackageMappingShould
    /// </summary>
    [TestClass]
    public class PackageMapShould: MappingTests
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

        // Use TestCleanup to run code after each test has run

        #endregion

        [TestMethod]
        public void CorrectlyMapPackageWithTwoShapes()
        {
            new PersistenceSpecification<Package>(_context.CurrentSession())
                .CheckProperty(b => b.Name, "ampul")
                .CheckProperty(p => p.Abbreviation, "amp")
                .CheckList(p => p.Shapes, GetShapesList(), (package, shape) => package.AddShape(shape))
                .VerifyTheMappings();
        }

        [TestMethod]
        public void WillNotAllowDuplicateShapes()
        {
            try
            {
                new PersistenceSpecification<Package>(_context.CurrentSession())
                    .CheckProperty(b => b.Name, "ampul")
                    .CheckProperty(p => p.Abbreviation, "amp")
                    .CheckList(p => p.Shapes, GetDuplicateShapes(), (package, shape) => package.AddShape(shape))
                    .VerifyTheMappings();
                Assert.Fail(new StackFrame().GetMethod().Name);
            }
            catch (System.Exception e)
            {
                Assert.IsNotNull(e);
            }
        }

        
        private IEnumerable<Shape> GetShapesList()
        {
            return new DefaultableList<Shape>
                       {
                           new Shape(new ShapeDto{ Name = "infusievloeistof"}),
                           new Shape(new ShapeDto{ Name = "injectiewater"})
                       };
        }

        private IEnumerable<Shape> GetDuplicateShapes()
        {
            return new DefaultableList<Shape>
                       {
                           new Shape(new ShapeDto{ Name = "infusievloeistof"}),
                           new Shape(new ShapeDto{ Name = "infusievloeistof"})
                       };
        }
    }
}
