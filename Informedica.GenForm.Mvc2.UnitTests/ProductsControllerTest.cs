using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services;
using Informedica.GenForm.Mvc2.Controllers;
using Informedica.GenForm.Presentation.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Mvc2.UnitTests
{
    
    
    [TestClass]
    public class ProductsControllerTest
    {


        private TestContext _testContextInstance;

        public TestContext TestContext
        {
            get 
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
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
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod]
        public void CreateNewProductCallsProductServicesNewProduct()
        {
            var controller = new ProductsController();
            IProductServices services = SetUpProductServices();

            try
            {
                controller.CreateNewProduct();
                Isolate.Verify.WasCalledWithAnyArguments(() => services.CreateProduct());
            }
            catch (VerifyException)
            {
                Assert.Fail("ProductsController did not call ProductsServices.CreateNewProduct");
            }
        }

        private static IProductServices SetUpProductServices()
        {
            var product = Isolate.Fake.Instance<IProduct>();
            var services = GenFormServiceProvider.Instance.Resolve<IProductServices>();
            Isolate.WhenCalled(() => services.CreateProduct()).WillReturn(product);
            return services;
        }

        [TestMethod]
        public void CreateNewProductReturnsProductPresentation()
        {
            var controller = new ProductsController();
            SetUpProductServices();

            var fakePresentation = Isolate.Fake.Instance<IProductPresentation>();
            Isolate.NonPublic.WhenCalled(typeof(ProductsController), "GetProductPresentation").WillReturn(fakePresentation);

            var result = controller.CreateNewProduct();

            Assert.IsNotNull(ActionResultParser.GetValueFromActionResult<IProductPresentation>(result, "data"));
        }
    }
}
