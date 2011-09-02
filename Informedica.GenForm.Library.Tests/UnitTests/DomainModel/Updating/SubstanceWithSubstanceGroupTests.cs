using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Updating
{
    /// <summary>
    /// Summary description for SubstanceWithSubstanceGroup
    /// </summary>
    [TestClass]
    public class SubstanceWithSubstanceGroupTests
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

        [TestMethod]
        public void ThatIfSubstanceGroupIsSetToNullSubstanceGroupDoesNotContainSubstance()
        {
            var subst = GetSubstanceWithGroup();
            var group = subst.SubstanceGroup;

            subst.RemoveFromSubstanceGroup();
            Assert.IsFalse(group.Substances.Contains(subst));
            Assert.IsNull(subst.SubstanceGroup);
        }

        [TestMethod]
        public void ThatWhenSubstanceIsRemovedFromSubstanceGroupSubstanceGroupOfSubstanceIsNull()
        {
            var subst = GetSubstanceWithGroup();
            subst.SubstanceGroup.Remove(subst);

            Assert.IsNull(subst.SubstanceGroup);            
        }

        private Substance GetSubstanceWithGroup()
        {
            var dto = SubstanceTestFixtures.GetSubstanceWithGroup();
            var subst =  Substance.Create(dto);
            var group = SubstanceGroup.Create(new SubstanceGroupDto {Name = dto.SubstanceGroupName});
            group.AddSubstance(subst);
            return subst;
        }
    }
}
