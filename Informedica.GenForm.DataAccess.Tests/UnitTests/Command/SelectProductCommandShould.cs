using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.DataAccess.Transactions;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
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
    /// Summary description for SelectProductCommandShould
    /// </summary>
    [TestClass]
    public class SelectProductCommandShould
    {
        private TestContext testContextInstance;

        private ISelectCommand<IProduct> _command;
        private Repository<IProduct, Product> _fakeRepository;
        private GenFormDataContext _fakeContext;
        private Func<Product, Boolean> _selector;

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
        public void CallProductRepositoryToFindTheProduct()
        {
            IsolateSelectCommand();

            ((IExecutable)_command).Execute(_fakeContext);
            Isolate.Verify.WasCalledWithAnyArguments(() => _fakeRepository.Fetch(_fakeContext, _selector));
        }

        [TestMethod]
        public void ReturnAnEmptyListForProductNameIsFoo()
        {
            GenFormApplication.Initialize();

            _command = (ISelectCommand<IProduct>)CommandFactory.CreateSelectCommand<IProduct>("foo");
            ((IExecutable)_command).Execute(new GenFormDataContext());

            Assert.IsTrue(_command.Result.Count() == 0);
        }

        [TestMethod]
        public void PutTheResultsInResultPropertyOfCommand()
        {
            GenFormApplication.Initialize();

            _command = (ISelectCommand<IProduct>)CommandFactory.CreateSelectCommand<IProduct>("foo");
            ((IExecutable)_command).Execute(new GenFormDataContext());

            Assert.IsNotNull(_command.Result);
        }

        [TestMethod]
        public void ReturnAnEmptyListForProductIdIs0()
        {
            GenFormApplication.Initialize();

            _command = (ISelectCommand<IProduct>)CommandFactory.CreateSelectCommand<IProduct>(0);
            ((IExecutable)_command).Execute(new GenFormDataContext());

            Assert.IsTrue(_command.Result.Count() == 0);
        }

        [TestMethod]
        public void SelectAllProductsForSelectWithoutCriteria()
        {
            GenFormApplication.Initialize();
            var list = CreateProductList();
            var commands = new CommandQueue();

            using (var context = CreateContext())
            {
                context.Connection.Open();
                context.Transaction = context.Connection.BeginTransaction();

                foreach (var product in list)
                {
                    commands.Enqueue(CommandFactory.CreateInsertCommand(product));
                }
                commands.Enqueue(CommandFactory.CreateSelectCommand<IProduct>());
                try
                {
                    foreach (var command in commands.Commands)
                    {
                        ((IExecutable)command).Execute(context);
                    }

                    Assert.IsTrue(((ISelectCommand<IProduct>)commands.Commands.Last()).Result.Count() == 3);
                }
                catch (Exception e)
                {
                    Assert.Fail(e.ToString());
                }
                finally
                {
                    context.Transaction.Rollback();
                    context.Connection.Close();
                }
            }
        }

        private IEnumerable<IProduct> CreateProductList()
        {
            return ProductTestFixtures.GetProductDtoListWithThreeItems().Select(
                dto => DomainFactory.Create<IProduct, ProductDto>(dto));
        }

        private GenFormDataContext CreateContext()
        {
            return new GenFormDataContext();
        }

        private void IsolateSelectCommand()
        {
            _selector = new Func<Product, bool>(x => x.ProductId == 1);
            _command = (ISelectCommand<IProduct>)CommandFactory.CreateSelectCommand<IProduct>(1);
            _fakeRepository = Isolate.Fake.Instance<Repository<IProduct, Product>>();
            _fakeContext = Isolate.Fake.Instance<GenFormDataContext>();
            ObjectFactory.Inject(_fakeRepository);
        }

    }
}
