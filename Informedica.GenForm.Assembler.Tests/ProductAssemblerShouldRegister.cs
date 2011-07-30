using System.Linq;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.Library.Services.Products.dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.Assembler.Tests
{
    
    /// <summary>
    ///This is a test class for ProductAssemblerTest and is intended
    ///to contain all ProductAssemblerTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProductAssemblerShouldRegister
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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            GenFormApplication.Initialize();
        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //    GenFormApplication.Initialize();
        //}
        
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod]
        public void AnImplementationOfProduct()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IProduct>(
                ObjectFactoryAssertUtility.GetMessageFor<IProduct>());
        }

        [TestMethod]
        public void AspecificConstructorMethodForProduct()
        {
            const string productName = "dopamine Dynatra infusievloeistof 5 mL ampul";
            var dto = new ProductDto{ ProductName = productName};
            var product = ObjectFactory.With(dto).GetInstance<IProduct>();
            
            Assert.IsInstanceOfType(product, typeof(IProduct));
            Assert.IsTrue(product.ProductName == productName);
        }

        [TestMethod]
        public void AnImplementationOfProductServices()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IProductServices>(
                ObjectFactoryAssertUtility.GetMessageFor<IProductServices>());
        }

        [TestMethod]
        public void AnImplementationOfProductRepository()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IProductRepository>(
                ObjectFactoryAssertUtility.GetMessageFor<IProductRepository>());
        }

        [TestMethod]
        public void AnImplementationOfProductMapper()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IDataMapper<IProduct, Database.Product>>(
                ObjectFactoryAssertUtility.GetMessageFor<IDataMapper<IProduct, Database.Product>>());
        }

        [TestMethod]
        public void AnImplementationOfBrand()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IBrand>(
                ObjectFactoryAssertUtility.GetMessageFor<IBrand>());
        }

        [TestMethod]
        public void AnImplementationOfBrandRepository()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IBrandRepository>(
                ObjectFactoryAssertUtility.GetMessageFor<IBrandRepository>());
        }

        [TestMethod]
        public void AnImplementationOfBrandMapper()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IDataMapper<IBrand, Database.Brand>>(
                ObjectFactoryAssertUtility.GetMessageFor<IDataMapper<IBrand, Database.Brand>>());
        }

        [TestMethod]
        public void AnImplementationOfGeneric()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IGeneric>(
                ObjectFactoryAssertUtility.GetMessageFor<IGeneric>());
        }

        [TestMethod]
        public void AnImplementationOfGenericRepository()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IGenericRepository>(
                ObjectFactoryAssertUtility.GetMessageFor<IGenericRepository>());
        }

        [TestMethod]
        public void AnImplementationOfGenericMapper()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IDataMapper<IGeneric, Database.Substance>>(
                ObjectFactoryAssertUtility.GetMessageFor<IDataMapper<IGeneric, Database.Substance>>());
        }

        [TestMethod]
        public void AnImplementationOfShape()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IShape>(
                ObjectFactoryAssertUtility.GetMessageFor<IShape>());
        }

        [TestMethod]
        public void AnImplementationOfShapeRepository()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IShapeRepository>(
                ObjectFactoryAssertUtility.GetMessageFor<IShapeRepository>());
        }

        [TestMethod]
        public void AnImplementationOfShapeMapper()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IDataMapper<IShape, Database.Shape>>(
                ObjectFactoryAssertUtility.GetMessageFor<IDataMapper<IShape, Database.Shape>>());
        }

        [TestMethod]
        public void AnImplementationOfUnit()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IUnit>(
                ObjectFactoryAssertUtility.GetMessageFor<IUnit>());
        }

        [TestMethod]
        public void AnImplementationOfUnitRepository()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IUnitRepository>(
                ObjectFactoryAssertUtility.GetMessageFor<IUnitRepository>());
        }

        [TestMethod]
        public void AnImplementationOfUnitMapper()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IDataMapper<IUnit, Database.Unit>>(
                ObjectFactoryAssertUtility.GetMessageFor<IDataMapper<IUnit,Database.Unit>>());
        }

        [TestMethod]
        public void AnImplementationOfSubstance()
        {
            ObjectFactoryAssertUtility.AssertRegistration<ISubstance>(
                ObjectFactoryAssertUtility.GetMessageFor<ISubstance>());
        }

        [TestMethod]
        public void AnImplementationOfSubstanceRepository()
        {
            ObjectFactoryAssertUtility.AssertRegistration<ISubstanceRepository>(
                ObjectFactoryAssertUtility.GetMessageFor<ISubstanceRepository>());
        }

        [TestMethod]
        public void AnImplementationOfSubstanceMapper()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IDataMapper<ISubstance, Database.Substance>>(
                ObjectFactoryAssertUtility.GetMessageFor<IDataMapper<ISubstance, Database.Substance>>());
        }

    }
}
