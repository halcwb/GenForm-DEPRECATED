using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Command
{
    /// <summary>
    /// Summary description for DeleteCommandShould
    /// </summary>
    [TestClass]
    public class DeleteProductCommandShould
    {
        private TestContext testContextInstance;
        private IDeleteCommand<IProduct> _command;
        private Repository<IProduct, Product> _fakeRepository;
        private GenFormDataContext _fakeContext;

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

        [Isolated]
        [TestMethod]
        public void CallProductRepositoryDelete()
        {
            IsolateDeleteCommand();
            var commandQueue = new CommandQueue();
            commandQueue.Enqueue((ICommand)_command);

            using (var transMgr = TransactionManagerFactory.CreateTransactionManager(commandQueue))
            {
                transMgr.StartTransaction();
                transMgr.ExecuteCommands();
                var selector = new Func<Product, Boolean>(x => x.ProductName == "");
                Isolate.Verify.WasCalledWithAnyArguments(() => _fakeRepository.Delete(_fakeContext, selector));
                transMgr.RollBackTransaction();
            }
        }

        private void IsolateDeleteCommand()
        {
            _command = (IDeleteCommand<IProduct>)CommandFactory.CreateDeleteCommand<IProduct>(ProductTestFixtures.GetProductDtoWithTwoSubstancesAndRoute().ProductName);
            _fakeRepository = Isolate.Fake.Instance<Repository<IProduct, Product>>();
            _fakeContext = Isolate.Fake.Instance<GenFormDataContext>();
            ObjectFactory.Inject(_fakeRepository);
        }

    }
}
