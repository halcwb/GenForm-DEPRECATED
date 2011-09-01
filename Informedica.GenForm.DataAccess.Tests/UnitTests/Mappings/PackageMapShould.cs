using System.Collections.Generic;
using System.Diagnostics;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Testing;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for PackageMappingShould
    /// </summary>
    [TestClass]
    public class PackageMapShould: TestSessionContext
    {
        private TestContext testContextInstance;

        public PackageMapShould() : base(false) {}

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
        public void CorrectlyMapPackage()
        {
            new PersistenceSpecification<Package>(Context.CurrentSession())
                .CheckProperty(b => b.Name, "ampul")
                .CheckProperty(p => p.Abbreviation, "amp")
                .VerifyTheMappings();
            
        }

        [TestMethod]
        public void CorrectlyMapPackageWithOneShape()
        {
            var comparer = new PackageComparer();
            new PersistenceSpecification<Package>(Context.CurrentSession(), comparer)
                .CheckProperty(b => b.Name, "ampul")
                .CheckProperty(p => p.Abbreviation, "amp")
                .CheckList(p => p.Shapes, GetShapesList(), (package, shape) => package.AddShape(shape))
                .VerifyTheMappings();
        }

        [TestMethod]
        public void WillThrowAnErrorWhenDuplicateShapes()
        {
            try
            {
                var comparer = new PackageComparer();
                new PersistenceSpecification<Package>(Context.CurrentSession(), comparer)
                    .CheckProperty(b => b.Name, "ampul")
                    .CheckProperty(p => p.Abbreviation, "amp")
                    .CheckList(p => p.Shapes, GetDuplicateShapes(), (package, shape) => package.AddShape(shape))
                    .VerifyTheMappings();
                Assert.Fail(new StackFrame().GetMethod().Name);
            }
            catch (System.Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }

        
        private IEnumerable<Shape> GetShapesList()
        {
            return new DefaultableList<Shape>
                       {
                           Shape.Create(new ShapeDto{ Name = "infusievloeistof"})                       };
        }

        private IEnumerable<Shape> GetDuplicateShapes()
        {
            return new DefaultableList<Shape>
                       {
                           Shape.Create(new ShapeDto{ Name = "infusievloeistof"}),
                           Shape.Create(new ShapeDto{ Name = "infusievloeistof"})
                       };
        }
    }
}
