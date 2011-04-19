using System;
using System.Collections.Generic;
using System.Text;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repository;
using Informedica.GenForm.Library.Services;
using Informedica.GenForm.Mvc2.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Tests.AcceptanceTests
{
    /// <summary>
    /// Summary description for CreateProductAcceptanceTest
    /// </summary>
    [TestClass]
    public class CreateProductAcceptanceTest
    {
        public CreateProductAcceptanceTest()
        {
            //
// TODO: Add constructor logic here
            //
        }

        private TestContext _testContextInstance;
        private static ProductsController _productsController;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _productsController = new ProductsController();
            var dal = Isolate.Fake.Instance<IDal<Product, ICriteria>>();
            GenFormServiceProvider.Instance.RegisterInstanceOfType(dal);
        }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CreateNewProductAndSave()
        {
            //
            IList<String> report = new List<string>();
            if (!AssertCreateNewProduct()) report.Add("Controller did not create new product");
            if (!AssertUpdateProduct()) report.Add("Controller did not update product form");
            if (!AssertSaveProduct()) report.Add("Controller did not save product");
            //

            Assert.IsTrue(report.Count == 0, CreateReport(report));
        }

        private static String CreateReport(IEnumerable<string> report)
        {
            var result = new StringBuilder();
            result.AppendLine();
            foreach (var item in report)
            {
                result.AppendLine(item);
            }
            return result.ToString();
        }

        private static Boolean AssertSaveProduct()
        {
            var product = Isolate.Fake.Instance<IProduct>();

            var result = _productsController.SaveProduct(product);

            return ActionResultParser.GetSuccessValueFromActionResult(result);
        }

        [Isolated]
        private static Boolean AssertUpdateProduct()
        {
            var product = new object(){};

            var result = _productsController.UpdateProduct(product);

            return (ActionResultParser.GetSuccessValueFromActionResult(result));
        }

        [Isolated]
        private static Boolean AssertCreateNewProduct()
        {
            var result = _productsController.CreateNewProduct();

            return (ActionResultParser.GetSuccessValueFromActionResult(result));
        }
    }
}
