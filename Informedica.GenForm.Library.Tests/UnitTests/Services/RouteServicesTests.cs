using System;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.TestFixtures.Fixtures;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Services
{
    /// <summary>
    /// Summary description for RouteTests
    /// </summary>
    [TestClass]
    public class RouteServicesTests : TestSessionContext
    {
        private TestContext testContextInstance;

        public RouteServicesTests() : base(true)
        {
        }

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
        public static void MyClassInitialize(TestContext testContext) { GenFormApplication.Initialize(); }
        
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

        [TestMethod]
        public void ThatArouteCanBeGet()
        {
            var route = RouteServices.WithDto(RouteTestFixtures.GetRouteIvDto()).Get();
            Assert.IsInstanceOfType(route, typeof(Route));
        }

        public void ThatRouteHasAnId()
        {
            var route = RouteServices.WithDto(RouteTestFixtures.GetRouteIvDto()).Get();
            Assert.IsTrue(route.Id != Guid.Empty);
        }

        [TestMethod]
        public void ThatRouteCanBeFound()
        {
            var route = RouteServices.WithDto(RouteTestFixtures.GetRouteIvDto()).Get();
            Assert.AreEqual(route, RouteServices.Routes.Single(x => x.Name == route.Name));          
        }

        [TestMethod]
        public void ThatRouteCanChangeName()
        {
            var route = RouteServices.WithDto(RouteTestFixtures.GetRouteIvDto()).Get();
            RouteServices.ChangeRouteName(route, "iv_changed");

            Context.CurrentSession().Transaction.Begin();
            route = RouteServices.Routes.SingleOrDefault(x => x.Name == "iv_changed");
            Assert.IsNotNull(route);
        }

        [TestMethod]
        public void ThatRouteCanBeAssociatedWithShape()
        {
            var route = RouteServices.WithDto(RouteTestFixtures.GetRouteWithShape()).Get();
            Assert.IsTrue(route.Shapes.Count() == 1);
        }

        [TestMethod]
        public void ThatRouteWithoutAShapeCanBeDeleted()
        {
            var route = RouteServices.WithDto(RouteTestFixtures.GetRouteIvDto()).Get();
            RouteServices.Delete(route);
            route = RouteServices.Routes.SingleOrDefault(x => x.Name == route.Name);
            Assert.IsNull(route);
        }

        [TestMethod]
        public void ThatDeletingRouteWithShapeDoesNotDeleteShape()
        {
            var route = RouteServices.WithDto(RouteTestFixtures.GetRouteWithShape()).Get();
            RouteServices.Delete(route);
            var shape =
                ShapeServices.Shapes.SingleOrDefault(
                    x => x.Name == RouteTestFixtures.GetRouteWithShape().Shapes.First().Name);
            Assert.IsNotNull(shape);
        }
        
    }
}
