using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.Library.Services.Products.dto;
using Informedica.GenForm.Mvc3.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using StructureMap;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    /// <summary>
    /// Summary description for ProductControllerShould
    /// </summary>
    [TestClass]
    public class ProductControllerShould
    {
        private TestContext testContextInstance;
        private static ProductController _controller;
        private IProductServices _services;
        private ProductDto _dto;

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
        public static void MyClassInitialize(TestContext testContext)
        {
            GenFormApplication.Initialize();
            _controller = GetNewController();
        }

        private static ProductController GetNewController()
        {
            return  new ProductController();
        }

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

        [Isolated]
        [TestMethod]
        public void CallProductServicesToSaveProduct()
        {
            IsolateController();
            Isolate.WhenCalled(() => _services.SaveProduct(_dto)).ReturnRecursiveFake();

            try
            {
                _controller.SaveProduct(_dto);
                Isolate.Verify.WasCalledWithExactArguments(() => _services.SaveProduct(_dto));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void CallProductServicesToDeleteAProduct()
        {
            IsolateController();
            Isolate.WhenCalled(() => _services.DeleteProduct(1)).IgnoreCall();

            try
            {
                _controller.DeleteProduct(1);
                Isolate.Verify.WasCalledWithExactArguments(() => _services.DeleteProduct(1));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void ReturnAnErrorMessageWhenCannotDeleteProduct()
        {
            _controller = new ProductController();

            var result = _controller.DeleteProduct(0);
            Assert.IsFalse(String.IsNullOrEmpty(ActionResultParser.GetPropertyValue<String>(result, "message")));
        }

        [TestMethod]
        public void ReturnSuccessIsTrueValueWhenCanDeleteProduct()
        {
            IsolateController();
            Isolate.WhenCalled(() => _services.DeleteProduct(1)).WithExactArguments().IgnoreCall();

            var result = _controller.DeleteProduct(1);
            Assert.IsTrue(ActionResultParser.GetSuccessValue(result));
        }

        [TestMethod]
        public void SaveANewSubstanceToDatabase()
        {
            var result = _controller.AddNewSubstance(GetSubstance());
            
            Assert.IsTrue(ActionResultParser.GetSuccessValue(result), 
                          "new substance could not be submitted to the database: " + ActionResultParser.GetPropertyValue<String>(result, "message"));
        }

        private static JObject GetSubstance()
        {
            var obj = Isolate.Fake.Instance<JObject>();
            Isolate.WhenCalled(() => obj.Value<String>("SubstanceName")).WillReturn("dopamine");
            return obj;
        }

        private void IsolateController()
        {
            _services = Isolate.Fake.Instance<IProductServices>();
            _dto = Isolate.Fake.Instance<ProductDto>();
            _controller = new ProductController(_services);
        }


    }
}
