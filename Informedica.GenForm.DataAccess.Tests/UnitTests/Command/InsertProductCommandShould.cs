﻿using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.DataAccess.Transactions;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;
using Product = Informedica.GenForm.Library.DomainModel.Products.Product;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Command
{
    /// <summary>
    /// Summary description for InsertProductCommand
    /// </summary>
    [TestClass]
    public class InsertProductCommandShould
    {
        private TestContext testContextInstance;
        private IInsertCommand<IProduct> _command;
        private IProduct _product;
        private ProductRepository _fakeRepository;
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
        //[ClassCleanup()]
        //public static void MyClassCleanup() {}
        
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
        public void BeConstructedWithAProduct()
        {
            var command = GetInsertProductCommand(CreateNewProduct());

            Assert.IsInstanceOfType(command.Item, typeof(IProduct));
        }

        [TestMethod]
        public void BeConstructedWithTheSameProductAsPassedInTheConstructor()
        {
            var product = CreateNewProduct();
            var command = GetInsertProductCommand(product);

            Assert.AreEqual(command.Item, product);
        }

        [TestMethod]
        public void BeAddableToCommandList()
        {
            var command = GetInsertProductCommand(CreateNewProduct());
            var list = new CommandList();
            list.Add((ICommand)command);

            Assert.IsInstanceOfType(list.Commands.First(), typeof(ICommand));
        }

        [TestMethod]
        public void CallInsertOnProductRepository()
        {
            IsolateInsertCommand();

            ((IExecutable)_command).Execute(_fakeContext);
            Isolate.Verify.WasCalledWithAnyArguments(() => _fakeRepository.Insert(_fakeContext, _product));
        }


        [TestMethod]
        public void BeExecutedByTransactionManager()
        {
            IsolateInsertCommand();
            var list = new CommandList();
            list.Add((ICommand)_command);
            using (var transMgr = GetTransactionManager(list))
            {
                transMgr.ExecuteCommands();
                Isolate.Verify.WasCalledWithAnyArguments(() => _fakeRepository.Insert(_fakeContext, _product));
            }

        }

        private ITransactionManager GetTransactionManager(CommandList list)
        {
            return ObjectFactory.With(list).GetInstance < ITransactionManager>();
        }

        private void IsolateInsertCommand()
        {
            _product = CreateNewProduct();
            _command = GetInsertProductCommand(_product);
            _fakeRepository = Isolate.Fake.Instance<ProductRepository>();
            _fakeContext = Isolate.Fake.Instance<GenFormDataContext>();
            ObjectFactory.Inject(_fakeRepository);
        }

        private static IProduct CreateNewProduct()
        {
            return new Product(ProductTestFixtures.GetProductDtoWithNoSubstances());
        }

        private static IInsertCommand<IProduct> GetInsertProductCommand(IProduct product)
        {
            return ObjectFactory.With(product).GetInstance<IInsertCommand<IProduct>>();
        }
    }
}
