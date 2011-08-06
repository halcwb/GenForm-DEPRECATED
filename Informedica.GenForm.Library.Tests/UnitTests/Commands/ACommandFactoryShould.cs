using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Commands
{
    /// <summary>
    /// Summary description for ACommandFactoryShould
    /// </summary>
    [TestClass]
    public class ACommandFactoryShould
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
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext) { GenFormApplication.Initialize();}
        
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
        public void BeAbleToCreateAnInsertCommand()
        {
            var product = (IProduct) new Product(ProductTestFixtures.GetProductDtoWithTwoSubstancesAndRoute());
            var command = CommandFactory.CreateInsertCommand(product);

            Assert.IsNotNull(command);
        }

        [TestMethod]
        public void BeAbleToCreateASelectCommandForStringCriteria()
        {
            var command = CommandFactory.CreateSelectCommand<IProduct>("test");
            Assert.IsNotNull(command);
        }

        [TestMethod]
        public void BeAbleToCreateASelectCommandForIntCriteria()
        {
            var command = CommandFactory.CreateSelectCommand<IProduct>(1);
            Assert.IsNotNull(command);
        }

        [TestMethod]
        public void BeAbleToCreateADeleteCommandForStringCriteria()
        {
            var command = CommandFactory.CreateDeleteCommand<IProduct>("test");
            Assert.IsNotNull(command);
        }

        [TestMethod]
        public void BeAbleToCreateADeleteCommandForIntCriteria()
        {
            var command = CommandFactory.CreateDeleteCommand<IProduct>(1);
            Assert.IsNotNull(command);
        }

        [TestMethod]
        public void BeAbleToCreateAFetchAllCommand()
        {
            var command = CommandFactory.CreateSelectCommand<IProduct>();
            Assert.IsNotNull(command);
        }

        [TestMethod]
        public void BeAbleToCreateASelectSubstanceCommandsWithNoArguments()
        {
            var command = CommandFactory.CreateSelectCommand<ISubstance>();
            Assert.IsInstanceOfType(command, typeof(ISelectCommand<ISubstance>));
        }

        [TestMethod]
        public void BeAbleToCreateASelectSubstanceCommandByName()
        {
            var command = CommandFactory.CreateSelectCommand<ISubstance>("paracetamol");
            Assert.IsInstanceOfType(command, typeof(ISelectCommand<ISubstance>));
        }
    }
}
