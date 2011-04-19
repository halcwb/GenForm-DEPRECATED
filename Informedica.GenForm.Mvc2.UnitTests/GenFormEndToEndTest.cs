using System.Web.Mvc;
using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Library.Services;
using Informedica.GenForm.Mvc2.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Mvc2.UnitTests
{
    /// <summary>
    /// Summary description for UserLoginGetSuccessOrFailureResponse
    /// </summary>
    [TestClass]
    public class GenFormEndToEndTest
    {
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

        [Isolated]
        [TestMethod]
        public void TestUserLoginGetResponse()
        {
            // Setup
            var services = GenFormServiceProvider.Instance.Resolve<ILoginServices>();
            Isolate.WhenCalled(() => services.Login(LoginUser.NewLoginUser("Admin", "Admin"))).CallOriginal();           
            var controller = new LoginController();

            // controller receives login request from GUI
            var response = controller.Login("Admin", "Admin");

            // LoginController delegates request to ServiceLayer
            Isolate.Verify.WasCalledWithExactArguments(() => services.Login(LoginUser.NewLoginUser("Admin", "Admin")));
            
            // Assert result
            var result =GetSuccessValueFromActionResult(response);
            Assert.IsTrue(result);
        }

        private static bool GetSuccessValueFromActionResult(ActionResult response)
        {
            return ActionResultParser.GetSuccessValueFromActionResult(response);
        }

        [Isolated]
        [TestMethod]
        public void TestUserFooCannotLogin()
        {
            var controller = new LoginController();

            var response = controller.Login("Foo", "Admin");

            Assert.IsFalse(GetSuccessValueFromActionResult(response));
        }
    }
}
