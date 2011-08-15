using System.Collections;
using System.Collections.Generic;
using FluentNHibernate.Testing;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for ProductMapping
    /// </summary>
    [TestClass]
    public class ProductMapShould : MappingTests
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
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
        public void CorrectlyMapAProduct()
        {
            new PersistenceSpecification<Product>(Context.CurrentSession(), new ProductComparer())
                .CheckProperty(x => x.Name, "dopamine Dynatra infusievloeistof 200 mg in 5 mL ampul")
                .CheckProperty(x => x.GenericName, "dopamine")
                .CheckReference(x => x.Brand, new Brand(new BrandDto {Name = "Dynatra"}))
                .CheckProperty(x => x.DisplayName, "dopamine Dynatra infusievloeistof 200 mg in 5 mL ampul")
                .CheckReference(x => x.Package, new Package(new PackageDto {Name = "ampul", Abbreviation = "amp"}))
                .CheckReference(x => x.Shape, new Shape(new ShapeDto {Name = "infusievloeistof"}))
                .CheckProperty(x => x.Quantity, new UnitValue(10, UnitFactory.CreateUnit(UnitTestFixtures.GetTestUnitMilligram())))

                .VerifyTheMappings();
        }

    }

    public class ProductComparer : NameComparer, IEqualityComparer<Product>, IEqualityComparer
    {
        public bool Equals(Product x, Product y)
        {
            return x.Equals(y) || EqualName(x.Name, y.Name);
        }

        public int GetHashCode(Product obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(object x, object y)
        {
            if (x.GetType() == typeof(Product)) return Equals((Product) x, (Product) y);
            return true;
        }

        public int GetHashCode(object obj)
        {
            return GetHashCode((Product)obj);
        }
    }
}
