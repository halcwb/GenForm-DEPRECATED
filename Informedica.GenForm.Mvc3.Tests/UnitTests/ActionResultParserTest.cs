using Ext.Direct.Mvc;
using Informedica.GenForm.Mvc3.Controllers;
using Informedica.GenForm.Presentation.Products;
using Informedica.GenForm.Tests;
using TypeMock.ArrangeActAssert;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ActionResultParserTest and is intended
    ///to contain all ActionResultParserTest Unit Tests
    ///</summary>
    [TestClass]
    public class ActionResultParserTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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

        [Isolated]
        [TestMethod]
        public void  GetSuccessValueFromActionResultReturnsBoolean()
        {
            var fakeActionResult = new FakeController().GetFakeActionResult();
            var result = ActionResultParser.GetSuccessValue(fakeActionResult);

            Assert.IsInstanceOfType(result, typeof (bool),
                                    "ActionResultParser did not return expected boolean value from success property");
        }

        [Isolated]
        [TestMethod]
        public void GetDataFromActionResultReturnsProductPresentation()
        {
            var fakeActionResult = new FakeController().GetFakeActionResult();
            var result = ActionResultParser.GetPropertyValue<IProductPresentation>(fakeActionResult, "data");

            Assert.IsInstanceOfType(result, typeof(IProductPresentation), 
                "ActionResultParser did not return expected IProductPresentation value from data property");
        }

        public class FakeController: Controller
        {
            public ActionResult GetFakeActionResult()
            {
                var fakePresentation = Isolate.Fake.Instance<IProductPresentation>();
                return this.Direct(new { success = true, data = fakePresentation } );
            }
        }

    }
}
