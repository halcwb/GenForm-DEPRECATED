using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    /// <summary>
    /// Summary description for RouteComparerReturns
    /// </summary>
    [TestClass]
    public class RouteComparerReturns
    {
        private TestContext testContextInstance;
        private static RouteComparer _comparer;

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
            _comparer = new RouteComparer();
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

        [TestMethod]
        public void TrueWhenRouteIsSameObject()
        {
            var route = CreateRoute(new RouteDto { Abbreviation = "iv", Name = "intraveneus"});
            Assert.IsTrue(_comparer.Equals(route, route));
        }

        [TestMethod]
        public void TrueWhenRouteHasSameName()
        {
            var dto = new RouteDto {Name = "intraveneus"};
            var route = CreateRoute(dto);
            var route2 = CreateRoute(dto);

            Assert.IsTrue(_comparer.Equals(route, route2));
        }

        [TestMethod]
        public void TrueWhenRouteHasSameAbbreviation()
        {
            var dto = new RouteDto { Name = "intraveneus", Abbreviation = "iv"};
            var route = CreateRoute(dto);
            var route2 = CreateRoute(dto);

            Assert.IsTrue(_comparer.Equals(route, route2));
        }

        [TestMethod]
        public void FalseWhenRouteIsNotSameObjectAndIsEmpty()
        {
            var route = CreateRoute(new RouteDto{ Abbreviation = "iv", Name = "intraveneus"});
            var route2 = CreateRoute(new RouteDto{ Abbreviation = "or", Name =  "oraal"});

            Assert.IsFalse(_comparer.Equals(route, route2));            
        }

        private Route CreateRoute(RouteDto dto)
        {
            return Route.Create(dto);
        }
    }
}
