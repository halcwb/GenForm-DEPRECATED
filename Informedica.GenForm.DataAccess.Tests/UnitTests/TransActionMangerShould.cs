using System;
using Informedica.GenForm.DataAccess.Transactions;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests
{
    /// <summary>
    /// Summary description for TransActionMangerShould
    /// </summary>
    [TestClass]
    public class TransActionMangerShould
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
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
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
        public void DisposeDataContextAfterUse()
        {
            var commandList = new CommandList();
            using (var transMgr = new TransactionManager(new GenFormDataContext(), commandList))
            {
                transMgr.StartTransaction();
            }
            
        }

        [TestMethod]
        public void UsesDefaultContextIfNotPassedInConstructor()
        {
            ObjectFactory.Inject(new GenFormDataContext());
            var list = new CommandList();
            using (var transMgr = new TransactionManager(list))
            {
                try
                {
                    transMgr.StartTransaction();
                }
                catch (Exception e)
                {
                    Assert.Fail(e.ToString());
                }
            }
        }


        [TestMethod]
        public void BeAbleToStartATransaction()
        {
            using (var context = new GenFormDataContext())
            {
                var commandList = new CommandList();
                using (var transMgr = new TransactionManager(context, commandList))
                {
                    transMgr.StartTransaction();
                    Assert.IsNotNull(context.Transaction, "transaction was not started");
                }
            }
        }

        [TestMethod]
        public void BeABleToCommitATransaction()
        {
            using (var context = GetFakeDataContext())
            {
                var commandList = new CommandList();
                using (var transMgr = new TransactionManager(context, commandList))
                {
                    transMgr.CommitTransaction();
                    Isolate.Verify.WasCalledWithAnyArguments(() => context.Transaction.Commit());
                }
            }
            
        }


        [TestMethod]
        public void BeAbleToRollBackATransaction()
        {
            using (var context = GetFakeDataContext())
            {
                var commandList = new CommandList();
                using (var transMgr = new TransactionManager(context, commandList))
                {
                    transMgr.RollBackTransaction();
                    Isolate.Verify.WasCalledWithAnyArguments(() => context.Transaction.Rollback());
                }
            }
            
        }

        [TestMethod]
        public void BeAbleToExecuteTheListOfTransactions()
        {
            var command = Isolate.Fake.Instance<TestCommand>();
            var context = Isolate.Fake.Instance<GenFormDataContext>();
            var list = new CommandList();
            list.Add(command);

            using (var transMgr = new TransactionManager(new GenFormDataContext(), list))
            {
                try
                {
                    transMgr.ExecuteCommands();
                    Isolate.Verify.WasCalledWithAnyArguments(() => command.Execute(context));

                }
                catch (Exception e)
                {
                    Assert.Fail(e.ToString());
                }
            }

        }

        private static GenFormDataContext GetFakeDataContext()
        {
            var context = Isolate.Fake.Instance<GenFormDataContext>();
            Isolate.WhenCalled(() =>context.Transaction.Rollback()).IgnoreCall();

            return context;
        }
    }

    public class TestCommand : ICommand, IExecutable
    {
        #region Implementation of IExecutable

        public void Execute(GenFormDataContext context)
        {
        }

        #endregion
    }
}
