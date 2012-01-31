using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.Mvc3.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    /// <summary>
    /// Summary description for ProductControllerShould
    /// </summary>
    [TestClass]
    public class ProductControllerShould
    {
        private static ProductController _controller;
        private ProductDto _dto;

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
            // ToDo: rewrite test
            //Isolate.WhenCalled(() => _services.SaveProduct(_dto)).ReturnRecursiveFake();

            try
            {
                _controller.SaveProduct(_dto);
                //Isolate.Verify.WasCalledWithExactArguments(() => _services.SaveProduct(_dto));
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
            // ToDo rewrite test
            //IsolateController();
            //Isolate.WhenCalled(() => _services.DeleteProduct(1)).IgnoreCall();

            //try
            //{
            //    _controller.DeleteProduct(1);
            //    Isolate.Verify.WasCalledWithExactArguments(() => _services.DeleteProduct(1));
            //}
            //catch (Exception e)
            //{
            //    Assert.Fail(e.ToString());
            //}
        }

        [TestMethod]
        public void ReturnAnErrorMessageWhenCannotDeleteProduct()
        {
            _controller = new ProductController();

            var result = _controller.DeleteProduct(Guid.NewGuid().ToString());
            Assert.IsFalse(String.IsNullOrEmpty(ActionResultParser.GetPropertyValue<String>(result, "message")));
        }

        [TestMethod]
        public void ReturnSuccessIsTrueValueWhenCanDeleteProduct()
        {
            IsolateController();
            var product = Isolate.Fake.Instance<IProduct>();
            Isolate.Fake.StaticMethods(typeof(ProductServices));
            Isolate.WhenCalled(() => ProductServices.Delete(product)).IgnoreCall();

            var result = _controller.DeleteProduct(Guid.NewGuid().ToString());
            Assert.IsTrue(ActionResultParser.GetSuccessValue(result));
        }

        private void IsolateController()
        {
            _dto = Isolate.Fake.Instance<ProductDto>();
            _controller = new ProductController();
        }


    }
}
