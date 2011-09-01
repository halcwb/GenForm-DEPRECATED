using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    [TestClass]
    public class UnitCollectionTests
    {
        [TestMethod]
        public void ThatUnitCollectionContainsUnit()
        {
            var unit = Unit.Create(UnitTestFixtures.GetTestUnitMilligram());
            var group = unit.UnitGroup;

            Assert.IsTrue(group.Units.Contains(unit));
        }
    }
}
