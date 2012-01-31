using FluentNHibernate.Testing;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.TestFixtures.Fixtures;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for ProductMapping
    /// </summary>
    [TestClass]
    public class ProductMapShould : TestSessionContext
    {
        public ProductMapShould() : base(false) {}

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
        public void CorrectlyMapAProduct()
        {
            var group = UnitGroup.Create(new UnitGroupDto { AllowConversion = true, Name = "massa"});
            new PersistenceSpecification<Product>(Context.CurrentSession(), new ProductComparer())
                .CheckProperty(x => x.Name, "dopamine Dynatra infusievloeistof 200 mg in 5 mL ampul")
                .CheckProperty(x => x.GenericName, "dopamine")
                .CheckReference(x => x.Brand, Brand.Create(new BrandDto {Name = "Dynatra"}))
                .CheckProperty(x => x.DisplayName, "dopamine Dynatra infusievloeistof 200 mg in 5 mL ampul")
                .CheckReference(x => x.Package, Package.Create(new PackageDto {Name = "ampul", Abbreviation = "amp"}))
                .CheckReference(x => x.Shape, Shape.Create(new ShapeDto {Name = "infusievloeistof"}))
                .CheckProperty(x => x.Quantity, UnitValue.Create(10, Unit.Create(UnitTestFixtures.GetTestUnitMilligram(), group)))

                .VerifyTheMappings();
        }
    }
}
