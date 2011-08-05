using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Identification;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    /// <summary>
    /// Summary description for ADomainModelShouldHaveAnIdentifirer
    /// </summary>
    [TestClass]
    public class ADomainModelShouldHaveAnIdentifirer
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
        [ClassInitialize()]
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
        public void ThatCanIdentifyAnObject()
        {
            var substanceDto = new SubstanceDto { SubstanceId = 1, SubstanceName = "paracetamol"};
            ISubstance subst = DomainFactory.Create<ISubstance, SubstanceDto>(substanceDto);
            var subst2 = DomainFactory.Create<ISubstance, SubstanceDto>(substanceDto);

            Assert.IsTrue(IdentityComparer.Compare(subst, subst2));
        }
    }
}
