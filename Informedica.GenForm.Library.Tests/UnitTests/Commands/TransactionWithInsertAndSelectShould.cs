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
    /// Summary description for TransactionWithInsertAndSelectShould
    /// </summary>
    [TestClass]
    public class TransactionWithInsertAndSelectShould
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
        public void BeAbleToFindInsertedProductByName()
        {
            var product = ProductFactory.CreateProduct(ProductTestFixtures.GetProductDtoWithTwoSubstancesAndRoute());
            var commandQueue = new CommandQueue();
            commandQueue.Enqueue((ICommand)CommandFactory.CreateInsertCommand(product));
            commandQueue.Enqueue((ICommand)CommandFactory.CreateSelectCommand<IProduct,String>(ProductTestFixtures.ProductName));

            using (var transMgr = TransactionManagerFactory.CreateTransactionManager(commandQueue))
            {
                transMgr.StartTransaction();
                transMgr.ExecuteCommands();
                transMgr.RollBackTransaction();

                var result = ((ISelectCommand<IProduct>)commandQueue.Commands.Last()).Result;
                Assert.AreEqual(GetProductFromList(result).ProductName, ProductTestFixtures.ProductName);
            }
        }

        private IProduct GetProductFromList(IEnumerable<IProduct> list)
        {
            return list.First();
        }
    }
}
