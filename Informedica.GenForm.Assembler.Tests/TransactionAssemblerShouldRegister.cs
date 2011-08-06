using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.Assembler.Tests
{
    /// <summary>
    /// Summary description for TransactionAssemblerShouldRegister
    /// </summary>
    [TestClass]
    public class TransactionAssemblerShouldRegister
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
        public void AnImplementationOfTransactionManager()
        {
            var list = new CommandQueue();
            ObjectFactoryAssertUtility.AssertRegistrationWith<CommandQueue, ITransactionManager>(list);
        }

        [TestMethod]
        public void AnImplementationOfInsertProductCommand()
        {
            var product = ObjectFactory.GetInstance<IProduct>();
            ObjectFactoryAssertUtility.AssertRegistrationWith<IProduct, IInsertCommand<IProduct>>(product);
        }

        [TestMethod]
        public void AnImplementationOfSelectProductCommandWithString()
        {
            ObjectFactoryAssertUtility.AssertRegistrationWith<String, ISelectCommand<IProduct>>("foo");
        }

        [TestMethod]
        public void AnImplementationOfSelectProductCommandWithInt()
        {
            ObjectFactoryAssertUtility.AssertRegistrationWith<Int32, ISelectCommand<IProduct>>(1);
        }

        [TestMethod]
        public void AnImplementationOfDeleteProductCommandWithString()
        {
            var command = Factory.ObjectFactory.Instance.Create<IDeleteCommand<IProduct>>("test");
            Assert.IsInstanceOfType(command, typeof(IDeleteCommand<IProduct>));
        }

        [TestMethod]
        public void AnImplementationOfDeleteProductCommandWithInt()
        {
            var command = Factory.ObjectFactory.Instance.Create<IDeleteCommand<IProduct>>(1);
            Assert.IsInstanceOfType(command, typeof(IDeleteCommand<IProduct>));
        }

        [TestMethod]
        public void AnImplementationOfSelectProductCommand()
        {
            ObjectFactoryAssertUtility.AssertRegistration<ISelectCommand<IProduct>>(
                ObjectFactoryAssertUtility.GetMessageFor<ISelectCommand<IProduct>>());  
        }

    }
}
