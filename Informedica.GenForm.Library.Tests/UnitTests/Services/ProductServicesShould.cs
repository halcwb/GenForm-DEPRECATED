using System;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.Tests.Fixtures;
using Informedica.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Library.Tests.UnitTests.Services
{
    
    
    /// <summary>
    ///This is a test class for IProductServicesTest and is intended
    ///to contain all IProductServicesTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProductServicesShould
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
        //    ProductAssembler.RegisterDependencies();
        //}
        
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetEmptyProduct
        ///</summary>
        [TestMethod]
        public void BeAbleToReturnAnEmptyProduct()
        {
            var services = GetProductServices(); 
            Assert.IsTrue(ObjectExaminer.ObjectHasEmptyProperties(services.GetEmptyProduct()), 
                          "services did not return an empty product");
        }

        [TestMethod]
        public void HaveAhelperClassToDetermineWhetherProductIsEmpty()
        {
            var dto = new ProductDto();
            var product = ObjectFactory.With(dto).GetInstance<IProduct>();
            Assert.IsTrue(ObjectExaminer.ObjectHasEmptyProperties(product), "helper method should return true");

            dto.ProductName = "Not Empty";
            product = ObjectFactory.With(dto).GetInstance<IProduct>();
            Assert.IsFalse(ObjectExaminer.ObjectHasEmptyProperties(product), "helper method should return false");
        }

        [Isolated]
        [TestMethod]
        public void CallProductRepositoryToSaveAdrug()
        {
            var productDto = new ProductDto();
            var product = Isolate.Fake.Instance<IProduct>();

            var repos = GetFakeRepository<IRepository<IProduct>, IProduct>(product);
            ObjectFactory.Inject(repos);

            try
            {
                using (repos.Rollback)
                {
                    GetProductServices().SaveProduct(productDto);

                } 
                Isolate.Verify.WasCalledWithAnyArguments(() => repos.Insert(product));
            }
            catch (Exception e)
            {
                AssertExceptionType(e, "product repository was not called to save product");
            }
            finally
            {
                GenFormApplication.Initialize();
            }
        }

        [TestMethod]
        public void BeAbleToSaveAproductWithNoSubstances()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();

            TrySaveProduct(dto);
        }

        private void TrySaveProduct(ProductDto dto)
        {
            var repos = ObjectFactory.GetInstance<IRepository<IProduct>>();
            ObjectFactory.Inject(typeof(IRepository<IProduct>), repos);
            try
            {
                using (repos.Rollback)
                {
                    GetProductServices().SaveProduct(dto);
                }
            }
            catch (Exception e)
            {
                Assert.Fail("did not save: " + e);
            }
        }

        [TestMethod]
        public void NotBeAbleToSaveAnInvalidProduct()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            dto.GenericName = null;

            try
            {
                TrySaveProduct(dto);
                Assert.Fail("invalid product should not be saved");
            }
            catch (Exception e)
            {
                Assert.IsNotNull(e.ToString());
            }

        }

        [TestMethod]
        public void BeAbleToSaveProductWithOneSubstance()
        {
            var dto = ProductTestFixtures.GetProductDtoWithOneSubstance();
            
            TrySaveProduct(dto);
        }

        [TestMethod]
        public void NotBeAbleToSaveProductWithInvalidSubstance()
        {
            var dto = ProductTestFixtures.GetProductDtoWithOneSubstance();
            dto.Substances.First().Substance = "";

            try
            {
                TrySaveProduct(dto);
                Assert.Fail("should not be able to save product with invalid substance");
            }
            catch (Exception e)
            {
                Assert.IsNotNull(e);
            }
        }

        [TestMethod]
        public void BeAbleToSaveProductWithTwoSubstances()
        {
            var dto = ProductTestFixtures.GetProductDtoWithTwoSubstances();

            TrySaveProduct(dto);
        }

        [TestMethod]
        public void BeAbleToSaveProductWithTwoSubstancesAndAroute()
        {
            var dto = ProductTestFixtures.GetProductDtoWithTwoSubstancesAndRoute();

            TrySaveProduct(dto);
        }

        [TestMethod]
        public void NotBeAbleToSaveProductWithInvalidRoute()
        {
            var dto = ProductTestFixtures.GetProductDtoWithTwoSubstancesAndRoute();
            dto.Routes.First().Route = "";

            try
            {
                TrySaveProduct(dto);
                Assert.Fail("should not be able to save route with invalid product");
            }
            catch (Exception e)
            {
                Assert.IsNotNull(e);
            }
        }

        [Isolated]
        [TestMethod]
        public void CallBrandRepositoryToAddAnewBrand()
        {
            var brand = ObjectFactory.GetInstance<IBrand>();

            var repos = GetFakeRepository<IRepository<IBrand>, IBrand>(brand);
            ObjectFactory.Inject(repos);

            try
            {
                using (repos.Rollback)
                {
                    GetProductServices().AddNewBrand(brand);

                } 
                Isolate.Verify.WasCalledWithExactArguments(() => repos.Insert(brand));
            }
            catch (Exception e)
            {
                AssertExceptionType(e, "Brand repository was not called to add brand");
            }

        }

        [Isolated]
        [TestMethod]
        public void CallSubstanceRepositoryToAddNewSubstance()
        {
            var dto = new SubstanceDto {Id = Guid.Empty, Name = "test"};
            var substance = DomainFactory.Create<ISubstance, SubstanceDto>(dto);

            var repos = GetFakeRepository<IRepository<ISubstance>, ISubstance>(substance);
            ObjectFactory.Inject(repos);

            try
            {
                using (repos.Rollback)
                {
                    GetProductServices().AddNewSubstance(dto);

                } 
                Isolate.Verify.WasCalledWithAnyArguments(() => repos.Insert(substance));
            }
            catch (Exception e)
            {
                AssertExceptionType(e, "Substance repository was not called to save substance");
            }
        }

        [Isolated]
        [TestMethod]
        public void AddNewSubstanceToRepository()
        {
            var substance = new SubstanceDto { Id = Guid.Empty, Name = "test"};

            var repos = ObjectFactory.GetInstance<IRepository<ISubstance>>();
            ObjectFactory.Inject(typeof(IRepository<ISubstance>), repos);

            try
            {
                using (repos.Rollback)
                {
                    GetProductServices().AddNewSubstance(substance);
                }
            }
            catch (Exception e)
            {
                Assert.Fail("substance could not be inserted in repository: " + e);
            }
        }


        private static T GetFakeRepository<T, TC>(TC item) where T: IRepository<TC>
        {
            var repos = Isolate.Fake.Instance<T>();
            Isolate.WhenCalled(() => repos.Insert(item)).IgnoreCall();

            return repos;
        }

        private static void AssertExceptionType(Exception e, String message)
        {
            if (e.GetType() != typeof(VerifyException)) throw e;
            Assert.Fail(message);
        }

        private static IProductServices GetProductServices()
        {
            return ObjectFactory.GetInstance<IProductServices>();
        }

    }
}
