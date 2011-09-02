using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.Tests;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    [TestClass]
    public class UnitCollectionTests : TestSessionContext
    {
        public UnitCollectionTests() : base(false)
        {
        }

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            GenFormApplication.Initialize();
        }

        [TestMethod]
        public void ThatUnitCollectionContainsUnit()
        {

            var unit = UnitServices.WithDto((UnitTestFixtures.GetTestUnitMilligram())).Get();
            var group = unit.UnitGroup;

            Assert.IsTrue(group.Units.Contains(unit));
        }
    }
}
