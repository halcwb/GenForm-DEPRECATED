using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Summary description for TransactionOfInsertSelectDeleteShould
    /// </summary>
    [TestClass]
    public class TransactionOfInsertSelectDeleteShould
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
        public void ResultInAnFirstSelectWithAndSecondSelectWithoutProduct()
        {
            var product = ProductFactory.CreateProduct(ProductTestFixtures.GetProductDtoWithTwoSubstancesAndRoute());
            var commandQueue = new CommandQueue();
            commandQueue.Enqueue(CommandFactory.CreateInsertCommand(product));
            commandQueue.Enqueue(CommandFactory.CreateSelectCommand<IProduct, String>(ProductTestFixtures.ProductName));
            commandQueue.Enqueue(CommandFactory.CreateDeleteCommand<IProduct, String>(ProductTestFixtures.ProductName));
            commandQueue.Enqueue(CommandFactory.CreateSelectCommand<IProduct, String>(ProductTestFixtures.ProductName));

            using (var transMgr = TransactionManagerFactory.CreateTransactionManager(commandQueue))
            {
                transMgr.StartTransaction();
                transMgr.ExecuteCommands();
                transMgr.RollBackTransaction();

                // dump the insert command
                commandQueue.Dequeue();
                // get the second command = first select command
                var result = ((ISelectCommand<IProduct>)commandQueue.Dequeue()).Result;
                Assert.AreEqual(GetProductFromList(result).ProductName, ProductTestFixtures.ProductName);
                // dump the third delete command
                commandQueue.Dequeue();
                // get the fourth command = second select command
                result = ((ISelectCommand<IProduct>) commandQueue.Dequeue()).Result;
                Assert.IsTrue(result.Count() == 0);
            }
        }

        private IProduct GetProductFromList(IEnumerable<IProduct> list)
        {
            return list.First();
        }

    }
}
