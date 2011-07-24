using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.Services;
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
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(ProductAssembler.RegisterDependencies());
            });
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
        //    ProductAssembler.RegisterDependencies();
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
            ObjectFactoryAssertUtility.AssertRegistration<IProduct>("no implementation for product was found");
        }

        [TestMethod]
        public void AnImplementationOfProductServices()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IProductServices>("no implementation for product services was found");
        }

        [TestMethod]
        public void AnImplementationOfProductRepository()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IProductRepository>("no implementation for product repository was found");
        }

        [TestMethod]
        public void AnImplementationOfBrand()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IBrand>("no implementation for brand was found");
        }

        [TestMethod]
        public void AnImplementationOfBrandRepository()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IBrandRepository>("no implementation for brand repository was found");
        }

        [TestMethod]
        public void AnImplementationOfGeneric()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IGeneric>("no implementation for brand was found");
        }

        [TestMethod]
        public void AnImplementationOfGenericRepository()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IGenericRepository>("no implementation for brand repository was found");
        }

        [TestMethod]
        public void AnImplementationOfShape()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IShape>("no implementation for brand was found");
        }

        [TestMethod]
        public void AnImplementationOfShapeRepository()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IShapeRepository>("no implementation for brand repository was found");
        }

        [TestMethod]
        public void AnImplementationOfUnit()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IUnit>("no implementation for brand was found");
        }

        [TestMethod]
        public void AnImplementationOfUnitRepository()
        {
            ObjectFactoryAssertUtility.AssertRegistration<IUnitRepository>("no implementation for brand repository was found");
        }

        [TestMethod]
        public void AnImplementationOfSubstance()
        {
            ObjectFactoryAssertUtility.AssertRegistration<ISubstance>("no implementation for brand was found");
        }

        [TestMethod]
        public void AnImplementationOfSubstanceRepository()
        {
            ObjectFactoryAssertUtility.AssertRegistration<ISubstanceRepository>("no implementation for brand repository was found");
        }


    }
}
