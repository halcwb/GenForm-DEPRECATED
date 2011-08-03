using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.DataAccess.Transactions;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services.Products.dto;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
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
        private ProductRepository _fakeRepository;
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
        public void CallProductRepositoryToFindTheProduct()
        {
            IsolateInsertCommand();

            ((IExecutable)_command).Execute(_fakeContext);
            Isolate.Verify.WasCalledWithAnyArguments(() => _fakeRepository.Fetch(_fakeContext, _selector));
        }

        private void IsolateInsertCommand()
        {
            _selector = new Func<Product, bool>(x => x.ProductId == 1);
            _command = CommandFactory.CreateSelectCommand<IProduct>(1);
            _fakeRepository = Isolate.Fake.Instance<ProductRepository>();
            _fakeContext = Isolate.Fake.Instance<GenFormDataContext>();
            ObjectFactory.Inject(_fakeRepository);
        }

    }
}
