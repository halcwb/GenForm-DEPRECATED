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
    /// Summary description for CommandListShould
    /// </summary>
    [TestClass]
    public class CommandListShould
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
        public void QueueACommand()
        {
            var list = new CommandQueue();
            var command = CreateCommand1();
            list.Enqueue((ICommand)command);

            Assert.AreEqual(list.Peek(), command);
        }

        [TestMethod]
        public void ReturnTheFirstCommandEnteredFirst()
        {
            var list = new CommandQueue();
            var command1 = CreateCommand1();
            var command2 = CreateCommand2();
            list.Enqueue((ICommand)command1);
            list.Enqueue((ICommand)command2);

            Assert.AreEqual(list.Peek(), command1);
        }

        [TestMethod]
        public void IterateOverTheCommandsInOrderOfQueue()
        {
            var list = new CommandQueue();
            var command1 = CreateCommand1();
            var command2 = CreateCommand2();
            list.Enqueue((ICommand)command1);
            list.Enqueue((ICommand)command2);

            var first = true;
            foreach (var command in list.Commands)
            {
                Assert.AreEqual(command, first ? command1 : command2);
                first = false;
            }
        }

        private static IInsertCommand<IProduct> CreateCommand1()
        {
            var dto = ProductTestFixtures.GetProductDtoWithTwoSubstancesAndRoute();
            var product = ObjectFactory.With(dto).GetInstance<IProduct>();
            return ObjectFactory.With(product).GetInstance<IInsertCommand<IProduct>>();
        }

        private static IInsertCommand<IProduct> CreateCommand2()
        {
            var dto = ProductTestFixtures.GetProductDtoWithNoSubstances();
            var product = ObjectFactory.With(dto).GetInstance<IProduct>();
            return ObjectFactory.With(product).GetInstance<IInsertCommand<IProduct>>();
        }
    }
}
