using Informedica.GenForm.Mvc3.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;

namespace Informedica.GenForm.Mvc3.Tests
{
    
    
    /// <summary>
    ///This is a test class for LoginControllerTest and is intended
    ///to contain all LoginControllerTest Unit Tests
    ///</summary>
    [TestClass]
    public class LoginControllerTest
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


        /// <summary>
        ///A test for Login
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        //[TestMethod]
        //[HostType("ASP.NET")]
        //[UrlToTest("http://localhost/genform/default.aspx")]
        //public void LoginTest() ToDo: Global.asax does not run prior to this test.
        //{
        //    LoginController target = new LoginController(); // TODO: Initialize to an appropriate value
        //    string userName = string.Empty; // TODO: Initialize to an appropriate value
        //    string password = string.Empty; // TODO: Initialize to an appropriate value
        //    string environment = string.Empty; // TODO: Initialize to an appropriate value
        //    ActionResult expected = null; // TODO: Initialize to an appropriate value
        //    ActionResult actual = target.Login(userName, password, environment);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}
    }
}
