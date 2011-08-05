using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.Library.Tests.UnitTests.Commands
{
    /// <summary>
    /// Summary description for TransactionManagerWithInsertProductCommandShould
    /// </summary>
    [TestClass]
    public class TransactionWithInsertProductShould
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
        public void GetAValidInsertCommand()
        {
            var command = (IInsertCommand<IProduct>)GetCommand();

            Assert.IsNotNull(command.Item);
            Assert.IsFalse(String.IsNullOrEmpty(command.Item.ProductName));
        }

        [TestMethod]
        public void BeAbleToExecuteAnInsertProductCommand()
        {
            var command = GetCommand();
            var list = new CommandQueue();
            list.Enqueue(command);

            using (var transMgr = GetTransactionManager(list))
            {
                transMgr.StartTransaction();
                transMgr.ExecuteCommands();
                transMgr.RollBackTransaction();
            }
        }

        [TestMethod]
        public void BeAbleToRunAnInsertAndSelectCommand()
        {
            
        }

        private ICommand GetCommand()
        {
            var product = (IProduct)new Product(ProductTestFixtures.GetProductDtoWithTwoSubstancesAndRoute());
            return CommandFactory.CreateInsertCommand(product);
        }

        private ITransactionManager GetTransactionManager(CommandQueue list)
        {
            return ObjectFactory.With(list).GetInstance<ITransactionManager>();
        }
    }
}
