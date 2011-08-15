using System.Collections.Generic;
using FluentNHibernate.Testing;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mappings
{
    /// <summary>
    /// Summary description for BrandMapShould
    /// </summary>
    [TestClass]
    public class BrandMapShould : MappingTests
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
        public void CorrectlyMapBrand()
        {
            new PersistenceSpecification<Brand>(_context.CurrentSession())
                .CheckProperty(b => b.Name, "Dynatra")
                //                    .CheckList(b => b.Products, CreateProductList());
                .VerifyTheMappings();

        }

        private static IEnumerable<Product> CreateProductList()
        {
            return new List<Product>
                       {
                            DomainFactory.Create<Product, ProductDto>(
                                ProductTestFixtures.GetProductDtoWithNoSubstances()),
                            DomainFactory.Create<Product, ProductDto>(
                                ProductTestFixtures.GetProductDtoWithOneSubstance())
                       };
        }
    }
}